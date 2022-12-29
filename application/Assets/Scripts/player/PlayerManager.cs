using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public ParticleSystem FX_smoke;
    public string CharactersSpritePath;
    public PlayableCharacters CurrentCharacter;
    private int CharactersMax = 0;
    public PlayerSkillsManager SkillMan;
    public CapsuleCollider2D Normal_Collider;


    public Color CurrentColor;

    [Header("Character's sprite")]
    public Sprite Arantia;
    public Sprite Arantia_SLM;
    public Sprite Pikun;
    public Sprite Pikun_SLM;
    public Sprite Ren;
    public Sprite Ren_SLM;
    public Sprite Stenpek;
    public Sprite Stenpek_SLM;

    [Header("Character's color")]
    public Color Arantia_col;
    public Color Pikun_col;
    public Color Ren_col;
    public Color Stenpek_col;


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
        // User tries to switch character?
        for(int i=1; i<CharactersMax+1; i++)
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
        SkillMan.NormalMode = false;
        SkillMan.Slime();

        // Change current character
        CurrentCharacter = (PlayableCharacters)indexEnum;

        // Store in pesistence
        GameManager.currentGame.PlayerCharacter = CurrentCharacter;
        RevtureGame.SaveAll();

        CurrentColor =  Color.black;

        // change sprites
        switch (CurrentCharacter)
        {
            case PlayableCharacters.Arantia:
                Normal_Collider.GetComponent<SpriteRenderer>().sprite = Arantia;
                SkillMan.SLM_Collider.GetComponent<SpriteRenderer>().sprite = Arantia_SLM;
                CurrentColor = Arantia_col;
                break;
            case PlayableCharacters.Pikun:
                Normal_Collider.GetComponent<SpriteRenderer>().sprite = Pikun;
                SkillMan.SLM_Collider.GetComponent<SpriteRenderer>().sprite = Pikun_SLM;
                CurrentColor = Pikun_col;

                break;
            case PlayableCharacters.Ren:
                Normal_Collider.GetComponent<SpriteRenderer>().sprite = Ren;
                SkillMan.SLM_Collider.GetComponent<SpriteRenderer>().sprite = Ren_SLM;
                CurrentColor = Ren_col;

                break;
            case PlayableCharacters.Stenpek:
                Normal_Collider.GetComponent<SpriteRenderer>().sprite = Stenpek;
                SkillMan.SLM_Collider.GetComponent<SpriteRenderer>().sprite = Stenpek_SLM;
                CurrentColor = Stenpek_col;

                break;
        }

        // Switch fx
        FX_smoke.gameObject.SetActive(CurrentCharacter == PlayableCharacters.Ren);

    }
}