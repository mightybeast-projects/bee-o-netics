using UnityEngine;

public class InfoPanelBehaviour : MonoBehaviour 
{
    [SerializeField] private GameObject _content;
    [SerializeField] private TooltipBehaviour _tooltipBehaviour;
    [SerializeField] private MutationTreeBehaviour _mutationTreeBehaviour;

    public void ShowInfo(BeeData beeData)
    {
        _content.SetActive(true);
        _tooltipBehaviour.InstancedShowBeeInfo(beeData.bee);
        _mutationTreeBehaviour.ShowTree(beeData);
    }
}