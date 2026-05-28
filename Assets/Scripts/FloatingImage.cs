using UnityEngine;
using System.Collections.Generic;

public class FloatingImage : MonoBehaviour
{
    [Header("Settings")]
    public float speed = 80f;

    private RectTransform rectTransform;
    private Vector2 velocity;
    private float minX, maxX, minY, maxY;
    private Vector2 halfSize;

    private static List<FloatingImage> all = new List<FloatingImage>();

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        all.RemoveAll(item => item == null);
        all.Add(this);
    }

    void OnDestroy()
    {
        all.Remove(this);
    }

    void Start()
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        Vector3[] canvasCorners = new Vector3[4];
        canvas.GetComponent<RectTransform>().GetWorldCorners(canvasCorners);

        Vector3[] imageCorners = new Vector3[4];
        rectTransform.GetWorldCorners(imageCorners);
        halfSize = new Vector2(
            (imageCorners[2].x - imageCorners[0].x) * 0.5f,
            (imageCorners[2].y - imageCorners[0].y) * 0.5f
        );

        minX = canvasCorners[0].x + halfSize.x;
        maxX = canvasCorners[2].x - halfSize.x;
        minY = canvasCorners[0].y + halfSize.y;
        maxY = canvasCorners[2].y - halfSize.y;

        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        velocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * speed;
    }

    void Update()
    {
        Vector3 pos = rectTransform.position;
        pos.x += velocity.x * Time.deltaTime;
        pos.y += velocity.y * Time.deltaTime;
        rectTransform.position = pos;

        CheckEdges();
        CheckCollisions();
    }

    void CheckEdges()
    {
        Vector3 pos = rectTransform.position;
        bool bounced = false;

        if (pos.x < minX)
        {
            pos.x = minX;
            velocity.x = Mathf.Abs(velocity.x);
            bounced = true;
        }
        else if (pos.x > maxX)
        {
            pos.x = maxX;
            velocity.x = -Mathf.Abs(velocity.x);
            bounced = true;
        }

        if (pos.y < minY)
        {
            pos.y = minY;
            velocity.y = Mathf.Abs(velocity.y);
            bounced = true;
        }
        else if (pos.y > maxY)
        {
            pos.y = maxY;
            velocity.y = -Mathf.Abs(velocity.y);
            bounced = true;
        }

        if (bounced)
        {
            float randomAngle = Random.Range(-15f, 15f) * Mathf.Deg2Rad;
            float cos = Mathf.Cos(randomAngle);
            float sin = Mathf.Sin(randomAngle);
            velocity = new Vector2(
                velocity.x * cos - velocity.y * sin,
                velocity.x * sin + velocity.y * cos
            ).normalized * speed;
        }

        rectTransform.position = pos;
    }

    void CheckCollisions()
    {
        for (int i = all.Count - 1; i >= 0; i--)
        {
            if (all[i] == null)
            {
                all.RemoveAt(i);
                continue;
            }

            if (all[i] == this) continue;

            Vector2 diff = (Vector2)rectTransform.position - (Vector2)all[i].rectTransform.position;
            float dist = diff.magnitude;
            float minDist = halfSize.x + all[i].halfSize.x;

            if (dist < minDist && dist > 0.01f)
            {
                Vector2 normal = diff.normalized;
                velocity = Vector2.Reflect(velocity, normal).normalized * speed;
                rectTransform.position += (Vector3)(normal * (minDist - dist) * 0.5f);
            }
        }
    }
}