using UnityEngine;

[CreateAssetMenu(fileName = "ProductionRate", menuName = "BeeBreedingPrototype/Trait/ProductionRate")]
public class ProductionRate : Trait
{
    public int rate;

    public override string GetDescription()
    {
        return "1 <sprite name=honey> / " + rate + " sec";
    }
}