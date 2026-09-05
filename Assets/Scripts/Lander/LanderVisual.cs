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
        EventBus.Subscribe<OnLeftFlyEvent>(OnLeftFly);
        EventBus.Subscribe<OnRightFlyEvent>(OnRightFly);
        EventBus.Subscribe<OnMiddleFlyEvent>(OnMiddleFly);
        EventBus.Subscribe<OnNoFlyEvent>(OnNoFly);
    }

    private void OnDestroy()
    {
        if (lander != null)
        {
            EventBus.Unsubscribe<OnLeftFlyEvent>(OnLeftFly);
            EventBus.Unsubscribe<OnRightFlyEvent>(OnRightFly);
            EventBus.Unsubscribe<OnMiddleFlyEvent>(OnMiddleFly);
            EventBus.Unsubscribe<OnNoFlyEvent>(OnNoFly);
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
    private void OnLeftFly(OnLeftFlyEvent e)
    {
        SetEmissionModule(Leftthruster, false);
        SetEmissionModule(Rightthruster, true);
        SetEmissionModule(Middlethruster, false);
    }
    private void OnRightFly(OnRightFlyEvent e)
    {
        SetEmissionModule(Leftthruster, true);
        SetEmissionModule(Rightthruster, false);
        SetEmissionModule(Middlethruster, false);
    }
    private void OnMiddleFly(OnMiddleFlyEvent e)
    {
        SetEmissionModule(Leftthruster, true);
        SetEmissionModule(Rightthruster, true);
        SetEmissionModule(Middlethruster, true);
    }
    private void OnNoFly(OnNoFlyEvent e)
    {
        SetEmissionModule(Leftthruster, false);
        SetEmissionModule(Rightthruster, false);
        SetEmissionModule(Middlethruster, false);
    }
}