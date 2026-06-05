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

    [Header("End Effect")]
    public GameObject endEffect;

    private int currentIndex = 0;
    private bool waitingForWord = false;
    private bool gameStarted = false;

    void Start()
    {
        TurnOffAllEffects();

        if (endEffect != null)
            endEffect.SetActive(false);

        if (startMenu != null)
            startMenu.SetActive(true);
    }

    public void StartGame()
    {
        if (gameStarted)
            return;

        gameStarted = true;

        if (startMenu != null)
            startMenu.SetActive(false);

        PlayCurrentClip();
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

        if (endingClip != null)
        {
            audioSource.clip = endingClip;
            audioSource.Play();

            if (endEffect != null)
                endEffect.SetActive(true);

            Debug.Log("Dziesmas beigas!");
        }
        else
        {
            Debug.Log("EndingClip nav ielikts!");
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