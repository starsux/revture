using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MAPController : MonoBehaviour
{
    public float zoom;
    public RectTransform ImageMap;

    private void Update()
    {
        // Set zoom
        ImageMap.localScale = new Vector2(zoom,zoom);


    }
}
