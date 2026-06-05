using UnityEngine;

public class AudioReactiveParticles : MonoBehaviour
{
    public AudioSource audioSource;
    public ParticleSystem particles;

    [Header("Bass Reaction")]
    public float sensitivity = 80f;
    public float minEmission = 5f;
    public float maxEmission = 120f;
    public float minSpeed = 0.2f;
    public float maxSpeed = 3f;

    private float[] spectrum = new float[64];

    void Update()
    {
        if (audioSource == null || particles == null)
            return;

        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);

        float bass = 0f;

        // zemās frekvences / bass
        for (int i = 0; i < 8; i++)
        {
            bass += spectrum[i];
        }

        bass *= sensitivity;
        bass = Mathf.Clamp01(bass);

        var emission = particles.emission;
        emission.rateOverTime = Mathf.Lerp(minEmission, maxEmission, bass);

        var main = particles.main;
        main.startSpeed = Mathf.Lerp(minSpeed, maxSpeed, bass);
    }
}