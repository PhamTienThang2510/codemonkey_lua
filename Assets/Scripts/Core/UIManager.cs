using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    // Sử dụng TMP_Text thay vì TextMeshPro để tương thích với cả 3D Text lẫn UI Canvas (TextMeshProUGUI)
    [SerializeField]
    private TMP_Text playerScore;
    
    [SerializeField]
    private TMP_Text Fuel;

    [SerializeField]
    private GameObject gameOverPanel; // Panel hiện lên khi thua

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        // Gán giá trị mặc định ban đầu
        UpdateScoreText(0);
        UpdateFuelText(0);
        
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false); // Ẩn popup khi mới vào game
        }
    }

    // Các hàm này sẽ được LevelManager gọi khi có dữ liệu mới
    public void UpdateScoreText(float score)
    {
        if (playerScore != null)
            playerScore.text = "Score: " + score.ToString();
    }

    public void UpdateFuelText(float fuelAmount)
    {
        if (Fuel != null)
            Fuel.text = "Fuel: " + fuelAmount.ToString();
    }

    // Gọi khi nhận event thua game
    public void ShowGameOverPopup()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }

    // Gắn vào nút "Play Again"
    public void RestartLevel()
    {
        // Cộng điểm màn chơi vào điểm tổng và lưu lại trước khi chơi lại
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.FinishLevel();
            SaveManager.SaveGame();
        }
        
        // Load lại Scene hiện tại
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Gắn vào nút "Main Menu"
    public void LoadMainMenu()
    {
        // Cộng điểm màn chơi vào điểm tổng và lưu lại trước khi thoát
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.FinishLevel();
            SaveManager.SaveGame();
        }
        
        // Giả sử Main Menu của bạn là Scene có index 0 hoặc tên "MainMenu"
        SceneManager.LoadScene(0); 
    }
}
