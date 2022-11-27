using UnityEngine;
using System.IO;
using System.Linq;
using System;

public class SaveData
{
    const int maxResults = 5;
    private PlayerData[] playerData = new PlayerData[maxResults];
    private readonly string path = Application.persistentDataPath + "/savefile.json";
    public SaveData()
    {
        if (File.Exists(path))
            ExtractData();
        else
        {
            for (int i = 0; i < playerData.Length; i++)
            {
                playerData[i] = new PlayerData();
            }
        }
    }

    public void SaveScore(string name, int score)
    {
        playerData[playerData.Length - 1].Name = name;
        playerData[playerData.Length - 1].Score = score;
        PlayerData[] result =  playerData.OrderByDescending(x => x.Score).ToArray();
        playerData = result;
        WriteToFile(playerData);
    }

    public void WriteToFile(PlayerData[] arroy)
    {
        string json = JsonHelper.ToJson(arroy, true);
        File.WriteAllText(path, json);
    }

    public void ExtractData()
    {
        string json = File.ReadAllText(path);
        playerData = JsonHelper.FromJson<PlayerData>(json);
    }

    public int MinScore()
    {
        return playerData[playerData.Length - 1].Score;
    }

    public void BestResult(out string name, out int score)
    {
        name = playerData[0].Name;
        score = playerData[0].Score;
    }

    public string ReadData()
    {
        string result = String.Empty;
        for (int i = 0; i < playerData.Length; i++)
        {
            result += $"{playerData[i].Name} : {playerData[i].Score} \r\n";
        }
        return result;
    }
}

[Serializable]
public class PlayerData
{
    public string Name;
    public int Score;
}
