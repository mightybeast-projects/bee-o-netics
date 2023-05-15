using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Compendium", menuName = "BeeBreedingPrototype/Compendium")]
public class Compendium : ScriptableObject 
{
    public List<BeeData> beeDatas;
    public List<MutationEntry> mutationEntries;

    public BeeData FindBeeData(Species species)
    {
        foreach (BeeData beeData in beeDatas)
            if (beeData.bee.activeSpecies == species) return beeData;
        
        return null;
    }
}