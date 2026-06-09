using UnityEngine;
using TMPro;

public class VRNameInputManager : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text nameDisplayText;

    [Header("Settings")]
    public int maxLetters = 12;

    private string currentName = "";

    private void Start()
    {
        UpdateDisplay();
    }

    public void AddLetter(string letter)
    {
        if (currentName.Length >= maxLetters)
            return;

        currentName += letter;
        UpdateDisplay();
    }

    public void DeleteLetter()
    {
        if (currentName.Length <= 0)
            return;

        currentName = currentName.Substring(0, currentName.Length - 1);
        UpdateDisplay();
    }

    public void ClearName()
    {
        currentName = "";
        UpdateDisplay();
    }

    public string GetName()
    {
        if (string.IsNullOrWhiteSpace(currentName))
            return "Player";

        return currentName;
    }

    private void UpdateDisplay()
    {
        if (nameDisplayText != null)
        {
            if (string.IsNullOrEmpty(currentName))
                nameDisplayText.text = "ENTER NAME";
            else
                nameDisplayText.text = currentName;
        }
    }
}