public class Breeder
{
    private Hybridizer _hybridizer;
    private Mutator _mutator;
    private Bee _resultingBee;

    public Breeder(Hybridizer hybridizer, Mutator mutator)
    {
        _hybridizer = hybridizer;
        _mutator = mutator;
    }

    public Bee Breed(Bee firstBee, Bee secondBee)
    {   
        _resultingBee = _hybridizer.CreateHybridBee(firstBee, secondBee);
        _mutator.AssignParentBees(firstBee, secondBee);
        _mutator.TryToMutateBee(_resultingBee);
        return _resultingBee;
    }
}