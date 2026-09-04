using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class Lander : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private float force = 700f;
    private bool isThrusting = false;
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    private void FixedUpdate()
    {
        isThrusting = false;
        if (Keyboard.current.upArrowKey.isPressed)
        {
            _rigidbody2D.AddForce(transform.up * force * Time.fixedDeltaTime);
            EventBus.Publish("MiddleThruster");
            isThrusting = true;
        }
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            _rigidbody2D.AddTorque(100 * Time.fixedDeltaTime);
            EventBus.Publish("LeftThruster");
            isThrusting = true;
        }
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            _rigidbody2D.AddTorque(-100 * Time.fixedDeltaTime);
            EventBus.Publish("RightThruster");
            isThrusting = true;
        }
        if (!isThrusting)
        {
            EventBus.Publish("NoThruster");
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
