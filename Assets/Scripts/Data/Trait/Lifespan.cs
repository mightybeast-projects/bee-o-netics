using UnityEngine;

[CreateAssetMenu(fileName = "Lifespan", menuName = "BeeBreedingPrototype/Trait/Lifespan")]
public class Lifespan : Trait
{
    public int duration;

    public override string GetDescription()
    {
        return duration + " sec";
    }
}