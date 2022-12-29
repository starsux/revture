using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkillsManager : MonoBehaviour
{
    public PlayerSkills[] Skills;
    public bool NormalMode = true; // Normal/Slime
    public ParticleSystem FX_slm;
    public CapsuleCollider2D SLM_Collider;
    public PlayerManager _PM;
    private PlayerSkills Current_skill = new PlayerSkills();
    public Image PowerBar;

    private void Start()
    {
        Current_skill.skill_Type = PlayerSkills.SkillType.none;
    }

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
        Current_skill = skill;
        switch(skill.skill_Type)
        {
            case PlayerSkills.SkillType.slime:
                Slime();
                break;
        }

        StartCoroutine(UpdatePowerBar());
        yield return new WaitForSeconds(Current_skill.SkillDuration);

        // End skill
        KillSkill(skill, PlayerSkills.KillSkillMode.All);

        // Recharge skill
        RechargePower(skill);

        // Reset skill to none
        Current_skill.skill_Type = PlayerSkills.SkillType.none;

    }

    private void KillSkill(PlayerSkills skill, PlayerSkills.KillSkillMode mode)
    {
        switch (mode)
        {
            case PlayerSkills.KillSkillMode.All:
                // Reset player to normal mode
                NormalMode = false;
                Slime();
                break;
        }
    }

    private IEnumerator UpdatePowerBar()
    {
        float elapsedTime = 0f;
        float fillAmountPerSecond = 1f / Current_skill.SkillDuration;

        while (elapsedTime < Current_skill.SkillDuration)
        {
            PowerBar.fillAmount = elapsedTime * fillAmountPerSecond;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    // The player explodes
    public void Suicidio()
    {

    }



    // Switch between slime/normal
    public void Slime()
    {
        // Set as opposite of current value
        NormalMode = !NormalMode;

        if (NormalMode)
        {
            // Enable normal sprite
            _PM.Normal_Collider.gameObject.SetActive(true);

            // disable slime sprite
            SLM_Collider.gameObject.SetActive(false);

            // Switch fx smoke
            _PM.FX_smoke.gameObject.SetActive(_PM.CurrentCharacter == PlayableCharacters.Ren);

            // Disable slime fx
            FX_slm.gameObject.SetActive(false);


        }
        else
        {
            // Disbale normal sprite
            _PM.Normal_Collider.gameObject.SetActive(false);

            // Enable slime sprite
            SLM_Collider.gameObject.SetActive(true);

            // Off smoke effect
            _PM.FX_smoke.gameObject.SetActive(false);

        }
    }

    public void RechargePower(PlayerSkills skill)
    {
        StartCoroutine(RechargePowerSleep(skill));
    }

    private IEnumerator RechargePowerSleep(PlayerSkills skill)
    {
        float elapsedTime = 0f;

        while (elapsedTime < skill.RechargePowerTime)
        {
            skill.CurrentPowerQuant = elapsedTime * skill.RechargePowerFactor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }


    public void RotateFXSLM(float dirx, float diry)
    {
        FX_slm.gameObject.transform.rotation = Quaternion.LookRotation(new Vector3(dirx, diry));

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
        CyclicBullet
    }

    public SkillType skill_Type;
    public KeyCode KeyActivate;
    public bool unlocked = true;
    public float SkillDuration;
    public float RechargePowerTime;
    public float PowerMaxLimit;
    [HideInInspector] public float CurrentPowerQuant;
    public float RechargePowerFactor = 0.1f;

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