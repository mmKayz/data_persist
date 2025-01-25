using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{

    public static DataManager Instance;
    public string PlayerName;
    public string HighScoreName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        if(Instance.PlayerName == null) {
            Instance.PlayerName = "";
        }

        if(Instance.HighScoreName == null) {
            Instance.HighScoreName = "";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static int LoadHighScore() {
        string path = Application.persistentDataPath + "/savefile.json";
        if(File.Exists(path)) {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            Instance.HighScoreName = data.PlayerName;
            return data.HighScore;
        }
        return 0;
    }

    public static string GetHighScoreName() {
        string path = Application.persistentDataPath + "/savefile.json";
        if(File.Exists(path)) {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            return data.PlayerName;
        }
        return "";
    }

    public static void SaveHighScore(int highScore) {
        SaveData data = new SaveData();
        data.PlayerName = Instance.PlayerName;
        data.HighScore = highScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    [System.Serializable]
    class SaveData {
        public int HighScore;
        public string PlayerName;

    }
}
