using UnityEngine;

public class SocketEffect : MonoBehaviour
{
    public ParticleSystem effect;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Kaut kas ienāca socket triggerī: " + other.name);

        NoteObject note = other.GetComponentInParent<NoteObject>();

        if (note != null)
        {
            Debug.Log("Tā ir nots: " + note.name);

            if (effect != null)
                effect.Play();
            else
                Debug.LogError("Effect nav piesaistīts!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Kaut kas izgāja no socket triggera: " + other.name);

        NoteObject note = other.GetComponentInParent<NoteObject>();

        if (note != null)
        {
            if (effect != null)
                effect.Stop();
        }
    }
}