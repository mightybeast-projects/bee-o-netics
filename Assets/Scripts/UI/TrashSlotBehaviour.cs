using UnityEngine;

public class TrashSlotBehaviour : MonoBehaviour
{
    private static TrashSlotBehaviour _instance;

    private void Awake() 
    {
        _instance = this;
        Hide();
    }

    public static void Show()
    {
        _instance.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        _instance.gameObject.SetActive(false);
    }
}