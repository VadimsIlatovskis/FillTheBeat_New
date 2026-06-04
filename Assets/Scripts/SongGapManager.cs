using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SongGapManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip endingClip;
    public List<LyricGap> gaps;

    private int currentIndex = 0;
    private bool waitingForWord = false;

    void Start()
    {
        PlayCurrentClip();
    }

    void PlayCurrentClip()
    {
        if (currentIndex >= gaps.Count)
        {
            Debug.Log("Dziesma pabeigta!");
            return;
        }

        waitingForWord = false;
        audioSource.clip = gaps[currentIndex].beforeClip;
        audioSource.Play();

        StartCoroutine(WaitUntilClipEnds());
    }

    IEnumerator WaitUntilClipEnds()
    {
        yield return new WaitWhile(() => audioSource.isPlaying);
        waitingForWord = true;
        Debug.Log("Tagad jāiemet: " + gaps[currentIndex].correctWord);
    }

    public void SubmitWord(string thrownWord)
    {
        if (!waitingForWord) return;

        if (thrownWord == gaps[currentIndex].correctWord)
        {
            currentIndex++;

            if (currentIndex >= gaps.Count)
            {
                audioSource.clip = endingClip;
                audioSource.Play();
                Debug.Log("Atskaņo dziesmas beigas!");
                return;
            }

            PlayCurrentClip();
        }
        else
        {
            Debug.Log("Nepareizā nots!");
        }
    }
}

[System.Serializable]
public class LyricGap
{
    public AudioClip beforeClip;
    public string correctWord;
}