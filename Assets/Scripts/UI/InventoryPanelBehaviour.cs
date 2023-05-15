using UnityEngine;

public class InventoryPanelBehaviour : MonoBehaviour 
{
    public Inventory inventory => _inventory;

    [SerializeField] private Inventory _inventory;
    [SerializeField] private GameObject _droneItemPrefab;
    [SerializeField] private GameObject _princessItemPrefab;

    private void Start()
    {
        UpdateInventory();
    }

    public void UpdateInventory()
    {
        foreach (Transform slot in transform)
            foreach (Transform item in slot)
                DestroyImmediate(item.gameObject);

        foreach (InventorySlot inventorySlot in _inventory.slots)
            AddNewBeeItem(inventorySlot);       
    }

    public void InitializeNewBeeItem(InventorySlot inventorySlot, Transform panelSlot)
    {
        GameObject _beeItem = null;

        if (inventorySlot.bee.beeType == BeeType.DRONE)
            _beeItem = Instantiate(_droneItemPrefab, panelSlot);
        if (inventorySlot.bee.beeType == BeeType.PRINCESS)
            _beeItem = Instantiate(_princessItemPrefab, panelSlot);
        
        _beeItem.GetComponent<BeeItemBehaviour>().Initialize(inventorySlot, panelSlot.GetComponent<BeeDropSlot>());
    }

    private void AddNewBeeItem(InventorySlot inventorySlot)
    {
        foreach (Transform panelSlot in transform)
        {
            if (SlotIsEmpty(panelSlot))
            {
                InitializeNewBeeItem(inventorySlot, panelSlot);
                return;
            }
        }
    }

    private bool SlotIsEmpty(Transform slot)
    {
        return slot.childCount == 0;
    }
}