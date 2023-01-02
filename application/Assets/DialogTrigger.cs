using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogTrigger : MonoBehaviour
{
    public Dialog _dg;
    public int DialogIndex;
    public UnityEvent OnEndDialog;

    private void Start()
    {
        _dg.OnEndDialog += TriggerEndDialog;
    }

    private void TriggerEndDialog()
    {
        OnEndDialog?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _dg.ShowDialog(GameManager.currentGame.StoryControl.Diag[DialogIndex], GameManager.currentGame.StoryControl.Diag[DialogIndex].Character);
        }
    }

}
