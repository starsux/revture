using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

public class SkillMechanics : MonoBehaviour
{
    public PlayerSkillsManager _PMS;
    public PlayerManager _PM;
    private float Duration;

    public GameObject _CyclicBullet;
    public float CB_Speed;
    public float CB_Angle;
    [Range(0, 2)]
    public float CB_RadiusMovement;

    public GameObject SLMPREFAB;
    public CapsuleCollider2D SLM_Collider;
    private GameObject SLM;
    [HideInInspector] public ParticleSystem FX_slm;
    public Sprite Arantia_SLM;
    public Sprite Pikun_SLM;
    public Sprite Ren_SLM;
    public Sprite Stenpek_SLM;



    public void CallSkillFunction(PlayerSkills.SkillType skill_Type, float _duration)
    {
        Duration = _duration;
        MethodInfo method = this.GetType().GetMethod(skill_Type.ToString());
        method.Invoke(this, null);
    }


    // The player explodes
    public void suicidio()
    {
        // If there is activation restrictions for this skill then return
        if (_PMS.ActivationRestrictions(PlayerSkills.SkillType.suicidio)) return;
        Debug.Log("*inserte animacion de explosion");
        PlayerSkills.GetSkill(PlayerSkills.SkillType.suicidio,GameManager.currentGame._skilldata).characterSuicided.Add(_PM.CurrentCharacter);
        RevtureGame.SaveAll();
        int nextIndex = -1;
        // Get index of next character alive
        foreach (PlayableCharacters c in Enum.GetValues(typeof(PlayableCharacters)))
        {
            // is current character alive?
            if (!PlayerSkills.GetSkill(PlayerSkills.SkillType.suicidio, GameManager.currentGame._skilldata).characterSuicided.Contains(c))
            {
                nextIndex = _PM.CharacterIndex(c);
                break;
            }
        }

        _PM.SwitchCharacter(nextIndex);

        _PMS.UpdateSuicidedIcons();
    }

    public static void ResetSuicidio()
    {
        foreach (var i in GameManager.RetrieveAllStoredGames())
        {
            PlayerSkills.GetSkill(PlayerSkills.SkillType.suicidio, i._skilldata).characterSuicided = new System.Collections.Generic.List<PlayableCharacters>();
            RevtureGame.SaveAll(i);
        }

    }

    public void composer()
    {
        Debug.Log("*UI for composer");
    }

    public void cyclicBullet()
    {
        StartCoroutine(cyclicBulletRoutine(Duration));

    }

    IEnumerator cyclicBulletRoutine(float duration)
    {
        GameObject _CB = Instantiate(_CyclicBullet, this.transform);
        float elapsed = 0f;
        float radiusAmount = (CB_RadiusMovement * 2) / duration;
        _CB.transform.Rotate(0, 0, 0);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            // Change rotation pivot position by CB_radius
            Vector3 _nwpos;
            _nwpos = _CB.transform.GetChild(0).localPosition;
            _nwpos.x = Mathf.Lerp(_nwpos.x, CB_RadiusMovement, elapsed / (duration / 2));
            _nwpos.y = 0;
            _nwpos.z = 0;
            _CB.transform.GetChild(0).localPosition = _nwpos;


            // Rotate
            _CB.transform.Rotate(0, 0, CB_Angle * Time.deltaTime * CB_Speed);

            yield return null;
        }
        Destroy(_CB);

    }

    // Switch between slime/normal
    public void slime()
    {
        // Set as opposite of current value
        _PMS.NormalMode = !_PMS.NormalMode;

        if (_PMS.NormalMode)
        {
            // Enable normal sprite
            _PM.Normal_Collider.gameObject.SetActive(true);

            // disable slime sprite
            //_PMS.SLM_Collider.gameObject.SetActive(false);
            // Destroy slime 
            Destroy(SLM);

            // Switch fx smoke
            _PM.FX_smoke.gameObject.SetActive(_PM.CurrentCharacter == PlayableCharacters.Ren);

            // Disable slime fx
            //_PMS.FX_slm.gameObject.SetActive(false);


        }
        else
        {
            // Disbale normal sprite
            _PM.Normal_Collider.gameObject.SetActive(false);

            // Enable slime sprite
            //_PMS.SLM_Collider.gameObject.SetActive(true);
            // Spawn slime
            SLM = Instantiate(SLMPREFAB, this.transform);
            FX_slm = SLM.transform.GetChild(0).GetComponent<ParticleSystem>();

            // change sprites
            switch (_PM.CurrentCharacter)
            {
                case PlayableCharacters.Arantia:
                    SLM_Collider.GetComponent<SpriteRenderer>().sprite = Arantia_SLM;
                    break;
                case PlayableCharacters.Pikun:
                    SLM_Collider.GetComponent<SpriteRenderer>().sprite = Pikun_SLM;

                    break;
                case PlayableCharacters.Ren:
                    SLM_Collider.GetComponent<SpriteRenderer>().sprite = Ren_SLM;

                    break;
                case PlayableCharacters.Stenpek:
                    SLM_Collider.GetComponent<SpriteRenderer>().sprite = Stenpek_SLM;

                    break;
            }


            // Off smoke effect
            _PM.FX_smoke.gameObject.SetActive(false);

        }
    }

}
