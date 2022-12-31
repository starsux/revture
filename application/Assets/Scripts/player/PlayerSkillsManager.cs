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

    private void Update()
    {

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

    private IEnumerator RunSkill(PlayerSkills skill)
    {
        PlayerSkills.Current = skill;
        if (!SkillActivated)
        {
            SkillActivated = true;

            _mechanics.CallSkillFunction(skill.skill_Type,skill.SkillDuration);

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
                    if(PowerBar.fillAmount >= 1)
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