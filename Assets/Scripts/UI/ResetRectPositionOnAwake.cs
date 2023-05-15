using UnityEngine;

public class ResetRectPositionOnAwake : MonoBehaviour 
{
    private void Awake() 
    {
        GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
    }
}