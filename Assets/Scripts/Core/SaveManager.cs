using UnityEngine;

// Đổi thành static class (không kế thừa MonoBehaviour) vì hệ thống save thường chỉ cần xử lý data độc lập
public static class SaveManager 
{
    public static void SaveGame()
    {
        if (GameManager.Instance != null)
        {
            float scoreToSave = GameManager.Instance.GlobalScore;
            PlayerPrefs.SetFloat("GlobalScore", scoreToSave);
            PlayerPrefs.Save();
            Debug.Log("Game Saved! Global Score: " + scoreToSave);
        }
    }

    public static void LoadGame()
    {
        if (GameManager.Instance != null)
        {
            float loadedScore = PlayerPrefs.GetFloat("GlobalScore", 0f);
            GameManager.Instance.SetGlobalScore(loadedScore);
            Debug.Log("Game Loaded! Global Score: " + loadedScore);
        }
    }
}
