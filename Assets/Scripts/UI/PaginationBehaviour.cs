using UnityEngine;
using UnityEngine.UI;

public class PaginationBehaviour : MonoBehaviour
{
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Image _selectedImage;

    public void ChangeStatus(bool status)
    {
        _backgroundImage.enabled = !status;
        _selectedImage.enabled = status;
    }
}