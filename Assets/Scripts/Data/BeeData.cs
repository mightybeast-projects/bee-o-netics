using UnityEngine;

[CreateAssetMenu(fileName = "BeeData", menuName = "BeeBreedingPrototype/BeeData")]
public class BeeData : ScriptableObject 
{
    public Bee bee;

    private void OnValidate()
    {
        bee.inactiveSpecies = bee.activeSpecies;
        for (int i = 0; i < bee.activeTraits.Count; i++)
            bee.inactiveTraits[i] = bee.activeTraits[i];
    }
}