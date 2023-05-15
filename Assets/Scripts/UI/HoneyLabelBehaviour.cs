using UnityEngine;
using UnityEngine.UI;

public class HoneyLabelBehaviour : MonoBehaviour 
{
    [SerializeField] private IntValue _honey;

    private Text _label;

    private void Awake()
    {
        _label = GetComponent<Text>();
        Update();
    }

    private void Update() 
    {
        _label.text = _honey.value.ToString();
    }
}