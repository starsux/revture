using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class player_movement : MonoBehaviour
{
    public float Speed = 5f;
    private Transform player;
    private Vector3 dir;

    private void Awake()
    {
        player = this.gameObject.GetComponent<Transform>();
        Speed /= 10;
    }

    private void Update()
    {

        // Get direction input of user
        float xmov = Input.GetAxisRaw("Horizontal");
        float ymov = Input.GetAxisRaw("Vertical");
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
        player.transform.position += dir * Speed;

    }

}
