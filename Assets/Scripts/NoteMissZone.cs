/*using UnityEngine;

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
}*/


using UnityEngine;

public class NoteMissZone : MonoBehaviour
{
    [Header("Настройки")]
    public bool teleportOnlyOnTriggerExit = true;

    private void OnTriggerEnter(Collider other)
    {
        if (teleportOnlyOnTriggerExit)
            return;

        TeleportNote(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!teleportOnlyOnTriggerExit)
            return;

        TeleportNote(other);
    }

    private void TeleportNote(Collider other)
    {
        NoteObject note = other.GetComponentInParent<NoteObject>();

        if (note == null || note.IsUsed())
            return;

        if (note.respawnPoint == null)
            return;

        Rigidbody rb = note.GetComponent<Rigidbody>();

        // Сбрасываем физику
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // Телепортируем обратно
        note.transform.position = note.respawnPoint.position;
        note.transform.rotation = note.respawnPoint.rotation;
    }
}