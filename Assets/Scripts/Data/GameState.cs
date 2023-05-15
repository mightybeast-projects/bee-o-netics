using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameState", menuName = "BeeBreedingPrototype/GameState")]
public class GameState : ScriptableObject 
{
    public List<BoolValue> beeButtonsStates;
    public IntValue playerLevel;
    public IntValue honey;
    public CompendiumState compendiumState;
    public Inventory inventory;
    public bool isNewGame => playerLevel.value < 3;
    public bool mutationEnabled => compendiumState.discoveredMutations.Count >= 1;

    public void Reset()
    {
        foreach (BoolValue boolValue in beeButtonsStates)
            boolValue.value = false;
        compendiumState.Reset();
        inventory.Reset();
        playerLevel.value = 0;
        honey.value = 0;
    }
}