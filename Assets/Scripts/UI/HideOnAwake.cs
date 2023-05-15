using UnityEngine;

public class HideOnAwake : MonoBehaviour 
{
    private void Awake() 
    {
        gameObject.SetActive(false);
    }
}