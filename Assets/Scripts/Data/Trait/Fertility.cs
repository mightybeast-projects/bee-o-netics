using UnityEngine;

[CreateAssetMenu(fileName = "Fertility", menuName = "BeeBreedingPrototype/Trait/Fertility")]
public class Fertility : Trait
{
    public int childrenAmount;

    public override string GetDescription()
    {
        return "<sprite name=bee> x " + childrenAmount;
    }
}