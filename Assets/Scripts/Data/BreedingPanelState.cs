using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BreedingPanelState", menuName = "BeeBreedingPrototype/BreedingPanelState")]
public class BreedingPanelState : ScriptableObject
{
    public string snapshotTime;
    public Bee firstBee;
    public Bee secondBee;
    public bool sliderEmpty;
    public float sliderValue;
    public float fillTime;
    public bool panelEmpty => !firstBee.activeSpecies && !secondBee.activeSpecies;

    public void Reset()
    {
        snapshotTime = "";
        firstBee = null;
        secondBee = null;
        sliderEmpty = true;
        sliderValue = 0;
        fillTime = 0;
    } 
}