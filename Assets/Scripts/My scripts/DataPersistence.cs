using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;



public class DataPersistence : MonoBehaviour
{
    public static DataPersistence Instance;

    public string playerName;

    //public Dictionary<string, List<int>> bestScores;
    public List<PlayerBestScores> bestScores;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadPlayerName();
        LoadBestScores();
    }
    

    [System.Serializable]
    class SaveData
    {
        public string playerName;
        
    }

    public void SavePlayerName()
    {
        SaveData data = new SaveData();
        data.playerName = playerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/players.json", json);
    }

    public void LoadPlayerName()
    {
        string path = Application.persistentDataPath + "/players.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerName = data.playerName;
        }
    }

    [System.Serializable]
    class SaveBestScoresData
    {

        //public Dictionary<string, List<int>> bestScores;
        public List<PlayerBestScores> bestScores;
    }

    [System.Serializable]
    public class PlayerBestScores
    {
        public string playerName;
        public int scores;
    }

    public void SaveBestScores(int scores) {
        if (bestScores == null) {
            bestScores = new List<PlayerBestScores>();
        }

        PlayerBestScores playerBestScores;

        if (bestScores.Count < 5)
        {
            playerBestScores = new PlayerBestScores();

            playerBestScores.playerName = playerName;
            playerBestScores.scores = scores;

            bestScores.Add(playerBestScores);
        }
        else 
        {
            int minScores = scores;
            int minScoresIdx = -1;

            for (int i = 0; i < bestScores.Count; i++) {
                playerBestScores = bestScores[i];

                if (playerBestScores.scores < scores && minScores > playerBestScores.scores) {
                    minScores = playerBestScores.scores;
                    minScoresIdx = i;
                }
            }

            foreach (var s in bestScores)
            {
                Debug.Log("player name = " + s.playerName + " scores=" + s.scores);
            }

            playerBestScores = new PlayerBestScores();

            playerBestScores.playerName = playerName;
            playerBestScores.scores = scores;

            if (minScoresIdx != -1) {
                bestScores[minScoresIdx] = playerBestScores;
            }
        }
    }

    public void PersistBestScores()
    {
        //Debug.Log("SaveBestScores Invoked!");

        //if (bestScores == null) {
        //    bestScores = new Dictionary<string, List<int>>();
        //}

        //if (bestScores.ContainsKey(playerName))
        //{
        //    List<int> currentPlayerBestScores;
        //    bool hasValue = bestScores.TryGetValue(playerName, out currentPlayerBestScores);

        //    if (hasValue)
        //    {
        //        currentPlayerBestScores.Add(scores);
        //    }
        //}
        //else {
        //    List<int> currentPlayerBestScores = new List<int>();
        //    currentPlayerBestScores.Add(scores);
        //    bestScores.Add(playerName, currentPlayerBestScores);
        //}

        SaveBestScoresData data = new SaveBestScoresData();
        data.bestScores = bestScores;

        string json = JsonUtility.ToJson(data);



        File.WriteAllText(Application.persistentDataPath + "/bestScores.json", json);
    }

    public void LoadBestScores()
    {
        string path = Application.persistentDataPath + "/bestScores.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveBestScoresData data = JsonUtility.FromJson<SaveBestScoresData>(json);

            bestScores = data.bestScores;
        }
    }
}
