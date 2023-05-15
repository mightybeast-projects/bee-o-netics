using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MutationEntry", menuName = "BeeBreedingPrototype/MutationEntry")]
public class MutationEntry : ScriptableObject 
{
    public List<Species> species = new List<Species>(2);
    public BeeData mutationResultData;
    [Range(0, 100)] public int mutationChance;
}