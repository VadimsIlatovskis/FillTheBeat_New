using UnityEngine;

public class NoteSocket : MonoBehaviour
{
    [Header("Какую ноту принимает этот сокет")]
    public NoteObject acceptedNotePrefab;

    [Header("Точка установки ноты")]
    public Transform snapPoint;

    private bool occupied = false;

    private void OnTriggerEnter(Collider other)
    {
        if (occupied)
            return;

        NoteObject note = other.GetComponentInParent<NoteObject>();

        if (note == null || note.IsUsed())
            return;

        if (note.notePrefab == acceptedNotePrefab.gameObject)
        {
            if (snapPoint == null)
                snapPoint = transform;

            occupied = true;
            note.SnapToSocket(snapPoint);
        }
    }
}