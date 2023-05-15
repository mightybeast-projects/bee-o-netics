using System;
using UnityEngine;
using UnityEngine.UI;

public class BreedingSlotsObserver : MonoBehaviour 
{
    [SerializeField] private BreedBehaviour _breedingBehaviour;
    [SerializeField] private HoneyMakerBehaviour _honeyMakerBehaviour;
    [SerializeField] private Slider _breedingSlider;
    [SerializeField] private BeeDropSlot _firstBreedingSlot;
    [SerializeField] private BeeDropSlot _secondBreedingSlot;
    [SerializeField] private ProgressionManager _progressionManager;
    [SerializeField] private GameState _gameState;
    [SerializeField] private GameObject _queenMask;
    [SerializeField] private ParticleSystem _beeParticle;
    
    private bool _sliderEmpty = true;
    private float _fillTime;
    private int _beeLifespan;
    private Bee _firstBee;
    private Bee _secondBee;

    private void Update()
    {
        if (_sliderEmpty)
        {
            if (BeeParentsAreCorrectlyPlaced())
            {
                if (!_gameState.mutationEnabled && BeeParentsAreNotTheSameSpecies()) return;

                FillSlider();

                if (SliderReachedMaxValue())
                {
                    try { BeginBreeding(); }
                    catch (Exception) { _sliderEmpty = true; }
                }
            }
            else
            {
                _breedingSlider.value = 0;
                _fillTime = 0;
            }
        }
        else
        {
            EmptySlider();

            if (SliderReachedMinValue())
                EndBreeding();
        }
    }

    private void EmptySlider()
    {
        _breedingSlider.value = Mathf.Lerp(_breedingSlider.maxValue, _breedingSlider.minValue, _fillTime);

        _fillTime += (float) 1 / _beeLifespan * Time.deltaTime;
    }

    private void FillSlider()
    {
        _breedingSlider.value = Mathf.Lerp(_breedingSlider.minValue, _breedingSlider.maxValue, _fillTime);

        _fillTime += (float) 1 / 3 * Time.deltaTime;
    }

    private void BeginBreeding()
    {
        _sliderEmpty = false;
        _fillTime = 0;

        _firstBee = _firstBreedingSlot.PeekBee();
        _secondBee = _secondBreedingSlot.PopBee();
        EnableBreedingAnimations();

        _beeLifespan = ((Lifespan)_firstBreedingSlot.PeekBee().activeTraits[0]).duration;
        StartCoroutine(_honeyMakerBehaviour.MakeHoney(_firstBee));
    }

    private void EndBreeding()
    {
        _sliderEmpty = true;
        _fillTime = 0;

        _firstBreedingSlot.PopBee();
        DisableBreedingAnumations();
        int breedingCycles = ((Fertility) _firstBee.activeTraits[2]).childrenAmount;

        _breedingBehaviour.Breed(_firstBee, _secondBee, breedingCycles);
        _progressionManager.UpdateProgress();
    }

    private void EnableBreedingAnimations()
    {
        _queenMask.SetActive(true);
        _beeParticle.textureSheetAnimation.AddSprite(_firstBee.activeSpecies.sprite);
        _beeParticle.gameObject.SetActive(true);
    }

    private void DisableBreedingAnumations()
    {
        _queenMask.SetActive(false);
        _beeParticle.gameObject.SetActive(false);
        _beeParticle.textureSheetAnimation.RemoveSprite(0);
    }

    private bool SliderReachedMinValue()
    {
        return _breedingSlider.value == _breedingSlider.minValue;
    }

    private bool SliderReachedMaxValue()
    {
        return _breedingSlider.value == _breedingSlider.maxValue;
    }

    private bool SlotsAreNotEmpty()
    {
        return _firstBreedingSlot.slotFilled && _secondBreedingSlot.slotFilled;
    }

    private bool BeeParentsAreCorrectlyPlaced()
    {
        if (SlotsAreNotEmpty())
            return _firstBreedingSlot.PeekBee().beeType == BeeType.PRINCESS &&
                    _secondBreedingSlot.PeekBee().beeType == BeeType.DRONE;
        return false;
    }

    private bool BeeParentsAreNotTheSameSpecies()
    {
        return _firstBreedingSlot.PeekBee().fullName != _secondBreedingSlot.PeekBee().fullName;
    }
}