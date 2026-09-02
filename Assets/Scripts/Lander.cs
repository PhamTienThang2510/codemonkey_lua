using UnityEngine;
using UnityEngine.InputSystem;

public class Lander : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private float force = 700f;
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    private void FixedUpdate()
    {
        if (Keyboard.current.upArrowKey.isPressed)
        {
            rigidbody2D.AddForce(transform.up * force * Time.deltaTime);
        }
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            rigidbody2D.AddTorque(100 * Time.deltaTime);
        }
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            rigidbody2D.AddTorque(-100 * Time.deltaTime);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
