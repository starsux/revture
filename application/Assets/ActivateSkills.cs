using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSkills : MonoBehaviour
{
    public void EnableSlime()
    {
        GameManager.currentGame.SkillSlotsUnlocked = 1;
        PlayerSkills.GetSkill(PlayerSkills.SkillType.slime,GameManager.currentGame._skilldata).unlocked= true;
    }
}
