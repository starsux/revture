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
    private PlayerSkillsManager _PSM;

    private void Awake()
    {
        _PM = this.gameObject.GetComponent<PlayerManager>();
        _PSM = this.gameObject.GetComponent<PlayerSkillsManager>();
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
            _PSM.RotateFXSLM(xmov,ymov);
            if (!_PSM.NormalMode) _PSM.FX_slm.gameObject.SetActive(true);

        }
        else if(xmov > 0)
        {
            // Return to normal
            PlayerSprite.flipX = false;
            _PSM.RotateFXSLM(xmov,ymov);

            if (!_PSM.NormalMode) _PSM.FX_slm.gameObject.SetActive(true);

        }
        else
        {
            // Emite fx slime
            if (!_PSM.NormalMode) _PSM.FX_slm.gameObject.SetActive(false);
        }

        if(ymov != 0f)
        {
            if (!_PSM.NormalMode) _PSM.FX_slm.gameObject.SetActive(true);

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
        // Player is moving in diagonal?
        if(dir.x != 0 && dir.y != 0)
        {
            // keep normal speed
            dir /= Mathf.Sqrt(2);
            player.transform.position += dir * _PSM.GetSpeed(Speed, Slime_Speed);

        }
        else
        {
            player.transform.position += dir * _PSM.GetSpeed(Speed, Slime_Speed);

        }




    }

}
