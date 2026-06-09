using UnityEngine;

public class SaveButtonController : MonoBehaviour
{
    public LeaderboardManager leaderboardManager;

    [Header("Menus")]
    public GameObject endScoreMenu;
    public GameObject leaderboardPanel;

    public void SaveScore()
    {
        Debug.Log("SAVE BUTTON PRESSED");

        if (endScoreMenu != null)
        {
            Debug.Log("Closing EndScoreMenu");
            endScoreMenu.SetActive(false);
        }

        if (leaderboardPanel != null)
        {
            Debug.Log("Opening LeaderboardPanel");
            leaderboardPanel.SetActive(true);
        }

        if (leaderboardManager != null)
        {
            Debug.Log("Saving player...");
            leaderboardManager.SaveCurrentPlayer();
        }
    }
}