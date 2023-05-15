using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompendiumBehaviour : MonoBehaviour 
{
    public Compendium _compendium;
    public CompendiumState _compendiumState;

    [SerializeField] private CatchManager _catchManager;
    [SerializeField] private InfoPanelBehaviour _infoPanelBehaviour;
    [SerializeField] private GameObject _compendiumItemPrefab; 
    [SerializeField] private GameObject _mutationHintItemPrefab;
    [SerializeField] private Transform _speciesPanelTransform;
    [SerializeField] private Transform _mutationsPanelTransform;
    
    private List<GameObject> _speciesItems;
    private List<GameObject> _mutationItems;
    
    private void Awake()
    {
        _speciesItems = new List<GameObject>();
        _mutationItems = new List<GameObject>();

        AddBeeCompendiumItems();
    }

    private void Start() 
    {
        UpdateCompendiumPanel();
    }

    public void DiscoverBeeSpecies(Species beeSpecies)
    {
        _compendiumState.discoveredSpecies.Add(beeSpecies);

        EnableBeeCompendiumSpecies(beeSpecies);

        _catchManager.UpdateBeeButtonPanel();
    }

    public void DiscoverMutation(MutationEntry me)
    {
        _compendiumState.discoveredMutations.Add(me);

        EnableCompendiumMutationItem(me);
    }

    private void EnableBeeCompendiumSpecies(Species beeSpecies)
    {
        GameObject beeToUnlock = _speciesItems.Find(x => x.name == beeSpecies.name);
        beeToUnlock.GetComponent<Button>().interactable = true;
        beeToUnlock.transform.GetChild(0).GetComponent<Image>().sprite = beeSpecies.sprite;
        beeToUnlock.transform.GetChild(0).GetComponent<Image>().color = Color.white;

        UnlockResultingBeeMutation(beeSpecies);
    }

    private void EnableCompendiumMutationItem(MutationEntry me)
    {
        GameObject newMutationItem = Instantiate(_mutationHintItemPrefab, _mutationsPanelTransform);
        newMutationItem.name = me.mutationResultData.bee.fullName;
        newMutationItem.transform.GetChild(0).GetComponent<Image>().sprite = me.species[0].sprite;
        newMutationItem.transform.GetChild(2).GetComponent<Image>().sprite = me.species[1].sprite;

        _mutationItems.Add(newMutationItem);
    }

    private void AddBeeCompendiumItems()
    {
        foreach (BeeData beeData in _compendium.beeDatas)
            AddBeeCompendiumItem(beeData);
    }

    private void AddBeeCompendiumItem(BeeData beeData)
    {
        GameObject newItem = Instantiate(_compendiumItemPrefab, _speciesPanelTransform);
        newItem.name = beeData.bee.fullName;
        newItem.GetComponent<Button>().onClick.AddListener(() => _infoPanelBehaviour.ShowInfo(beeData));
        _speciesItems.Add(newItem);
    }

    private void UpdateCompendiumPanel()
    {
        foreach (MutationEntry me in _compendiumState.discoveredMutations)
            EnableCompendiumMutationItem(me);

        foreach (Species species in _compendiumState.discoveredSpecies)
            EnableBeeCompendiumSpecies(species);
    }

    private void UnlockResultingBeeMutation(Species beeSpecies)
    {
        MutationEntry resultingMutationEntry = _compendium.mutationEntries.Find(x =>
                        x.mutationResultData.bee.activeSpecies == beeSpecies);

        if (resultingMutationEntry != null)
        {
            GameObject mutationItem = _mutationItems.Find(x =>
                    x.name == resultingMutationEntry.mutationResultData.bee.fullName);
            if (mutationItem == null)
            {
                DiscoverMutation(resultingMutationEntry);
                mutationItem = _mutationItems.Find(x =>
                    x.name == resultingMutationEntry.mutationResultData.bee.fullName);
            }

            ShowMutationItemResult(resultingMutationEntry, mutationItem);
        }
    }

    private void ShowMutationItemResult(MutationEntry me, GameObject mutationItem)
    {
        if (_compendiumState.discoveredSpecies.Contains(me.mutationResultData.bee.activeSpecies))
            mutationItem.transform.GetChild(4).GetComponent<Image>().sprite 
                = me.mutationResultData.bee.activeSpecies.sprite;
    }
}