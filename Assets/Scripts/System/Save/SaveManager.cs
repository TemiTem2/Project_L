using System;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    #region Singleton
    public static SaveManager Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this )
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    private string GetPath(int slot)
    {
        return Path.Combine(Application.persistentDataPath, $"save{slot}.json");
    }

    #region Get
    private GameData.PlayerData GetPlayerData()
    {
        var database = Database.Instance;
        return new GameData.PlayerData
        {
            currentPlayCharName = database.currentPlayCharName,
            currentPlayerSkill = database.currentPlayerSkill,
            currentCharInfo = database.currentCharInfo,
            currentSkillInfo = database.currentSkillInfo
        };
    }
    private GameData.StatData GetStatData()
    {
        var statManager = PlayerStatManager.instance;
        return new GameData.StatData
        {
            level = statManager.level,
            exp = statManager.exp,
            expToNextLevel = statManager.expToNextLevel,
            skillPoints = statManager.skillPoints,
            protectedTargetHP = statManager.protectedTargetHP,

            statPoint = statManager.statPoint
        };
    }
    private GameData.StageData GetStageData()
    {
        var gameManager = GameManager.Instance;
        return new GameData.StageData
        {
            currentState = gameManager.currentState,
            currentDayIndex = gameManager.currentDayIndex
        };
    }    
    private GameData CurrentData()
    {
        return new GameData
        {
            playerData = GetPlayerData(),
            statData = GetStatData(),
            stageData = GetStageData()
        };
    }
    #endregion

    public void Save(int slot)
    {

        string path = GetPath(slot);
        string json = JsonUtility.ToJson(CurrentData(), true);
        File.WriteAllText(path, json);
    }

    #region Set
    private void SetPlayerData(GameData.PlayerData player)
    {
        var database = Database.Instance;
        database.currentPlayCharName = player.currentPlayCharName;
        database.currentPlayerSkill = player.currentPlayerSkill;
        database.LoadPlayerData();
        database.LoadSkillData();
    }
    private void SetStatdata(GameData.StatData stat)
    {
        var statManager = PlayerStatManager.instance;
        statManager.level = stat.level;
        statManager.exp = stat.exp;
        statManager.expToNextLevel = stat.expToNextLevel;
        statManager.skillPoints = stat.skillPoints;
        statManager.protectedTargetHP = stat.protectedTargetHP;
        statManager.statPoint = stat.statPoint;
    }
    private void SetStageData(GameData.StageData stage)
    {
        var gameManager = GameManager.Instance;
        gameManager.ChangeState(stage.currentState);
        gameManager.currentDayIndex = stage.currentDayIndex;
    }
    private void SetGameData(GameData data)
    {
        SetStageData(data.stageData);
        SetPlayerData(data.playerData);
        SetStatdata(data.statData);
    }
    #endregion
    
    public void Load(int slot)
    {
        string path = GetPath(slot);
        if (!File.Exists(path))
        {
            Debug.Log("세이브 파일 없음");
            return;
        }

        string json = File.ReadAllText(path);
        GameData data = JsonUtility.FromJson<GameData>(json);
        SetGameData(data);
    }

    public void DeleteSave(int slot)
    {
        string path = GetPath(slot);
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    public bool HasSave(int slot)
    {
        return File.Exists(GetPath(slot));
    }

    public Tuple<GameData.StageData, string> GetPreview(int slot)
    {
        if (!HasSave(slot)) return null;
        string path = GetPath(slot);
        if (!File.Exists(path)) return null;
        string json = File.ReadAllText(path);
        GameData data  = JsonUtility.FromJson<GameData>(json);
        return Tuple.Create(data.stageData, data.playerData.currentPlayCharName);
    }
}
