using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class HighScoreTracker : MonoBehaviour
{
    public static HighScoreTracker Instance;
    public int highScore;
    public string highScoreName;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        LoadHighScore();

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SaveHighScore()
    {
        HighScoreData data = new HighScoreData();
        data.highScore = highScore;
        data.highScoreName = highScoreName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/highscore.json", json);
    }

    private void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/highscore.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            HighScoreData data = JsonUtility.FromJson<HighScoreData>(json);

            highScore = data.highScore;
            highScoreName = data.highScoreName;
        }
    }

    [System.Serializable]
    class HighScoreData
    {
        public int highScore;
        public string highScoreName;
    }
}
