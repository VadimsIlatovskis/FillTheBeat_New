using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SongGapManager : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip endingClip;

    [Header("Song Gaps")]
    public List<LyricGap> gaps;

    [Header("UI")]
    public GameObject startMenu;
    public GameObject menuButtons;
    public GameObject leaderBoard;
    public GameObject howToPlay;
    public GameObject exitMenu;

    [Header("Leaderboard")]
    public LeaderboardManager leaderboardManager;
    public GameObject endScoreMenu;

    [Header("End Effect")]
    public GameObject endEffect;

    private int currentIndex = 0;
    private bool waitingForWord = false;
    private bool gameStarted = false;

    private float gameStartTime;
    private float finalTime;

    void Start()
    {
        TurnOffAllEffects();

        if (endEffect != null)
            endEffect.SetActive(false);

        if (endScoreMenu != null)
            endScoreMenu.SetActive(false);

        if (startMenu != null)
            startMenu.SetActive(true);

        if (menuButtons != null)
            menuButtons.SetActive(true);

        if (leaderBoard != null)
            leaderBoard.SetActive(false);

        if (howToPlay != null)
            howToPlay.SetActive(false);

        if (exitMenu != null)
            exitMenu.SetActive(false);
    }

    public void StartGame()
    {
        if (gameStarted)
            return;

        gameStarted = true;
        gameStartTime = Time.time;

        HideAllMenus();

        PlayCurrentClip();
    }

    public void ShowLeaderBoard()
    {
        if (startMenu != null)
            startMenu.SetActive(true);

        if (menuButtons != null)
            menuButtons.SetActive(false);

        if (leaderBoard != null)
            leaderBoard.SetActive(true);

        if (howToPlay != null)
            howToPlay.SetActive(false);

        if (exitMenu != null)
            exitMenu.SetActive(false);
    }

    public void ShowHowToPlay()
    {
        if (startMenu != null)
            startMenu.SetActive(true);

        if (menuButtons != null)
            menuButtons.SetActive(false);

        if (howToPlay != null)
            howToPlay.SetActive(true);

        if (leaderBoard != null)
            leaderBoard.SetActive(false);

        if (exitMenu != null)
            exitMenu.SetActive(false);
    }

    public void ShowExitMenu()
    {
        if (startMenu != null)
            startMenu.SetActive(true);

        if (menuButtons != null)
            menuButtons.SetActive(false);

        if (exitMenu != null)
            exitMenu.SetActive(true);

        if (leaderBoard != null)
            leaderBoard.SetActive(false);

        if (howToPlay != null)
            howToPlay.SetActive(false);
    }

    public void BackToMainMenu()
    {
        if (startMenu != null)
            startMenu.SetActive(true);

        if (menuButtons != null)
            menuButtons.SetActive(true);

        if (leaderBoard != null)
            leaderBoard.SetActive(false);

        if (howToPlay != null)
            howToPlay.SetActive(false);

        if (exitMenu != null)
            exitMenu.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exit game");
    }

    void HideAllMenus()
    {
        if (startMenu != null)
            startMenu.SetActive(false);

        if (menuButtons != null)
            menuButtons.SetActive(false);

        if (leaderBoard != null)
            leaderBoard.SetActive(false);

        if (howToPlay != null)
            howToPlay.SetActive(false);

        if (exitMenu != null)
            exitMenu.SetActive(false);

        if (endScoreMenu != null)
            endScoreMenu.SetActive(false);
    }

    void PlayCurrentClip()
    {
        if (currentIndex >= gaps.Count)
        {
            PlayEndingClip();
            return;
        }

        waitingForWord = false;
        TurnOffAllEffects();

        audioSource.clip = gaps[currentIndex].beforeClip;
        audioSource.Play();

        StartCoroutine(WaitUntilClipEnds());
    }

    IEnumerator WaitUntilClipEnds()
    {
        yield return new WaitWhile(() => audioSource.isPlaying);

        waitingForWord = true;
        ShowCurrentEffect();

        Debug.Log("Tagad jāiemet: " + gaps[currentIndex].correctWord);
    }

    public void SubmitWord(string thrownWord)
    {
        if (!gameStarted)
            return;

        if (!waitingForWord)
            return;

        if (thrownWord == gaps[currentIndex].correctWord)
        {
            HideCurrentEffect();

            currentIndex++;

            if (currentIndex >= gaps.Count)
            {
                PlayEndingClip();
                return;
            }

            PlayCurrentClip();
        }
        else
        {
            Debug.Log("Nepareizā nots!");
        }
    }

    void PlayEndingClip()
    {
        waitingForWord = false;
        TurnOffAllEffects();

        finalTime = Time.time - gameStartTime;

        if (leaderboardManager != null)
            leaderboardManager.SetCurrentTime(finalTime);

        if (endScoreMenu != null)
            endScoreMenu.SetActive(true);

        if (endingClip != null)
        {
            audioSource.clip = endingClip;
            audioSource.Play();

            if (endEffect != null)
                endEffect.SetActive(true);

            Debug.Log("Dziesmas beigas! Laiks: " + finalTime);
        }
        else
        {
            Debug.Log("EndingClip nav ielikts! Laiks: " + finalTime);
        }
    }

    void ShowCurrentEffect()
    {
        if (currentIndex < gaps.Count && gaps[currentIndex].dropEffect != null)
            gaps[currentIndex].dropEffect.SetActive(true);
    }

    void HideCurrentEffect()
    {
        if (currentIndex < gaps.Count && gaps[currentIndex].dropEffect != null)
            gaps[currentIndex].dropEffect.SetActive(false);
    }

    void TurnOffAllEffects()
    {
        foreach (LyricGap gap in gaps)
        {
            if (gap.dropEffect != null)
                gap.dropEffect.SetActive(false);
        }
    }
}

[System.Serializable]
public class LyricGap
{
    public AudioClip beforeClip;
    public string correctWord;
    public GameObject dropEffect;
}