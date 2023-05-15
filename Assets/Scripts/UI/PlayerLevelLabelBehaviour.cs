using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevelLabelBehaviour : MonoBehaviour 
{
    [SerializeField] private IntValue _playerLevel;

    private TMP_Text _label;

    private void Awake() 
    {
        _label = GetComponent<TMP_Text>();
        Update();
    }

    private void Update() 
    {
        _label.text = _playerLevel.value.ToString();
    }
}