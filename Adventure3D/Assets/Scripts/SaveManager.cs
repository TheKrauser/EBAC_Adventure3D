using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : Singleton<SaveManager>
{
    public SaveSetup saveSetup;
    private string dataPath = Application.streamingAssetsPath + "/save.txt";

    protected override void Awake()
    {
        base.Awake();
        Load();
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        
    }

    public void Save()
    {
        saveSetup.coins = ItemManager.Instance.GetCoins().value;
        saveSetup.healthPacks = ItemManager.Instance.GetLifePacks().value;
        saveSetup.health = Player.Instance.GetCurrentHealth();

        string setupToJson = JsonUtility.ToJson(saveSetup, true);
        SaveFile(setupToJson);
    }

    private void SaveFile(string json)
    {
        string path = dataPath;
        File.WriteAllText(path, json);
    }

    public void CheckLoad()
    {
        Debug.Log("SAVE SETUP: \n" + JsonUtility.ToJson(saveSetup));
    }

    public void ClearData()
    {
        CreateNewSaveSetup();

        string setupToJson = JsonUtility.ToJson(saveSetup, true);
        SaveFile(setupToJson);
    }

    public void CreateNewSaveSetup()
    {
        saveSetup = new SaveSetup();
        saveSetup.coins = 0;
        saveSetup.healthPacks = 0;
        saveSetup.health = 0;
        saveSetup.playerPosition = Vector3.zero;
        saveSetup.lastCheckpoint = 0;
    }

    public void Load()
    {
        string fileLoaded = "";

        if (File.Exists(dataPath))
        {
            fileLoaded = File.ReadAllText(dataPath);
            saveSetup = JsonUtility.FromJson<SaveSetup>(fileLoaded);
        }
        else
        {
           CreateNewSaveSetup();
        }
    }
}

[System.Serializable]
public class SaveSetup
{
    public int coins;
    public int healthPacks;
    public int health;
    public Vector3 playerPosition;
    public int lastCheckpoint;
}
