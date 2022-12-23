using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public ParticleSystem FX_smoke;
    public string CharactersSpritePath;
    public CapsuleCollider2D Normal_Collider;
    public CapsuleCollider2D SLM_Collider;
    public bool NormalMode = true; // Normal/Slime
    public PlayableCharacters CurrentCharacter;
    private int CharactersMax = 0;

    public KeyCode KeyForToggleSlime = KeyCode.Q;

    public Sprite Arantia;
    public Sprite Arantia_SLM;
    public Sprite Pikun;
    public Sprite Pikun_SLM;
    public Sprite Ren;
    public Sprite Ren_SLM;
    public Sprite Stenpek;
    public Sprite Stenpek_SLM;


    void Start()
    {
        CharactersMax = Enum.GetNames(typeof(PlayableCharacters)).Length;

        // Get current character
        CurrentCharacter = GameManager.currentGame.PlayerCharacter;

        // Switch sprites to current
        SwitchCharacter((int)CurrentCharacter);
    }

    // Update is called once per frame
    void Update()
    {
        // User press key toggle?
        if (Input.GetKeyUp(KeyForToggleSlime))
        {
            TogglePlayerMode();
        }

        // User tries to switch character?
        for(int i=1; i<CharactersMax; i++)
        {
            if (Input.GetKeyUp(i.ToString()))
            {
                SwitchCharacter(i-1);

            }
        }
    }

    private void SwitchCharacter(int indexEnum)
    {
        // Toggle to normal mode
        NormalMode = false;
        TogglePlayerMode();

        // Change current character
        CurrentCharacter = (PlayableCharacters)indexEnum;

        // Store in pesistence
        GameManager.currentGame.PlayerCharacter = CurrentCharacter;
        RevtureGame.SaveAll();

        // change sprites
        switch (CurrentCharacter)
        {
            case PlayableCharacters.Arantia:
                Normal_Collider.GetComponent<SpriteRenderer>().sprite = Arantia;
                SLM_Collider.GetComponent<SpriteRenderer>().sprite = Arantia_SLM;
                break;
            case PlayableCharacters.Pikun:
                Normal_Collider.GetComponent<SpriteRenderer>().sprite = Pikun;
                SLM_Collider.GetComponent<SpriteRenderer>().sprite = Pikun_SLM;
                break;
            case PlayableCharacters.Ren:
                Normal_Collider.GetComponent<SpriteRenderer>().sprite = Ren;
                SLM_Collider.GetComponent<SpriteRenderer>().sprite = Ren_SLM;
                break;
            case PlayableCharacters.Stenpek:
                Normal_Collider.GetComponent<SpriteRenderer>().sprite = Stenpek;
                SLM_Collider.GetComponent<SpriteRenderer>().sprite = Stenpek_SLM;
                break;
        }

        // Switch fx
        FX_smoke.gameObject.SetActive(CurrentCharacter == PlayableCharacters.Ren);

    }

    // Switch between slime/normal
    private void TogglePlayerMode()
    {
        // Set as opposite of current value
        NormalMode = !NormalMode;

        if (NormalMode)
        {
            // Enable normal sprite
            Normal_Collider.gameObject.SetActive(true);

            // disable slime sprite
            SLM_Collider.gameObject.SetActive(false);

        }
        else
        {
            // Disbale normal sprite
            Normal_Collider.gameObject.SetActive(false);

            // Enable slime sprite
            SLM_Collider.gameObject.SetActive(true);

        }
    }

    /// <summary>
    /// Determines what speed will be used based on current mode
    /// </summary>
    /// <param name="speed"></param>
    /// <param name="slime_Speed"></param>
    /// <returns></returns>
    internal float GetSpeed(float speed, float slime_Speed)
    {
        if (NormalMode) return speed;
        else return slime_Speed;
    }
}
