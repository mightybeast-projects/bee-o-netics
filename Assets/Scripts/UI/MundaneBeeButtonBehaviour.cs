using System.Collections;
using DanielLochner.Assets.SimpleScrollSnap;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

public class MundaneBeeButtonBehaviour : NonMundaneBeeButtonBehaviour
{
    [SerializeField] private SimpleScrollSnap _mainScroll;
    [SerializeField] private BoolValue _queenCatched;
    [SerializeField] private bool _firstButton;
    [ShowIf("_firstButton")] [SerializeField] private GameObject _pagination;

    private void Start()
    {
        transform.GetChild(0).GetComponent<Image>().sprite = _beeToAdd.bee.activeSpecies.sprite;
        if (_queenCatched.value) Destroy(gameObject);
    }

    public override void CatchBee()
    {
        base.CatchBee();

        _queenCatched.value = true;
        if (_firstButton)
        {
            _mainScroll.enabled = true;
            _pagination.SetActive(true);
        }
        
        StartCoroutine(SwitchMainPanelAndDestroyWithDelay());
    }

    private IEnumerator SwitchMainPanelAndDestroyWithDelay()
    {
        yield return new WaitForSeconds(0.01f);
        _mainScroll.GoToNextPanel();
        Destroy(gameObject);
    }
}