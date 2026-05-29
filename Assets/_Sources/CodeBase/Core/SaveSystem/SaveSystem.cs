using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[DefaultExecutionOrder(999)]
public class SaveSystem : MonoBehaviour
{
    private string _saveFolder;

    [SerializeField] private List<MonoBehaviour> _saveables;
    private void Awake()
    {
        _saveFolder = Application.persistentDataPath + "/save.json";
    }

    private void OnEnable()
    {
        GlobalContext.Enemy.UnitDied += SaveGame;
        GlobalContext.UpgradeSystem.UpgradeProgressed += OnUpgradeProgressed;
    }

    private void Start()
    {
        LoadGame();
    }

    private void OnDisable()
    {
        GlobalContext.Enemy.UnitDied -= SaveGame;
        GlobalContext.UpgradeSystem.UpgradeProgressed -= OnUpgradeProgressed;
    }

    public void Save(GameData data)
    {
        string json = JsonConvert.SerializeObject(data);
        File.WriteAllText(_saveFolder, json);
    }

    public GameData Load()
    {
        if (!HasSave())
            return null;

        string json = File.ReadAllText(_saveFolder);
        GameData data = JsonConvert.DeserializeObject<GameData>(json);
        return data;
    }

    public bool HasSave()
    {
        return File.Exists(_saveFolder);
    }

    private void SaveGame()
    {
        GameData data = new GameData();

        foreach (MonoBehaviour item in _saveables)
        {
            if (item.TryGetComponent<ISaveable>(out ISaveable saveable))
            {
                saveable.Save(data);
            }
        }

        Save(data);
    }

    private void LoadGame()
    {
        GameData data = Load();
        if (data == null)
            return;

        foreach (MonoBehaviour item in _saveables)
        {
            if (item.TryGetComponent<ISaveable>(out ISaveable saveable))
            {
                saveable.Load(data);
            }
        }
    }

    private void OnUpgradeProgressed(UpgradeDataSO upgrade, int level)
    {
        SaveGame();
    }
}
