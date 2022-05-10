using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairFollowMouse : MonoBehaviour
{
    private Canvas UI_Canvas;
    // Start is called before the first frame update
    void Start()
    {
        UI_Canvas = transform.parent.gameObject.GetComponent<Canvas>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 mousePositionViewport = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector2 crosshairPosInRect = new Vector2(UI_Canvas.pixelRect.xMax * mousePositionViewport.x, UI_Canvas.pixelRect.yMax * mousePositionViewport.y);
        
        GetComponent<RectTransform>().anchoredPosition = crosshairPosInRect;
    }
}
