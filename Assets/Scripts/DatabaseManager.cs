using System.Collections.Generic;
using System.IO;
using System.Linq;
using SQLite;
using UnityEngine;

public class HighScore
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string PlayerName { get; set; }
    public int Score { get; set; }
    public float CompletionTime { get; set; }
}

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager Instance { get; private set; }

    private string dbPath;
    private SQLiteConnection dbConnection;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        dbPath = Path.Combine(Application.persistentDataPath, "gamedata.db");
        dbConnection = new SQLiteConnection(dbPath);

        dbConnection.CreateTable<HighScore>();

        Debug.Log("Database initialized at: " + dbPath);
    }

    public void SaveHighScore(string playerName, int score, float completionTime)
    {
        HighScore newScore = new HighScore
        {
            PlayerName = playerName,
            Score = score,
            CompletionTime = completionTime
        };

        dbConnection.Insert(newScore);
        Debug.Log("Saved Score: " + playerName + " - " + score);
    }

    public List<HighScore> GetTopHighScores(int count)
    {
        return dbConnection.Table<HighScore>()
            .OrderByDescending(s => s.Score)
            .Take(count)
            .ToList();
    }
}