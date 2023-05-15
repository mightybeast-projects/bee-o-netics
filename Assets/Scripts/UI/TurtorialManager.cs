using System.Collections.Generic;
using DanielLochner.Assets.SimpleScrollSnap;
using UnityEngine;

public class TurtorialManager : MonoBehaviour
{
    [SerializeField] private SimpleScrollSnap _mainScroll;
    [SerializeField] private BoolValue _secondQueenCatched;
    [SerializeField] private GameObject _researchPanel;
    [SerializeField] private IntValue _honey;
    [SerializeField] private HintPanelBehaviour _hintPanelBehaviour;
    [SerializeField] private GameState _gameState;

    private int _lastHintIndex;

    private void Start() 
    {
        if (_gameState.playerLevel.value >= 2)
            Destroy(gameObject);
    }

    private void Update()
    {
        if (_lastHintIndex == 0 && _mainScroll.enabled)
            ShowNextHint();
        else if (_lastHintIndex == 1 && _honey.value >= 2)
            ShowNextHint();
        else if (_lastHintIndex == 2 && _gameState.compendiumState.discoveredSpecies.Count == 1)
            ShowNextHint();
        else if (_lastHintIndex == 3 && _secondQueenCatched.value)
            ShowNextHint();
        else if (_lastHintIndex == 4 && _researchPanel.activeInHierarchy)
            ShowNextHint();
        else if (_lastHintIndex == 5 && _gameState.compendiumState.discoveredMutations.Count == 1)
        {
            ShowNextHint();
            Destroy(gameObject);
        }
    }

    private void ShowNextHint()
    {
        _hintPanelBehaviour.AddNextHintAndOpen();
        _lastHintIndex++;
    }
}