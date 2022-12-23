using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class player_movement : MonoBehaviour
{
    public float Speed = 5f;
    public float Slime_Speed = 6f;
    public SpriteRenderer PlayerSprite;
    private Transform player;
    private Vector3 dir;
    private PlayerManager _PM;

    private void Awake()
    {
        _PM = this.gameObject.GetComponent<PlayerManager>();
        player = this.gameObject.GetComponent<Transform>();
        Speed /= 10;
        Slime_Speed /= 10;
    }

    private void Update()
    {
        // Get direction input of user
        float xmov = Input.GetAxisRaw("Horizontal");
        float ymov = Input.GetAxisRaw("Vertical");

        // Is player direction left?
        if (xmov < 0)
        {
            // Flip sprite
            PlayerSprite.flipX = true;

            // Flip fx
            _PM.RotateFXSLM(xmov,ymov);
            if (!_PM.NormalMode) _PM.FX_slm.gameObject.SetActive(true);

        }
        else if(xmov > 0)
        {
            // Return to normal
            PlayerSprite.flipX = false;
            _PM.RotateFXSLM(xmov,ymov);

            if (!_PM.NormalMode) _PM.FX_slm.gameObject.SetActive(true);

        }
        else
        {
            // Emite fx slime
            if (!_PM.NormalMode) _PM.FX_slm.gameObject.SetActive(false);
        }

        if(ymov != 0f)
        {
            if (!_PM.NormalMode) _PM.FX_slm.gameObject.SetActive(true);

        }


        // Set in axis raw
        dir = new Vector3(xmov, ymov);
        // check if user does not move the character
        if(dir == Vector3.zero && GameManager.currentGame.PlayerPosition != player.transform.position)
        {
            // Set this position as last
            GameManager.currentGame.PlayerPosition = player.transform.position;

            // Save change
            RevtureGame.SaveAll();
        }


    }

    private void FixedUpdate()
    {
        // Apply movement
        player.transform.position += dir * _PM.GetSpeed(Speed,Slime_Speed);




    }

}
