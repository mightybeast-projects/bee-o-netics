using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CompendiumState", menuName = "BeeBreedingPrototype/CompendiumState")]
public class CompendiumState : ScriptableObject
{
    public List<Species> discoveredSpecies;
    public List<MutationEntry> discoveredMutations;

    public void Reset() 
    {
        discoveredSpecies = new List<Species>();
        discoveredMutations = new List<MutationEntry>();
    }
}