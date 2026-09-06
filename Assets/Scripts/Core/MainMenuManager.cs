using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text globalCoinText;

    private void Start()
    {
        // Hiển thị Global Score (Coin)
        // Nếu GameManager chưa khởi tạo (chạy thẳng từ màn hình chính), 
        // ta có thể gọi LoadGame() từ SaveManager để nạp điểm.
        
        SaveManager.LoadGame(); // Đọc điểm từ hệ thống lưu trữ

        if (GameManager.Instance != null && globalCoinText != null)
        {
            globalCoinText.text = "Global Coin: " + GameManager.Instance.GlobalScore.ToString();
        }
    }

    // Gắn hàm này vào sự kiện OnClick của nút Play
    public void PlayGame()
    {
        // Load Scene gameplay. Giả sử scene gameplay ở build index 1
        SceneManager.LoadScene(1);
    }
}
