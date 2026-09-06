using UnityEngine;

public class LanderScoring : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 1. Có phải landing pad không?
        if (!collision.gameObject.CompareTag("landing"))
        {
            Debug.Log("Crashed! Not on landing pad!");
            EventBus.Publish(new OnPlayerCrashEvent());
            return;
        }

        // 2. Lấy Landing component
        Landing landing = collision.gameObject.GetComponent<Landing>();

        if (landing == null)
        {
            Debug.Log("Crashed! No landing script!");
            EventBus.Publish(new OnPlayerCrashEvent());
            return;
        }

        // =========================
        // LANDING CONDITIONS
        // =========================

        // 3. Kiểm tra tốc độ
        float maxLandingSpeed = 3.5f;
        float speed = collision.relativeVelocity.magnitude;

        if (speed > maxLandingSpeed)
        {
            Debug.Log("Crashed! Too fast!");
            EventBus.Publish(new OnPlayerCrashEvent());
            return;
        }

        // 4. Kiểm tra góc
        float dotVector = Vector2.Dot(Vector2.up, transform.up);
        float minDot = 0.9f;

        if (dotVector < minDot)
        {
            Debug.Log("Crashed! Wrong angle!");
            EventBus.Publish(new OnPlayerCrashEvent());
            return;
        }

        // =========================
        // SCORE
        // =========================

        float score = 0f;

        // Speed: 40 points
        float speedScore =
            Mathf.InverseLerp(maxLandingSpeed, 0f, speed) * 40f;

        // Angle: 30 points
        float angleScore =
            Mathf.InverseLerp(minDot, 1f, dotVector) * 30f;

        // Position: 30 points
        Collider2D landingCollider =
            landing.GetComponent<Collider2D>();

        Bounds bounds = landingCollider.bounds;

        float distanceFromCenter =
            Mathf.Abs(transform.position.x - bounds.center.x);

        float maxDistance =
            bounds.extents.x;

        float positionScore =
            Mathf.InverseLerp(maxDistance, 0f, distanceFromCenter) * 30f;

        // Total base score
        score = speedScore + angleScore + positionScore;

        // Landing pad bonus
        score *= landing.GetLandingScoreBonus();

        Debug.Log(
            $"Landed!" +
            $" TOTAL: {score:F0}"
        );
    }
}
