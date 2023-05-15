using System.IO;
using UnityEngine;

public class Serializer : MonoBehaviour 
{
    [SerializeField] private GameState _gameState;

    private string _gameStateStr;
    private string filePath;

    private void Awake() 
    {
        filePath = Application.persistentDataPath + "/save.dat";
    }

    public void SerializeGameState()
    {
        string beeButtonsJson = "";
        foreach (BoolValue boolValue in _gameState.beeButtonsStates)
            beeButtonsJson += JsonUtility.ToJson(boolValue, true) + "\n*\n";
        string playerLevelJson = JsonUtility.ToJson(_gameState.playerLevel, true);
        string honeyJson = JsonUtility.ToJson(_gameState.honey, true);
        string compendiumStateJson = JsonUtility.ToJson(_gameState.compendiumState, true);
        string inventoryJson = JsonUtility.ToJson(_gameState.inventory, true);

        string _gameStateStr = beeButtonsJson + 
                                playerLevelJson + "\n*\n" + honeyJson + "\n*\n" + 
                                compendiumStateJson + "\n*\n" + inventoryJson;

        File.WriteAllText(filePath, _gameStateStr);
    }

    public void DeserializeGameState()
    {
        if (!File.Exists(filePath)) return;

        _gameStateStr = File.ReadAllText(filePath);

        string[] data = _gameStateStr.Split('*');
        for (int i = 0; i < _gameState.beeButtonsStates.Count; i++)
            JsonUtility.FromJsonOverwrite(data[i], _gameState.beeButtonsStates[i]);
        JsonUtility.FromJsonOverwrite(data[4], _gameState.playerLevel);
        JsonUtility.FromJsonOverwrite(data[5], _gameState.honey);
        JsonUtility.FromJsonOverwrite(data[6], _gameState.compendiumState);
        JsonUtility.FromJsonOverwrite(data[7], _gameState.inventory);
    }
}