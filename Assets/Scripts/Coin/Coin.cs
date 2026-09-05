using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float coinValue = 10f; // Giá trị của đồng xu

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra xem đối tượng chạm vào có component Lander không (tức là người chơi)
        if (collision.TryGetComponent<Lander>(out Lander lander))
        {
            // Phát sự kiện nhặt xu kèm theo giá trị của đồng xu
            EventBus.Publish(new OnCoinPickup(coinValue));

            // Tự tiêu hủy đồng xu sau khi được nhặt
            Destroy(gameObject);
        }
    }
}
