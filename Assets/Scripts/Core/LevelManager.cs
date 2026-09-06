using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public float CurrentLevelScore { get; private set; }
    public float CurrentFuel { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Đăng ký nhận sự kiện khi bắt đầu màn
        EventBus.Subscribe<OnCoinPickup>(OnCoinPickedUp);
        EventBus.Subscribe<OnFuelChangedEvent>(OnFuelChanged);
        EventBus.Subscribe<OnPlayerCrashEvent>(OnPlayerCrashed);
    }

    private void OnDestroy()
    {
        // Luôn huỷ đăng ký khi LevelManager bị destroy (ví dụ qua màn khác)
        EventBus.Unsubscribe<OnCoinPickup>(OnCoinPickedUp);
        EventBus.Unsubscribe<OnFuelChangedEvent>(OnFuelChanged);
        EventBus.Unsubscribe<OnPlayerCrashEvent>(OnPlayerCrashed);
    }

    private void OnPlayerCrashed(OnPlayerCrashEvent crashEvent)
    {
        if (UIManager.Instance != null)
        {
            UIManager.Instance.ShowGameOverPopup();
        }
    }

    private void OnCoinPickedUp(OnCoinPickup scorebonus)
    {
        CurrentLevelScore += scorebonus.CoinValue;
        
        // Cập nhật lên UI
        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateScoreText(CurrentLevelScore);
        }
    }

    private void OnFuelChanged(OnFuelChangedEvent fuelEvent)
    {
        CurrentFuel = fuelEvent.fuelAmount;
        
        // Cập nhật lên UI
        if (UIManager.Instance != null)
        {
            // Làm tròn số cho đẹp trên UI
            UIManager.Instance.UpdateFuelText(Mathf.Round(CurrentFuel));
        }
    }

    // Gọi hàm này khi người chơi hoàn thành màn chơi
    public void FinishLevel()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddToGlobalScore(CurrentLevelScore);
        }
    }
}
