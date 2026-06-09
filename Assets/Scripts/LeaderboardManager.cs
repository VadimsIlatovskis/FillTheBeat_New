using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class LeaderboardManager : MonoBehaviour
{
    [Header("Name Input")]
    public VRNameInputManager nameInputManager;

    [Header("Top 3 Texts")]
    public TMP_Text firstPlaceText;
    public TMP_Text secondPlaceText;
    public TMP_Text thirdPlaceText;

    private List<LeaderboardEntry> entries = new List<LeaderboardEntry>();
    private string savePath;
    private float currentTime;

    private void Awake()
    {
        savePath = Path.Combine(Application.persistentDataPath, "leaderboard.json");
        Debug.Log("Leaderboard path: " + savePath);

        LoadLeaderboard();
        RefreshLeaderboardText();
    }

    public void SetCurrentTime(float time)
    {
        currentTime = time;
        Debug.Log("Current time set: " + currentTime);
    }

    public void SaveCurrentPlayer()
    {
        Debug.Log("SaveCurrentPlayer called");

        if (nameInputManager == null)
        {
            Debug.LogError("NameInputManager nav piesaistīts!");
            return;
        }

        string playerName = nameInputManager.GetName();
        Debug.Log("Player name from input: " + playerName);
        Debug.Log("Time to save: " + currentTime);

        entries.Add(new LeaderboardEntry
        {
            playerName = playerName,
            time = currentTime
        });

        entries = entries.OrderBy(e => e.time).ToList();

        SaveLeaderboard();
        LoadLeaderboard();
        RefreshLeaderboardText();

        Debug.Log("Leaderboard entries count: " + entries.Count);
    }

    private void RefreshLeaderboardText()
    {
        if (firstPlaceText != null)
            firstPlaceText.text = "1. ---";

        if (secondPlaceText != null)
            secondPlaceText.text = "2. ---";

        if (thirdPlaceText != null)
            thirdPlaceText.text = "3. ---";

        if (entries.Count > 0 && firstPlaceText != null)
            firstPlaceText.text = "1. " + entries[0].playerName + " - " + FormatTime(entries[0].time);

        if (entries.Count > 1 && secondPlaceText != null)
            secondPlaceText.text = "2. " + entries[1].playerName + " - " + FormatTime(entries[1].time);

        if (entries.Count > 2 && thirdPlaceText != null)
            thirdPlaceText.text = "3. " + entries[2].playerName + " - " + FormatTime(entries[2].time);
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        int milliseconds = Mathf.FloorToInt((time * 1000f) % 1000f);

        return $"{minutes:00}:{seconds:00}.{milliseconds:000}";
    }

    private void SaveLeaderboard()
    {
        LeaderboardData data = new LeaderboardData
        {
            entries = entries
        };

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);

        Debug.Log("Leaderboard saved JSON: " + json);
    }

    private void LoadLeaderboard()
    {
        if (!File.Exists(savePath))
        {
            Debug.Log("Leaderboard file not found yet.");
            return;
        }

        string json = File.ReadAllText(savePath);
        Debug.Log("Leaderboard loaded JSON: " + json);

        LeaderboardData data = JsonUtility.FromJson<LeaderboardData>(json);

        if (data != null && data.entries != null)
            entries = data.entries.OrderBy(e => e.time).ToList();
    }
}

[System.Serializable]
public class LeaderboardEntry
{
    public string playerName;
    public float time;
}

[System.Serializable]
public class LeaderboardData
{
    public List<LeaderboardEntry> entries;
}