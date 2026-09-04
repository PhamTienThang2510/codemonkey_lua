using TMPro;
using UnityEngine;

public class Landing : MonoBehaviour
{
    [SerializeField]
    private int LandingScoreBonusMutipule = 3;
    private TextMeshPro landingScoreText;
    private void Awake()
    {
        landingScoreText = GetComponentInChildren<TextMeshPro>();
        landingScoreText.text = "x" + LandingScoreBonusMutipule.ToString();
    }
    public int GetLandingScoreBonus()
    {
        return LandingScoreBonusMutipule;
    }
}
