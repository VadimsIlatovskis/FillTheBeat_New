using UnityEngine;

public class NeonPulse : MonoBehaviour
{
    public Color neonColor = Color.cyan;
    public float minIntensity = 3f;
    public float maxIntensity = 10f;
    public float pulseSpeed = 2f;

    private Material mat;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        float intensity = Mathf.Lerp(
            minIntensity,
            maxIntensity,
            (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f
        );

        mat.SetColor("_EmissionColor", neonColor * intensity);
    }
}