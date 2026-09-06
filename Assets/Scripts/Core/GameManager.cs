using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    // Đổi tên biến thành GlobalScore để phân biệt rõ ràng
    public float GlobalScore { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // LevelManager sẽ gọi hàm này khi kết thúc màn chơi
    public void AddToGlobalScore(float levelScore)
    {
        GlobalScore += levelScore;
        Debug.Log("Global Score Updated: " + GlobalScore);
    }

    // SaveManager dùng hàm này để load điểm
    public void SetGlobalScore(float score)
    {
        GlobalScore = score;
    }
}
