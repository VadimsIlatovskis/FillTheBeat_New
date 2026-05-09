using UnityEngine;

public class NoteSocket : MonoBehaviour
{
    [Header("Какую ноту принимает")]
    public GameObject acceptedNotePrefab;

    [Header("Точка установки")]
    public Transform snapPoint;

    private bool occupied = false;

    private void OnTriggerEnter(Collider other)
    {
        if (occupied)
            return;

        NoteObject note = other.GetComponentInParent<NoteObject>();

        if (note == null || note.IsUsed())
            return;

        // Проверяем правильная ли нота
        if (note.notePrefab != acceptedNotePrefab)
            return;

        occupied = true;

        // Телепортируем ноту в центр сокета
        note.SnapToSocket(snapPoint);
    }
}