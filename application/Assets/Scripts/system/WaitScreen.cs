using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class WaitScreen : MonoBehaviour
{
    public string CinematicFileName;
    private bool CreatingGame = false;
    private string NewGameName;
    private bool UserSkip;

    // Start is called before the first frame update
    void Start()
    {

        string[] args = TS_Actions.WStatus.Split(";");


        // Check Wait screen status
        if (args[0] == "NEWGAME")
        {
            NewGameName = args[2];
            //todo: Play cinematic  async while create new game
            var vPlayer = this.gameObject.AddComponent<VideoPlayer>();
            vPlayer.playOnAwake = false;
            vPlayer.renderMode = VideoRenderMode.CameraNearPlane;
            vPlayer.url = Application.streamingAssetsPath + "/" + CinematicFileName;
            // on end video
            vPlayer.loopPointReached += VPlayer_loopPointReached;
            // Every frame ready
            vPlayer.frameReady += VPlayer_frameReady;
            vPlayer.sendFrameReadyEvents = true;
            vPlayer.Play();

        }
        else if (args[0] == "LOADGAME")
        {
            string game_id = args[1];
            GameManager.LoadGame(game_id);
            SceneManager.LoadScene("GAME");

        }
    }

    private void Update()
    {
        if (Input.anyKey)
        {

            UserSkip = true;
        }
    }

    private void VPlayer_loopPointReached(VideoPlayer source)
    {
        // when video ends playing load scene of game with fade transition
        //Todo: SceneTransition
        SceneManager.LoadScene("GAME");
    }

    private void VPlayer_frameReady(VideoPlayer source, long frameIdx)
    {
        if (RevtureGame.CreationFinished && UserSkip)
        {
            RevtureGame.CreationFinished = false; // reset var
            // Break video
            VPlayer_loopPointReached(source);
        }

        // if video playing and game is not creation
        if (source.isPlaying && !CreatingGame)
        {
            // Set flag true
            CreatingGame = true;
            // Create game
            GameManager.CreateNewGame(NewGameName);
        }

    }

}
