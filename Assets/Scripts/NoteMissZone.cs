using UnityEngine;

public class NoteMissZone : MonoBehaviour
{
    [Header("Настройки")]
    public bool destroyOnlyOnTriggerExit = true;

    private void OnTriggerEnter(Collider other)
    {
        if (destroyOnlyOnTriggerExit)
            return;

        TryRespawn(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!destroyOnlyOnTriggerExit)
            return;

        TryRespawn(other);
    }

    private void TryRespawn(Collider other)
    {
        NoteObject note = other.GetComponentInParent<NoteObject>();

        if (note == null || note.IsUsed())
            return;

        note.DestroyAndRespawn();
    }
}