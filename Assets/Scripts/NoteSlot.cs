using UnityEngine;

public class NoteSlot : MonoBehaviour
{
    public SongGapManager songManager;

    private void OnTriggerEnter(Collider other)
    {
        NoteWord note = other.GetComponent<NoteWord>();

        if (note != null)
        {
            songManager.SubmitWord(note.word);
        }
    }
}
