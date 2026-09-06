using UnityEngine;

public struct FuelPadTriggerEvent
{
    public float fuelAmount;
    public FuelPadTriggerEvent(float amount)
    {
        fuelAmount = amount;
    }
}
public struct  OnCoinPickup
{
    public float CoinValue;
    public OnCoinPickup(float value)
    {
        CoinValue = value;
    }
}
public struct OnLeftFlyEvent
{
}
public struct OnRightFlyEvent
{
}
public struct OnMiddleFlyEvent
{
}
public struct OnNoFlyEvent
{
}
public struct OnPlayerCrashEvent
{
}
public struct OnFuelChangedEvent
{
    public float fuelAmount;
    public OnFuelChangedEvent(float amount)
    {
        fuelAmount = amount;
    }
}