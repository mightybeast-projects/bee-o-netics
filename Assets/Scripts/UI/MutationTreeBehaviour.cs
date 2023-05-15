using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MutationTreeBehaviour : MonoBehaviour
{
    [SerializeField] private Compendium _compendium;
    [SerializeField] private CompendiumState _compendiumState;
    [SerializeField] private Image _firstParent;
    [SerializeField] private GameObject _parentPlus;
    [SerializeField] private Image _secondParent;
    [SerializeField] private GameObject _equals;
    [SerializeField] private Image _selectedBee;
    [SerializeField] private GameObject _mutationPlus;
    [SerializeField] private Transform _possibleMutationsPanel;
    [SerializeField] private GameObject _possibleMutationItem;

    private BeeData _selectedBeeData;

    public void ShowTree(BeeData beeData)
    {
        _selectedBeeData = beeData;

        ShowSelectedBeeParents();
        ShowSelectedBeePossibleMutations();
    }

    private void ShowSelectedBeeParents()
    {
        MutationEntry mutationResult = _compendium.mutationEntries.Find(x => 
                    x.mutationResultData == _selectedBeeData);

        if (mutationResult != null)
        {
            ChangeParentEquationStatus(true);
            _firstParent.sprite = mutationResult.species[0].sprite;
            _secondParent.sprite = mutationResult.species[1].sprite;
        }
        else
            ChangeParentEquationStatus(false);

        _selectedBee.sprite = _selectedBeeData.bee.activeSpecies.sprite;
    }

    private void ShowSelectedBeePossibleMutations()
    {
        foreach (Transform child in _possibleMutationsPanel)
            Destroy(child.gameObject);

        _mutationPlus.SetActive(true);

        List<MutationEntry> possibleMutations = _compendium.mutationEntries.Where(x =>
                x.species.Contains(_selectedBeeData.bee.activeSpecies)).ToList();

        if (possibleMutations.Count == 0) 
        {
            _mutationPlus.SetActive(false);
            return;
        }

        foreach (MutationEntry possibleMutation in possibleMutations)
            AddPossibleMutation(possibleMutation);
    }

    private void AddPossibleMutation(MutationEntry possibleMutation)
    {
        GameObject possibleMutationItem = Instantiate(_possibleMutationItem, _possibleMutationsPanel);
        Species otherSpecies = possibleMutation.species[0];

        if (otherSpecies == _selectedBeeData.bee.activeSpecies)
            otherSpecies = possibleMutation.species[1];

        if (PlayerDiscoveredMutation(possibleMutation))
            possibleMutationItem.transform.GetChild(0).GetComponent<Image>().sprite = otherSpecies.sprite;

        if (PlayerDiscoveredMutationResult(possibleMutation))
            possibleMutationItem.transform.GetChild(2).GetComponent<Image>().sprite
                = possibleMutation.mutationResultData.bee.activeSpecies.sprite;
    }

    private void ChangeParentEquationStatus(bool status)
    {
        _firstParent.enabled = status;
        _parentPlus.SetActive(status);
        _secondParent.enabled = status;
        _equals.SetActive(status);
    }

    private bool PlayerDiscoveredMutationResult(MutationEntry possibleMutation)
    {
        return _compendiumState.discoveredSpecies.Contains(possibleMutation.mutationResultData.bee.activeSpecies);
    }

    private bool PlayerDiscoveredMutation(MutationEntry possibleMutation)
    {
        return _compendiumState.discoveredMutations.Contains(possibleMutation);
    }
}