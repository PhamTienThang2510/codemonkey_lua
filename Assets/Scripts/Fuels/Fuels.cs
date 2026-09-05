using UnityEngine;

public class Fuels : MonoBehaviour
{
    [SerializeField] private float fuelAmount = 5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra xem đối tượng chạm vào có phải là Lander (người chơi) không
        if (collision.TryGetComponent<Lander>(out Lander lander))
        {
            Debug.Log("Entered fuel pad trigger!");
            
            // Phát sự kiện cộng xăng
            EventBus.Publish(new FuelPadTriggerEvent { fuelAmount = this.fuelAmount });
            
            // Tự tắt chính nó sau khi được ăn
            gameObject.SetActive(false);
        }
    }
}
