using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TpPlayer : MonoBehaviour
{
    public Transform GoTo;
    public GameObject Target;
    public float SecondsToEnable = 2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Target.tag))
        {
            Target.transform.position = GoTo.position;
            GoTo.GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(WaitForEnable());
        }
    }

    IEnumerator WaitForEnable()
    {
        yield return new WaitForSeconds(SecondsToEnable);
        GoTo.GetComponent<BoxCollider2D>().enabled = true;
    }
}
