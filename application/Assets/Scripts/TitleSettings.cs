using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSettings : MonoBehaviour
{
    public KeyCode ReturnKey;
    public int TitleScene;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(ReturnKey))
        {
            SceneManager.LoadScene(TitleScene);
        }
    }
}
