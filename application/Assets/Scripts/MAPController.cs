using UnityEngine;

public class MAPController : MonoBehaviour
{
    public float zoom;
    public RectTransform ImageMap;

    private void Update()
    {
        // Set zoom
        ImageMap.localScale = new Vector2(zoom, zoom);


    }
}
