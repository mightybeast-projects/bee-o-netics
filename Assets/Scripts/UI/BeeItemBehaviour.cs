using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BeeItemBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,
    IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public InventorySlot inventorySlot => _inventorySlot;

    private BeeDropSlot _currentSlot;
    private InventorySlot _inventorySlot;
    private Canvas _canvas;
    private GraphicRaycaster _graphicRaycaster;
    private InventoryPanelBehaviour _inventoryPanel;
    private Vector3 _moveOffset;

    private void Awake() 
    {
        _canvas = GetComponentInParent<Canvas>();
        _graphicRaycaster = _canvas.GetComponent<GraphicRaycaster>();
    }

    public void Initialize(InventorySlot inventorySlot, BeeDropSlot dropSlot)
    {
        _inventoryPanel = dropSlot.transform.parent.GetComponent<InventoryPanelBehaviour>();
        _inventorySlot = inventorySlot;
        _currentSlot = dropSlot;
        _currentSlot.currentItem = this;

        ShowBee();
    }

    private void ShowBee()
    {
        GetComponent<Image>().sprite = _inventorySlot.bee.activeSpecies.sprite;
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _inventorySlot.amount.ToString();
        if (_inventorySlot.amount == 1)
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipBehaviour.ShowBeeInfo(_inventorySlot.bee);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipBehaviour.Hide();
    }

    private void RemoveBeeFromInventory()
    {
        if (BeeItemIsInInventoryPanel())
        {
            _inventoryPanel.inventory.RemoveBee(_inventorySlot.bee);
            _inventoryPanel.UpdateInventory();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        TrashSlotBehaviour.Show();

        RectTransformUtility.
            ScreenPointToLocalPointInRectangle(_canvas.transform as RectTransform, 
                eventData.position, 
                _canvas.worldCamera, 
                out Vector2 pos);
        _moveOffset = transform.position - _canvas.transform.TransformPoint(pos);

        transform.SetParent(_canvas.transform, true);

        if (BeeItemIsInInventoryPanel())
        {
            _inventoryPanel.inventory.RemoveBee(_inventorySlot.bee);
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().enabled = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {        
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _canvas.transform as RectTransform, eventData.position, _canvas.worldCamera, out Vector2 pos);
        transform.position = _canvas.transform.TransformPoint(pos) + _moveOffset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        var results = new List<RaycastResult>();
        _graphicRaycaster.Raycast(eventData, results);
        FindNewBeeSlot(results);

        if (BeeItemIsInTrashSlot())
        {
            RemoveBeeFromInventory();
            Destroy(gameObject);
        }

        if (BeeItemIsInInventoryPanel())
        {
            _inventoryPanel.inventory.AddBee(_inventorySlot.bee);
            Destroy(gameObject);
        }
        else
        {
            transform.SetParent(_currentSlot.transform);
            transform.localPosition = Vector3.zero;
        }
        
        _inventoryPanel.UpdateInventory();
        TrashSlotBehaviour.Hide();
    }

    private void FindNewBeeSlot(List<RaycastResult> results)
    {
        foreach (var hit in results)
        {
            var slot = hit.gameObject.GetComponent<BeeDropSlot>();
            if (slot)
            {
                if (!slot.slotFilled)
                {
                    _currentSlot.currentItem = null;
                    _currentSlot = slot;
                    _currentSlot.currentItem = this;
                }
                break;
            }
        }
    }

    private bool BeeItemIsInTrashSlot()
    {
        return _currentSlot.name == "TrashSlot";
    }

    private bool BeeItemIsInInventoryPanel()
    {
        return _currentSlot.transform.parent.GetComponent<InventoryPanelBehaviour>();
    }
}