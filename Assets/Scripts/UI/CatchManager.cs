using UnityEngine;

public class CatchManager : MonoBehaviour
{
    [SerializeField] private InventoryPanelBehaviour _inventoryPanelBehaviour;
    [SerializeField] private Transform _nonMundaneBeeButtonPanel;
    [SerializeField] private GameObject _beeButtonPrefab;
    [SerializeField] private Compendium _compendium;
    [SerializeField] private CompendiumState _compendiumState;

    private void Start()
    {
        UpdateBeeButtonPanel();
    }

    public void UpdateBeeButtonPanel()
    {
        foreach (Transform button in _nonMundaneBeeButtonPanel)
            Destroy(button.gameObject);

        foreach (Species discoveredSpecies in _compendiumState.discoveredSpecies)
        {
            BeeData beeData = _compendium.FindBeeData(discoveredSpecies);
            GameObject newBeeButton = Instantiate(_beeButtonPrefab, _nonMundaneBeeButtonPanel);
            newBeeButton.GetComponent<NonMundaneBeeButtonBehaviour>().Initialize(_inventoryPanelBehaviour, beeData);
        }
    }
}