using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public ParticleSystem FX_smoke;
    public ParticleSystem FX_slm;
    public string CharactersSpritePath;
    public CapsuleCollider2D Normal_Collider;
    public CapsuleCollider2D SLM_Collider;
    public bool NormalMode = true; // Normal/Slime
    public PlayableCharacters CurrentCharacter;
    private int CharactersMax = 0;


    public KeyCode KeyForToggleSlime = KeyCode.Q;

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

    public Image PowerBar;


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
        NormalMode = false;
        TogglePlayerMode();

        // Change current character
        CurrentCharacter = (PlayableCharacters)indexEnum;

        // Store in pesistence
        GameManager.currentGame.PlayerCharacter = CurrentCharacter;
        RevtureGame.SaveAll();

        Color currentColor =  Color.black;

        // change sprites
        switch (CurrentCharacter)
        {
            case PlayableCharacters.Arantia:
                Normal_Collider.GetComponent<SpriteRenderer>().sprite = Arantia;
                SLM_Collider.GetComponent<SpriteRenderer>().sprite = Arantia_SLM;
                currentColor = Arantia_col;
                break;
            case PlayableCharacters.Pikun:
                Normal_Collider.GetComponent<SpriteRenderer>().sprite = Pikun;
                SLM_Collider.GetComponent<SpriteRenderer>().sprite = Pikun_SLM;
                currentColor = Pikun_col;

                break;
            case PlayableCharacters.Ren:
                Normal_Collider.GetComponent<SpriteRenderer>().sprite = Ren;
                SLM_Collider.GetComponent<SpriteRenderer>().sprite = Ren_SLM;
                currentColor = Ren_col;

                break;
            case PlayableCharacters.Stenpek:
                Normal_Collider.GetComponent<SpriteRenderer>().sprite = Stenpek;
                SLM_Collider.GetComponent<SpriteRenderer>().sprite = Stenpek_SLM;
                currentColor = Stenpek_col;

                break;
        }

        // Switch fx
        FX_smoke.gameObject.SetActive(CurrentCharacter == PlayableCharacters.Ren);
        // Change color of bar
        PowerBar.color = currentColor;
    }

    internal void RotateFXSLM(float dirx, float diry)
    {
        FX_slm.gameObject.transform.rotation = Quaternion.LookRotation(new Vector3(dirx, diry));

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

            // Switch fx smoke
            FX_smoke.gameObject.SetActive(CurrentCharacter == PlayableCharacters.Ren);

            // Disable slime fx
            FX_slm.gameObject.SetActive(false);


        }
        else
        {
            // Disbale normal sprite
            Normal_Collider.gameObject.SetActive(false);

            // Enable slime sprite
            SLM_Collider.gameObject.SetActive(true);

            // Off smoke effect
            FX_smoke.gameObject.SetActive(false);

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
