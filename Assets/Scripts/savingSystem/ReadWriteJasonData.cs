using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class ReadWriteJasonData : MonoBehaviour
{
    [SerializeField] private gameSession gameSession;
    playerData playerData;

    private string filePath;

    private void Start()
    {
        filePath = Application.dataPath + "/playerData.json";
        Debug.Log("File will be saved at: " + filePath);

    }

    public void SaveToJson()
    {
        playerData.playerLives = gameSession.GetLives();
        playerData.levelNumber = SceneManager.GetActiveScene().buildIndex;
        playerData.score = gameSession.GetScore();

        // Serialize and save to file
        string json = JsonUtility.ToJson(playerData, true);
        File.WriteAllText(filePath, json);

        Debug.Log("Game data saved to " + filePath);
    }

    public void LoadFromJson()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            playerData = JsonUtility.FromJson<playerData>(json);

            // Load data into gameSession
            gameSession.SetGameSessionData(playerData.levelNumber, playerData.playerLives, playerData.score);

            Debug.Log("Game data loaded from " + filePath);
        }
        else
        {
            Debug.LogWarning("Save file not found at " + filePath);
        }
    }

    public void CloseGame()
    {
        Application.Quit();
        Debug.Log("App Closed!");
    }
}