using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public CapsuleCollider2D Normal_Collider;
    public CapsuleCollider2D SLM_Collider;
    public bool NormalMode = true; // Normal/Slime
    public PlayableCharacters CurrentCharacter;

    public KeyCode KeyForToggleSlime = KeyCode.Q;

    void Start()
    {
        // Get current character
        CurrentCharacter = GameManager.currentGame.PlayerCharacter;

        // Switch sprites to current
        SwitchCharacter();
    }

    // Update is called once per frame
    void Update()
    {
        // User press key toggle?
        if (Input.GetKeyUp(KeyForToggleSlime))
        {
            TogglePlayerMode();
        }
    }

    private void SwitchCharacter()
    {
        Debug.Log("Toggle to: " + CurrentCharacter.ToString());
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
