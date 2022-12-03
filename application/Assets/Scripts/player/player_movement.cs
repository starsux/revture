using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class player_movement : MonoBehaviour
{
    public float Speed = 5f;
    private Transform player;

    private void Awake()
    {
        player = this.gameObject.GetComponent<Transform>();
        Speed /= 10;
    }

    private void FixedUpdate()
    {
        float xmov = Input.GetAxisRaw("Horizontal");
        float ymov = Input.GetAxisRaw("Vertical");
        Vector3 nwPos = new Vector3(xmov, ymov);

        player.transform.position += nwPos * Speed;

    }

}
