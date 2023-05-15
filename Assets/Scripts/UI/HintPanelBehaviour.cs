using System.Collections.Generic;
using UnityEngine;

public class HintPanelBehaviour : MonoBehaviour 
{
    [SerializeField] private GameObject _content;
    [SerializeField] private List<GameObject> _hintPanels;
    [SerializeField] private IntValue _playerLevel;

    private int _lastAddedHintIndex;

    private void Start()
    {
        if (_playerLevel.value < 2)
            foreach (GameObject panel in _hintPanels)
                panel.SetActive(false);
    }

    public void AddNextHintAndOpen()
    {
        _hintPanels[_lastAddedHintIndex++].SetActive(true);
        _content.SetActive(true);
    }
}