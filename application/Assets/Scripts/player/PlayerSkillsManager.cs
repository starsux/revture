using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkillsManager : MonoBehaviour
{

    public PlayerSkills[] Skills;
    public bool NormalMode = true; // Normal/Slime
    public PlayerManager _PM;
    public Image PowerBar;
    public bool SkillActivated = false;
    public SkillMechanics _mechanics;
    private GameRuntime _GM;

    private void Awake()
    {
        _GM = GameObject.FindGameObjectWithTag("MANAGER").GetComponent<GameRuntime>();
    }

    private void Start()
    {
        UpdateSuicidedIcons();
    }

    public void UpdateSuicidedIcons()
    {
        if (GameManager.currentGame._skilldata.CharactersSuicided())
        {
            foreach (var c in GameManager.currentGame._skilldata.characterSuicided)
            {
                GetSuicideIcon(c).SetActive(false);

            }
        }
    }

    private GameObject GetSuicideIcon(PlayableCharacters characterSuicided)
    {
        switch (characterSuicided)
        {
            case PlayableCharacters.Arantia: return _GM.Icon_arantia;
            case PlayableCharacters.Ren: return _GM.Icon_ren;
            case PlayableCharacters.Pikun: return _GM.Icon_pikun;
            case PlayableCharacters.Stenpek: return _GM.Icon_stenpek;
        }

        return null;
    }

    private void Update()
    {
        // If global pause is true disable skills
        if (GameRuntime.GLOBALPAUSE) return;


        // Check all skils unlocked
        foreach (PlayerSkills sk in PlayerSkills.filter(Skills, "UNLOCKED"))
        {
            // Current skill key is pressed?
            if (Input.GetKeyUp(sk.KeyActivate))
            {
                StartCoroutine(RunSkill(sk));
            }
        }

    }

    /// <summary>
    /// Check if exists enabled restriction for skill activations
    /// </summary>
    /// <returns>True if exists restriccions</returns>
    public bool ActivationRestrictions(PlayerSkills.SkillType skill)
    {
        switch (skill)
        {
            case PlayerSkills.SkillType.suicidio:
                return GameManager.currentGame._skilldata.characterSuicided.Count == Enum.GetValues(typeof(PlayableCharacters)).Length - 1;

        }

        return false;
    }

    private IEnumerator RunSkill(PlayerSkills skill)
    {
        PlayerSkills.Current = skill;
        if (!SkillActivated)
        {
            SkillActivated = true;

            _mechanics.CallSkillFunction(skill.skill_Type, skill.SkillDuration);

            StartCoroutine(DrainPower(skill));
            yield return new WaitForSeconds(skill.SkillDuration);

            // End skill
            KillSkill(skill, PlayerSkills.KillSkillMode.All);

            //// Recharge skill
            StartCoroutine(RechargePower(skill));

        }
    }

    private IEnumerator DrainPower(PlayerSkills skill)
    {
        if (ActivationRestrictions(PlayerSkills.SkillType.suicidio)) yield break;

        float elapsedTime = 0f;
        float fillAmountPerSecond = 1f / skill.SkillDuration;

        // Calculate amount per second for currentPowerQuant depending of fillamountPerSecond
        float skillAmount = skill.PowerMaxLimit / skill.SkillDuration;

        while (elapsedTime < skill.SkillDuration)
        {
            if (!_PM.TestMode)
            {
                PowerBar.fillAmount = 1 - elapsedTime * fillAmountPerSecond;

            }
            skill.CurrentPowerQuant = float.Parse((skill.PowerMaxLimit - elapsedTime * skillAmount).ToString("F1"));

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private void KillSkill(PlayerSkills skill, PlayerSkills.KillSkillMode mode)
    {
        // Reset current
        PlayerSkills.Current = null;

        switch (mode)
        {
            case PlayerSkills.KillSkillMode.All:
                // Reset player to normal mode
                NormalMode = false;
                _mechanics.slime();
                break;
            case PlayerSkills.KillSkillMode.Specific:
                break;
        }
        SkillActivated = false;

    }

    // TODO: NOT WORK
    public IEnumerator RechargePower(PlayerSkills skill)
    {

        // Check if time is not zero
        if (skill.RechargePowerTime != 0)
        {
            float elapsedTime = 0f;
            float skillAmount = skill.PowerMaxLimit / skill.RechargePowerTime; // Fill factor for power
            float fillAmount = 1 / skill.RechargePowerTime; // Fill factor for power


            while (elapsedTime < skill.SkillDuration)
            {
                if (!_PM.TestMode)
                {

                    PowerBar.fillAmount += elapsedTime * fillAmount;
                    if (PowerBar.fillAmount >= 1)
                    {
                        PowerBar.fillAmount = 1;
                        break;
                    }
                }
                skill.CurrentPowerQuant += elapsedTime * skillAmount;

                elapsedTime += Time.deltaTime;

                yield return null;
            }

            skill.CurrentPowerQuant = skill.PowerMaxLimit;

        }
        else
        {
            // If the recharge time is zero, immediately set the progress bar to full and the power to the max limit
            if (!_PM.TestMode)
            {
                PowerBar.fillAmount = 1;

            }
            skill.CurrentPowerQuant = skill.PowerMaxLimit;
        }

    }


    #region partials
    public void RotateFXSLM(float dirx, float diry)
    {
        if (!NormalMode)
        {
            _mechanics.FX_slm.gameObject.transform.rotation = Quaternion.LookRotation(new Vector3(0, diry, dirx));

        }

    }

    /// <summary>
    /// Determines what speed will be used based on current mode
    /// </summary>
    /// <param name="speed"></param>
    /// <param name="slime_Speed"></param>
    /// <returns></returns>
    public float GetSpeed(float speed, float slime_Speed)
    {
        if (NormalMode) return speed;
        else return slime_Speed;
    }
    #endregion
}

[Serializable]
public class PlayerSkills
{
    public enum KillSkillMode
    {
        All,
        Specific
    }

    public enum SkillType
    {
        none,
        slime,
        suicidio,
        composer,
        cyclicBullet
    }

    public SkillType skill_Type;
    public KeyCode KeyActivate;
    public bool unlocked = true;
    public float SkillDuration;
    public float RechargePowerTime;
    public float PowerMaxLimit;
    [HideInInspector] public float CurrentPowerQuant;
    public static PlayerSkills Current = null;

    internal static IEnumerable<PlayerSkills> filter(PlayerSkills[] skills, string v)
    {
        switch (v)
        {
            case "UNLOCKED":
                foreach (PlayerSkills s in skills)
                {
                    if (s.unlocked)
                    {
                        yield return s;
                    }
                }
                break;

        }

    }
}