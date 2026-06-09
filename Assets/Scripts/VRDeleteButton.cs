using UnityEngine;

public class VRDeleteButton : MonoBehaviour
{
    public VRNameInputManager inputManager;

    public void PressDelete()
    {
        if (inputManager != null)
        {
            inputManager.DeleteLetter();
        }
    }
}