using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static StoryDialog;

public class Dialog : MonoBehaviour
{

    public TextMeshProUGUI _text;
    public Image _image;
    public Sprite[] Icons;
    public float WriteTime;
    public float DelayToHide;
    private bool _disposed = false;
    public Animator anim;
    public delegate void DelOnEndDialog();
    public event DelOnEndDialog OnEndDialog;

    private void Start()
    {
        this.gameObject.SetActive(false);
        _text.text = "";


    }

    /// <summary>
    /// Show a message on the scene
    /// </summary>
    /// <param name="message">Text to show as dialog</param>
    /// <param name="_icon">Icon in dialog</param>
    public void ShowDialog(StoryDialog sg, DialogImage _icon)
    {
        if (!_disposed)
        {
            _disposed = true;
            this.gameObject.SetActive(true);
            _image.sprite = GetSprite(_icon);
            sg.Done = true;
            StartCoroutine(WriteText(sg.ToString()));
        }

    }

    private Sprite GetSprite(DialogImage icon)
    {
        foreach (Sprite sprite in Icons)
        {


            if (sprite.name.Split("_")[1] == icon.ToString())
            {
                return sprite;
            }
        }
        return null;
    }

    private IEnumerator WriteText(string msg)
    {
        float elapsedTime = 0f;
        float letterQuant = msg.Length - 1 / WriteTime;
        int LetterLast = 0;

        while (elapsedTime < WriteTime)
        {
            elapsedTime += Time.deltaTime;
            LetterLast = Mathf.FloorToInt(elapsedTime * letterQuant);
            LetterLast = LetterLast > msg.Length ? msg.Length : LetterLast;
            _text.text = msg.Substring(0, LetterLast);

            if (_text.text == msg) break;

            yield return null;
        }

        StartCoroutine(DelayHide());

    }

    private IEnumerator DelayHide()
    {
        yield return new WaitForSeconds(DelayToHide);
        // Run animation
        anim.enabled = true;
    }

    public void EndAnimation()
    {
        _disposed = false;
        anim.enabled = false;

        this.gameObject.SetActive(false);
        OnEndDialog?.Invoke();
    }
}
