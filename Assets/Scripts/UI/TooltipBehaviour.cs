using NaughtyAttributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TooltipBehaviour : MonoBehaviour 
{
    private static TooltipBehaviour _instance;
    [SerializeField] private bool _instanced;
    [BoxGroup("UI elements")] [SerializeField] private Image _beeImage;
    [BoxGroup("UI elements")] [SerializeField] private Text _beeNameText;
    [BoxGroup("UI elements")] [SerializeField] private Image _activeSpeciesImage;
    [BoxGroup("UI elements")] [SerializeField] private Text _activeSpeciesText;
    [BoxGroup("UI elements")] [SerializeField] private Image _inactiveSpeciesImage;
    [BoxGroup("UI elements")] [SerializeField] private Text _inactiveSpeciesText;
    [BoxGroup("UI elements")] [SerializeField] private Text _activeLifespan;
    [BoxGroup("UI elements")] [SerializeField] private Text _inactiveLifespan;
    [BoxGroup("UI elements")] [SerializeField] private TMP_Text _activeProductionRate;
    [BoxGroup("UI elements")] [SerializeField] private TMP_Text _inactiveProductionRate;
    [BoxGroup("UI elements")] [SerializeField] private TMP_Text _activeFertility;
    [BoxGroup("UI elements")] [SerializeField] private TMP_Text _inactiveFertility;

    private void Awake() 
    {
        if (!_instanced)
        {
            _instance = this;
            Hide();
        }
    }

    public void InstancedShowBeeInfo(Bee bee)
    {
        gameObject.SetActive(true);
        ShowInfo(bee);
    }

    public static void ShowBeeInfo(Bee bee)
    {
        _instance.gameObject.SetActive(true);
        _instance.ShowInfo(bee); 
    }

    public static void Hide()
    {
        _instance.gameObject.SetActive(false);
    }

    private void ShowInfo(Bee bee)
    {
        _beeNameText.text = bee.fullName;
        _beeImage.sprite = bee.activeSpecies.sprite;
        _activeSpeciesImage.sprite = bee.activeSpecies.sprite;
        _activeSpeciesText.text = bee.activeSpecies.name;
        _inactiveSpeciesImage.sprite = bee.inactiveSpecies.sprite;
        _inactiveSpeciesText.text = bee.inactiveSpecies.name;
        _activeLifespan.text = bee.activeTraits[0].GetDescription();
        _inactiveLifespan.text = bee.inactiveTraits[0].GetDescription();
        _activeProductionRate.text = bee.activeTraits[1].GetDescription();
        _inactiveProductionRate.text = bee.inactiveTraits[1].GetDescription();
        _activeFertility.text = bee.activeTraits[2].GetDescription();
        _inactiveFertility.text = bee.inactiveTraits[2].GetDescription();
    }
}