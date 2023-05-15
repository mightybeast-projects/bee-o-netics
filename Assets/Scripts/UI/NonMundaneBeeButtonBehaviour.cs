using UnityEngine;
using UnityEngine.UI;

public class NonMundaneBeeButtonBehaviour : MonoBehaviour
{
    [SerializeField] protected InventoryPanelBehaviour _inventoryPanel;
    [SerializeField] protected Inventory _inventory;
    [SerializeField] protected BeeData _beeToAdd;

    protected Bee _beeClone;

    public void Initialize(InventoryPanelBehaviour inventoryPanel, BeeData beeToAdd)
    {
        _inventoryPanel = inventoryPanel;
        _beeToAdd = beeToAdd;

        transform.GetChild(0).GetComponent<Image>().sprite = _beeToAdd.bee.activeSpecies.sprite;
    }

    public virtual void CatchBee()
    {
        AddNewBee(BeeType.PRINCESS);
        AddNewBee(BeeType.DRONE);
        _inventoryPanel.UpdateInventory();
    }

    private void AddNewBee(BeeType beeType)
    {
        _beeClone = Instantiate(_beeToAdd).bee;
        _beeClone.SetBeeType(beeType);
        _inventory.AddBee(_beeClone);
    }
}