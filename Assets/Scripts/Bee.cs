using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class Bee
{
    public string fullName => (activeSpecies.name == inactiveSpecies.name)? 
                                activeSpecies.name : 
                                activeSpecies.name + inactiveSpecies.name;
    public BeeType beeType;
    public Species activeSpecies;
    public Species inactiveSpecies;
    public List<Trait> activeTraits;
    public List<Trait> inactiveTraits;
    public bool isPurebred => activeSpecies == inactiveSpecies;

    public Bee()
    {
        activeTraits = new List<Trait>();
        inactiveTraits = new List<Trait>();
    }

    public void SetBeeType(BeeType beeType)
    {
        this.beeType = beeType;
    }

    public void SetSpecies(Species activeSpecies, Species inactiveSpecies)
    {
        this.activeSpecies = activeSpecies;
        this.inactiveSpecies = inactiveSpecies;
    }

    public void SetActiveSpecies(Species activeSpecies)
    {
        this.activeSpecies = activeSpecies;
    }

    public void SetInactiveSpecies(Species inactiveSpecies)
    {
        this.inactiveSpecies = inactiveSpecies;
    }

    public void SetActiveTraits(List<Trait> activeTraits)
    {
        this.activeTraits = activeTraits;
    }

    public void SetInactiveTraits(List<Trait> inactiveTraits)
    {
        this.inactiveTraits = inactiveTraits;
    }

    public void AddActiveTrait(Trait trait)
    {
        activeTraits.Add(trait);
    }

    public void AddInactiveTrait(Trait inactiveTrait)
    {
        inactiveTraits.Add(inactiveTrait);
    }

    public override bool Equals(object obj)
    {
        Bee anotherBee = obj as Bee;
        return this.beeType.Equals(anotherBee.beeType) &&
            this.activeSpecies == anotherBee.activeSpecies &&
            this.inactiveSpecies == anotherBee.inactiveSpecies &&
            this.activeTraits.SequenceEqual(anotherBee.activeTraits) &&
            this.inactiveTraits.SequenceEqual(anotherBee.inactiveTraits);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return base.ToString();
    }
}