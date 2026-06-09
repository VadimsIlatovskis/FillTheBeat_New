using UnityEngine;

public class VRLetterButton : MonoBehaviour
{
    public VRNameInputManager inputManager;
    public string letter = "A";

    public void PressLetter()
    {
        if (inputManager != null)
        {
            inputManager.AddLetter(letter);
        }
    }
}