using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    public int NextScene;
    private bool UserSkip;

    public string movie;

    private void Start()
    {
        var vPlayer = this.gameObject.AddComponent<UnityEngine.Video.VideoPlayer>();
        vPlayer.playOnAwake = false;
        vPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
        vPlayer.url = Application.streamingAssetsPath + "/" + movie;
        vPlayer.loopPointReached += EndVideo;
        vPlayer.Play();
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {

            UserSkip = true;
        }
    }

    private void EndVideo(UnityEngine.Video.VideoPlayer source)
    {
        SceneManager.LoadScene(NextScene);
    }
}
