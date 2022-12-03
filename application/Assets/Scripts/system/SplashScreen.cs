using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    public Object NextScene;
    public string movie;

    private void Start(){
        var vPlayer = this.gameObject.AddComponent<UnityEngine.Video.VideoPlayer>();
        vPlayer.playOnAwake = false;
        vPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
        vPlayer.url = Application.streamingAssetsPath + "/" + movie;
        vPlayer.loopPointReached += EndVideo;
        vPlayer.Play();
    }

    private void EndVideo(UnityEngine.Video.VideoPlayer source)
    {
        SceneManager.LoadScene(NextScene.name);
    }
}
