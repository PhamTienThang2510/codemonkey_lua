using UnityEngine;

public class LanderVisual : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem Leftthruster;
    [SerializeField]
    private ParticleSystem Rightthruster;
    [SerializeField]
    private ParticleSystem Middlethruster;
    [SerializeField] private Lander lander;

    private void Awake()
    {
        if (lander == null)
        {
            lander = GetComponentInParent<Lander>();
        }
    }

    private void Start()
    {
        Init();
        EventBus.Subscribe("LeftThruster", OnLeftFly);
        EventBus.Subscribe("RightThruster", OnRightFly);
        EventBus.Subscribe("MiddleThruster", OnMiddleFly);
        EventBus.Subscribe("NoThruster", OnNoFly);
    }

    private void OnDestroy()
    {
        if (lander != null)
        {
            EventBus.Unsubscribe("LeftThruster", OnLeftFly);
            EventBus.Unsubscribe("RightThruster", OnRightFly);
            EventBus.Unsubscribe("MiddleThruster", OnMiddleFly);
            EventBus.Unsubscribe("NoThruster", OnNoFly);
        }
    }
    private void Init()
    {
        SetEmissionModule(Leftthruster, false);
        SetEmissionModule(Rightthruster, false);
        SetEmissionModule(Middlethruster, false);
    }
    private void SetEmissionModule(ParticleSystem particleSystem, bool isEnabled)
    {
        var emissionModule = particleSystem.emission;
        emissionModule.enabled = isEnabled;
    }
    private void OnLeftFly()
    {
        SetEmissionModule(Leftthruster, false);
        SetEmissionModule(Rightthruster, true);
        SetEmissionModule(Middlethruster, false);
    }
    private void OnRightFly()
    {
        SetEmissionModule(Leftthruster, true);
        SetEmissionModule(Rightthruster, false);
        SetEmissionModule(Middlethruster, false);
    }
    private void OnMiddleFly()
    {
        SetEmissionModule(Leftthruster, true);
        SetEmissionModule(Rightthruster, true);
        SetEmissionModule(Middlethruster, true);
    }
    private void OnNoFly()
    {
        SetEmissionModule(Leftthruster, false);
        SetEmissionModule(Rightthruster, false);
        SetEmissionModule(Middlethruster, false);
    }
}
