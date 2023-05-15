using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class Hybridizer
{
    private List<Func<Bee, Bee, Bee>> _hybridizationFunctions;
    private Bee _hybridBee;

    public Hybridizer() 
    {
        _hybridizationFunctions = new List<Func<Bee, Bee, Bee>>
        {
            (firstBee, secondBee) => CreateHybridSpecies(firstBee.activeSpecies, secondBee.activeSpecies),
            (firstBee, secondBee) => CreateHybridSpecies(firstBee.activeSpecies, secondBee.inactiveSpecies),
            (firstBee, secondBee) => CreateHybridSpecies(firstBee.inactiveSpecies, secondBee.activeSpecies),
            (firstBee, secondBee) => CreateHybridSpecies(firstBee.inactiveSpecies, secondBee.inactiveSpecies)
        };
    }

    public Bee CreateHybridBee(Bee firstBee, Bee secondBee)
    {
        Func<Bee, Bee, Bee> HybridizationFunction = _hybridizationFunctions[Random.Range(0, 4)];
        _hybridBee = HybridizationFunction(firstBee, secondBee);

        for (int i = 0; i < firstBee.activeTraits.Count; i++)
        {
            if (RandomUtils.RandomBool())
                AddActiveTraitFrom(firstBee, i);
            else
                AddActiveTraitFrom(secondBee, i);

            if (RandomUtils.RandomBool())
                AddInaciveTraitFrom(firstBee, i);
            else
                AddInaciveTraitFrom(secondBee, i);
        }

        return _hybridBee;
    }

    private void AddActiveTraitFrom(Bee fromBee, int traitIndex)
    {
        if (RandomUtils.RandomBool())
            _hybridBee.AddActiveTrait(fromBee.activeTraits[traitIndex]);
        else
            _hybridBee.AddActiveTrait(fromBee.inactiveTraits[traitIndex]);
    }

    private void AddInaciveTraitFrom(Bee fromBee, int traitIndex)
    {
        if (RandomUtils.RandomBool())
            _hybridBee.AddInactiveTrait(fromBee.activeTraits[traitIndex]);
        else
            _hybridBee.AddInactiveTrait(fromBee.inactiveTraits[traitIndex]);
    }

    private Bee CreateHybridSpecies(Species speciesA, Species speciesB)
    {
        Bee bee = new Bee();
        if (RandomUtils.RandomBool())
            bee.SetSpecies(speciesA, speciesB);
        else 
            bee.SetSpecies(speciesB, speciesA);
        return bee;
    }
}