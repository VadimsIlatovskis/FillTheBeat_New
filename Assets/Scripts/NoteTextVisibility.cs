using Oculus.Interaction;
using UnityEngine;
using Oculus.Interaction.HandGrab;

public class NoteTextVisibility : MonoBehaviour
{
    public GameObject textObject;

    private HandGrabInteractable handGrab;
    private bool wasGrabbed;

    private void Awake()
    {
        handGrab = GetComponent<HandGrabInteractable>();
    }

    private void Update()
    {
        bool isGrabbed = handGrab.State == InteractableState.Select;

        if (isGrabbed && !wasGrabbed)
        {
            textObject.SetActive(false);
        }
        else if (!isGrabbed && wasGrabbed)
        {
            textObject.SetActive(true);
        }

        wasGrabbed = isGrabbed;
    }
}
