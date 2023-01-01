using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public bool TestMode = false;
    public ParticleSystem FX_smoke;
    public string CharactersSpritePath;
    public PlayableCharacters CurrentCharacter;
    private int CharactersMax = 0;
    public PlayerSkillsManager SkillMan;
    public SkillMechanics SkillMechs;
    public CapsuleCollider2D Normal_Collider;


    public SkillMechanics _SM;


    public Color CurrentColor;

    [Header("Character's sprite")]
    public Sprite Arantia;
    public Sprite Pikun;
    public Sprite Ren;
    public Sprite Stenpek;

    [Header("Character's color")]
    public Color Arantia_col;
    public Color Pikun_col;
    public Color Ren_col;
    public Color Stenpek_col;


    void Start()
    {
        TestMode = false;

        CharactersMax = Enum.GetNames(typeof(PlayableCharacters)).Length;

        if (!TestMode)
        {
            // Get current character
            CurrentCharacter = GameManager.currentGame.PlayerCharacter;
        }


        // Switch sprites to current
        SwitchCharacter((int)CurrentCharacter);
    }

    // Update is called once per frame
    void Update()
    {

        // User tries to switch character?
        for (int i = 1; i < CharactersMax + 1; i++)
        {

            if (Input.GetKeyUp(i.ToString()))
            {
                SwitchCharacter(i - 1);

            }
        }
    }

    public void SwitchCharacter(int indexEnum)
    {
        // Check if pause is true
        if (GameRuntime.GLOBALPAUSE) return;

        // If character to switch has suicide return
        if (GameManager.currentGame._skilldata.CharacterSuicidedState((PlayableCharacters)indexEnum)) return;

        // Toggle to normal mode
        SkillMan.NormalMode = false;
        _SM.slime();

        // Change current character
        CurrentCharacter = (PlayableCharacters)indexEnum;

        if (!TestMode)
        {
            // Store in pesistence
            GameManager.currentGame.PlayerCharacter = CurrentCharacter;
            RevtureGame.SaveAll();
        }


        CurrentColor = Color.black;

        // change sprites
        switch (CurrentCharacter)
        {
            case PlayableCharacters.Arantia:
                Normal_Collider.GetComponent<SpriteRenderer>().sprite = Arantia;
                CurrentColor = Arantia_col;
                break;
            case PlayableCharacters.Pikun:
                Normal_Collider.GetComponent<SpriteRenderer>().sprite = Pikun;
                CurrentColor = Pikun_col;

                break;
            case PlayableCharacters.Ren:
                Normal_Collider.GetComponent<SpriteRenderer>().sprite = Ren;
                CurrentColor = Ren_col;

                break;
            case PlayableCharacters.Stenpek:
                Normal_Collider.GetComponent<SpriteRenderer>().sprite = Stenpek;
                CurrentColor = Stenpek_col;

                break;
        }

        // Switch fx
        FX_smoke.gameObject.SetActive(CurrentCharacter == PlayableCharacters.Ren);

    }

    internal int CharacterIndex(PlayableCharacters charc)
    {
        return (int)charc;
    }
}