using DanielLochner.Assets.SimpleScrollSnap;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class ResearcherBehaviour : MonoBehaviour 
{
    [SerializeField] private SimpleScrollSnap _mainScrollSnap;
    [SerializeField] private CompendiumBehaviour _compendiumBehaviour;
    [SerializeField] private Compendium _compendium;
    [SerializeField] private CompendiumState _compendiumState;
    [SerializeField] private IntValue _honey;
    [BoxGroup("ButtonPanelElements")] [SerializeField] private BeeDropSlot _firstResearchSlot;
    [BoxGroup("ButtonPanelElements")] [SerializeField] private BeeDropSlot _secondResearchSlot;
    [BoxGroup("HintPanelElements")] [SerializeField] private GameObject _hintPanel;
    [BoxGroup("HintPanelElements")] [SerializeField] private Image _firstHintImage;
    [BoxGroup("HintPanelElements")] [SerializeField] private Image _secondHintImage;
    [BoxGroup("HintPanelElements")] [SerializeField] private Image _answerImage;
    [BoxGroup("HintPanelElements")] [SerializeField] private Sprite _rightAnswerSprite;
    [BoxGroup("HintPanelElements")] [SerializeField] private Sprite _wrongAnswerSprite;

    private Bee _firstBee;
    private Bee _secondBee;
    private MutationEntry _currentMutationEntry;

    public void ResearchBees()
    {
        if (SlotsAreEmpty() || 
            (OneOfTheSlotContainsPrincess() && OneOfThePrincessesIsNotDiscovered()) || 
            NotEnoughHoney())
            return;

        InitializeHintPanel();

        if (TwoBeesAreTheSameSpecies())
        {
            _answerImage.sprite = _firstBee.activeSpecies.sprite;
            return;
        }

        _honey.value -= 5;
        
        ResearchPossibleMutation();
    }

    private void ResearchPossibleMutation()
    {
        foreach (MutationEntry me in _compendium.mutationEntries)
        {
            _currentMutationEntry = me;
            if (MutationEntryContainsBeesSpecies(_firstBee.activeSpecies, _secondBee.activeSpecies))
            {
                _answerImage.sprite = _rightAnswerSprite;
                if (PlayerDidNotDiscoverMutation())
                {
                    _compendiumBehaviour.DiscoverMutation(_currentMutationEntry);
                    _mainScrollSnap.GoToPanel(2);
                }
                return;
            }
        }

        _answerImage.sprite = _wrongAnswerSprite;
    }

    private bool PlayerDidNotDiscoverMutation()
    {
        return !_compendiumState.discoveredMutations.Contains(_currentMutationEntry);
    }

    private void InitializeHintPanel()
    {
        _hintPanel.SetActive(true);
        _firstBee = _firstResearchSlot.PopBee();
        _secondBee = _secondResearchSlot.PopBee();
        _firstHintImage.sprite = _firstBee.activeSpecies.sprite;
        _secondHintImage.sprite = _secondBee.activeSpecies.sprite;
    }

    private bool SlotsAreEmpty() 
    {
        return _firstResearchSlot.transform.childCount == 1 ||
                _secondResearchSlot.transform.childCount == 1;
    }

    private bool MutationEntryContainsBeesSpecies(Species speciesA, Species speciesB)
    {
        return speciesA != speciesB &&
                _currentMutationEntry.species.Contains(speciesA) && 
                _currentMutationEntry.species.Contains(speciesB);
    }

    private bool OneOfTheSlotContainsPrincess()
    {
        return _firstResearchSlot.PeekBee().beeType == BeeType.PRINCESS ||
                _secondResearchSlot.PeekBee().beeType == BeeType.PRINCESS;
    }

    private bool OneOfThePrincessesIsNotDiscovered()
    {
        return !_compendiumState.discoveredSpecies.Contains(_firstResearchSlot.PeekBee().activeSpecies) ||
                !_compendiumState.discoveredSpecies.Contains(_secondResearchSlot.PeekBee().activeSpecies);
    }

    private bool NotEnoughHoney()
    {
        return _honey.value < 5;
    }

    private bool TwoBeesAreTheSameSpecies()
    {
        return _firstBee.fullName == _secondBee.fullName &&
                _firstBee.isPurebred && _secondBee.isPurebred;
    }
}