using System;
using DanielLochner.Assets.SimpleScrollSnap;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class ProgressionManager : MonoBehaviour 
{
    [SerializeField] private CompendiumBehaviour _compendiumBehaviour;
    [SerializeField] private Compendium _compendium;
    [SerializeField] private GameState _gameState;
    [SerializeField] private Serializer _serializer;
    [SerializeField] private GameObject _victoryScreen;
    [SerializeField] private bool _resetOnQuit = false;

    [BoxGroup("Panels")] [SerializeField] private SimpleScrollSnap _mainScroll;
    [BoxGroup("Panels")] [SerializeField] private GameObject _pagination;
    [BoxGroup("Panels")] [SerializeField] private GameObject _secondBreedingPanel;
    [BoxGroup("Panels")] [SerializeField] private GameObject _researcherPanel;
    [BoxGroup("Panels")] [SerializeField] private GameObject _trashSlot;
    [BoxGroup("Panels")] [SerializeField] private Button _infoPanelCloseButton;
    [BoxGroup("Panels")] [SerializeField] private Button _infoPanelBackground;

    [BoxGroup("Bee buttons")] [SerializeField] private GameObject _bSpeciesButton;
    [BoxGroup("Bee buttons")] [SerializeField] private GameObject _dSpeciesButton;
    [BoxGroup("Bee buttons")] [SerializeField] private GameObject _fSpeciesButton;

    private bool _shownDiscoveredMutations;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        _serializer.DeserializeGameState();

        if (_gameState.isNewGame)
            _mainScroll.enabled = false;
    }

    private void Start()
    {
        if (!_gameState.isNewGame)
        {
            _mainScroll.enabled = true;
            _pagination.SetActive(true);
            LoadUnlockedContent();
            _mainScroll.GoToPanel(1);
        }
    } 

    public void UpdateProgress()
    {
        UpdateCompendiumProgress();
        UnlockNewContent();

        if (CompeniumIsFull()) ShowVictoryScreen();
    }

    public void ResetOnQuit()
    {
        _resetOnQuit = true;
    }

    private void LoadUnlockedContent()
    {
        UpdateCompendiumProgress();

        _researcherPanel.SetActive(true);
        UnlockNewBeeButtonAndTransition(_bSpeciesButton);
        UnlockNewBeeButtonAndTransition(_dSpeciesButton);
        
        if (_gameState.compendiumState.discoveredSpecies.Count >= 4)
            UnlockNewBeeButtonAndTransition(_fSpeciesButton);
        if (_gameState.compendiumState.discoveredSpecies.Count >= 5)
        {
            _secondBreedingPanel.SetActive(true);
            _trashSlot.SetActive(true);
        }
    }

    private void UpdateCompendiumProgress()
    {
        foreach (InventorySlot slot in _gameState.inventory.slots)
            if (BeeInSlotCanBeUnlocked(slot))
                _compendiumBehaviour.DiscoverBeeSpecies(slot.bee.activeSpecies);
    }

    private void UnlockNewContent()
    {
        if (_gameState.playerLevel.value == 0 && _gameState.compendiumState.discoveredSpecies.Count >= 1)
        {
            _mainScroll.GoToPanel(2);
            _infoPanelCloseButton.onClick.AddListener(() => SecondBeeSpeciesTransition()());
            _infoPanelBackground.onClick.AddListener(() => SecondBeeSpeciesTransition()());
        }
        if (_gameState.playerLevel.value == 1 && _gameState.compendiumState.discoveredSpecies.Count >= 2)
            InvokeAndLevelUp(() => _researcherPanel.SetActive(true));
        if (_gameState.playerLevel.value == 2 && _gameState.compendiumState.discoveredSpecies.Count >= 3)
            InvokeAndLevelUp(() => UnlockNewBeeButtonAndTransition(_dSpeciesButton));
        if (_gameState.playerLevel.value == 3 && _gameState.compendiumState.discoveredSpecies.Count >= 4)
            InvokeAndLevelUp(() => UnlockNewBeeButtonAndTransition(_fSpeciesButton));
        if (_gameState.playerLevel.value == 4 && _gameState.compendiumState.discoveredSpecies.Count >= 5)
            InvokeAndLevelUp(() =>  {
                _secondBreedingPanel.SetActive(true);
                _trashSlot.SetActive(true);
                });
    }

    private Action SecondBeeSpeciesTransition()
    {
        return () => 
        { 
            if (!_gameState.beeButtonsStates[1].value)
                    InvokeAndLevelUp(() => UnlockNewBeeButtonAndTransition(_bSpeciesButton)); 
        };
    }

    private void InvokeAndLevelUp(Action action)
    {
        action();
        _gameState.playerLevel.value++;
    }

    private void UnlockNewBeeButtonAndTransition(GameObject beeButton)
    {
        _mainScroll.GoToPanel(0);
        beeButton.SetActive(true);
    }

    private void ShowVictoryScreen()
    {
        _victoryScreen.gameObject.SetActive(true);
    }

    private void OnApplicationPause(bool pauseStatus) 
    {
        if (pauseStatus)
        {
            HandleApplicationExit();
            Application.Quit();
        }
    }

    private void OnApplicationQuit()
    {
        HandleApplicationExit();
    }

    private void HandleApplicationExit()
    {
        if (_resetOnQuit || _gameState.isNewGame)
            _gameState.Reset();

        _serializer.SerializeGameState();  
    }

    private bool BeeInSlotCanBeUnlocked(InventorySlot slot)
    {
        return BeeIsNotUnlocked(slot.bee) && 
                slot.bee.beeType == BeeType.DRONE &&
                slot.bee.isPurebred && 
                slot.amount >= 3;
    }

    private bool BeeIsNotUnlocked(Bee bee)
    {
        return !_gameState.compendiumState.discoveredSpecies.Contains(bee.activeSpecies);
    }

    private bool CompeniumIsFull()
    {
        return _gameState.compendiumState.discoveredSpecies.Count == _compendium.beeDatas.Count;
    }

}