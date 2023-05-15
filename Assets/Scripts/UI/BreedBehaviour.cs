using UnityEngine;

public class BreedBehaviour : MonoBehaviour 
{
    [SerializeField] private Compendium _compendium;
    [SerializeField] private InventoryPanelBehaviour _inventoryBehaviour;
    [SerializeField] private Inventory _inventory;

    private Hybridizer _hybridizer;
    private Mutator _mutator;
    private Breeder _breeder;
    private Bee _firstBee;
    private Bee _newBee;

    private void Awake()
    {
        _hybridizer = new Hybridizer();
        _mutator = new Mutator(_compendium.mutationEntries);
        _breeder = new Breeder(_hybridizer, _mutator);
    }

    public void Breed(Bee firstBee, Bee secondBee, int cycles)
    {
        for (int i = 0; i < cycles; i++)
        {
            if (TwoBeesAreTheSameSpecies(firstBee, secondBee))
                _newBee = _hybridizer.CreateHybridBee(firstBee, secondBee);
            else
                _newBee = _breeder.Breed(firstBee, secondBee);

            if (i == 0)
                _newBee.SetBeeType(BeeType.PRINCESS);

            _inventory.AddBee(_newBee);
        }

        _inventoryBehaviour.UpdateInventory();
    }

    private bool TwoBeesAreTheSameSpecies(Bee firstBee, Bee secondBee)
    {
        return firstBee.fullName == secondBee.fullName &&
                firstBee.isPurebred && secondBee.isPurebred;
    }
}