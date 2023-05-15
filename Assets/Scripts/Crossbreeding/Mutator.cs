using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class Mutator
{
    private List<MutationEntry> _mutationEntries;
    private List<Func<BeeData>> _mutationActions;
    private MutationEntry _currentMutationEntry;
    private Bee _firstParentBee;
    private Bee _secondParentBee;

    public Mutator(List<MutationEntry> mutationEntries) 
    {
        _mutationEntries = mutationEntries;

        _mutationActions = new List<Func<BeeData>>() 
        {
            () => Mutate(_firstParentBee.activeSpecies, _secondParentBee.activeSpecies),
            () => Mutate(_firstParentBee.activeSpecies, _secondParentBee.inactiveSpecies),
            () => Mutate(_firstParentBee.inactiveSpecies, _secondParentBee.activeSpecies),
            () => Mutate(_firstParentBee.inactiveSpecies, _secondParentBee.inactiveSpecies)
        };
    }

    public void AssignParentBees(Bee firstParentBee, Bee secondParentBee)
    {
        _firstParentBee = firstParentBee;
        _secondParentBee = secondParentBee;
    }

    public void TryToMutateBee(Bee hybrid)
    {
        BeeData activeGenesReplacement = _mutationActions[Random.Range(0, 4)]();
        if (activeGenesReplacement != null)
        {
            hybrid.SetActiveSpecies(activeGenesReplacement.bee.activeSpecies);
            hybrid.SetActiveTraits(activeGenesReplacement.bee.activeTraits);
        }
        BeeData inactiveGenesReplacement = _mutationActions[Random.Range(0, 4)]();
        if (inactiveGenesReplacement != null)
        {
            hybrid.SetInactiveSpecies(inactiveGenesReplacement.bee.inactiveSpecies);
            hybrid.SetInactiveTraits(inactiveGenesReplacement.bee.inactiveTraits);
        }
    }

    private BeeData Mutate(Species firstSpecies, Species secondSpecies)
    {
        foreach (MutationEntry me in _mutationEntries)
        {
            _currentMutationEntry = me;
            if (MutationEntryContainsBeesSpecies(firstSpecies, secondSpecies))
                if (MutationOccured())
                    return _currentMutationEntry.mutationResultData;
        }

        return null;
    }

    private bool MutationEntryContainsBeesSpecies(Species speciesA, Species speciesB)
    {
        return speciesA != speciesB &&
                _currentMutationEntry.species.Contains(speciesA) && 
                _currentMutationEntry.species.Contains(speciesB);
    }

    private bool MutationOccured()
    {
        return Random.value <= (float) _currentMutationEntry.mutationChance / 100;
    }
}