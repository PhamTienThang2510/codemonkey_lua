using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class Lander : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private float force = 700f;
    private float fuelAmount = 10f;
    private float Coin = 0f;
    private bool isThrusting = false;
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventBus.Subscribe<FuelPadTriggerEvent>(OnFuelPadTrigger);
        EventBus.Subscribe<OnCoinPickup>(OnCoinPickup);
    }

    private void FixedUpdate()
    {
        isThrusting = false;
        if (fuelAmount <= 0)
        {
            EventBus.Publish(new OnNoFlyEvent());
            return;
        }
        if (Keyboard.current.upArrowKey.isPressed)
        {
            _rigidbody2D.AddForce(transform.up * force * Time.fixedDeltaTime);
            EventBus.Publish(new OnMiddleFlyEvent());
            isThrusting = true;
        }
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            _rigidbody2D.AddTorque(100 * Time.fixedDeltaTime);
            EventBus.Publish(new OnLeftFlyEvent());
            isThrusting = true;
        }
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            _rigidbody2D.AddTorque(-100 * Time.fixedDeltaTime);
            EventBus.Publish(new OnRightFlyEvent());
            isThrusting = true;
        }
        UseFuelToMove();
        if (!isThrusting)
        {
            EventBus.Publish(new OnNoFlyEvent());
        }
    }
    private void OnDestroy()
    {
        EventBus.Unsubscribe<FuelPadTriggerEvent>(OnFuelPadTrigger);
        EventBus.Unsubscribe<OnCoinPickup>(OnCoinPickup);
    }

    private void UseFuelToMove()
    {
        if (isThrusting)
        {
            this.fuelAmount -= 2f * Time.fixedDeltaTime;
        }
    }
    private void OnFuelPadTrigger(FuelPadTriggerEvent e)
    {
        this.fuelAmount += e.fuelAmount;
    }
    private void OnCoinPickup(OnCoinPickup e)
    {
        this.Coin += e.CoinValue;
    }
}
