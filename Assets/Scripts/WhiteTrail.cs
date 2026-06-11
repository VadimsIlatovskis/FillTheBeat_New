using UnityEngine;

public class WhiteTrail : MonoBehaviour
{
    private void Start()
    {
        TrailRenderer trail = gameObject.AddComponent<TrailRenderer>();

        trail.time = 0.5f;
        trail.startWidth = 0.02f;
        trail.endWidth = 0f;

        trail.material = new Material(Shader.Find("Sprites/Default"));
        trail.startColor = Color.white;
        trail.endColor = new Color(1f, 1f, 1f, 0f);

        trail.minVertexDistance = 0.05f;
    }
}