using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioClip timeRunningOut;

    public List<AudioClip> roundEndClips;

    AudioSource audioSourceTimer;
    AudioSource audioSourceFX;

    private delegate void UpdateAction();
    private UpdateAction updateAction;

    bool timeOutTriggered = false;

    void Awake()
    {
        audioSourceTimer = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        audioSourceFX = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        audioSourceFX.volume = 0.7f;

        audioSourceFX.clip = roundEndClips[0];
        audioSourceFX.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        updateAction = DefaultUpdate;
    }

    // Update is called once per frame
    void Update()
    {
        updateAction();
    }

    void DefaultUpdate()
    {

    }

    void FadeInUpdate()
    {
        audioSourceTimer.volume += (Time.deltaTime * 0.5f);
        if (audioSourceTimer.volume >= 1)
        {
            updateAction = DefaultUpdate;
        }
    }

    void FadeOutUpdate()
    {
        audioSourceTimer.volume -= (Time.deltaTime * 0.5f);
        if (audioSourceTimer.volume <= 0)
        {
            audioSourceTimer.Stop();
            updateAction = DefaultUpdate;
        }
    }

    public void TimeRunningOut(float t)
    {
        if (t <= 10 && !timeOutTriggered)
        {
            audioSourceTimer.clip = timeRunningOut;
            FadeInTimeRunningOut();
            audioSourceTimer.Play();
            timeOutTriggered = true;
        }

        if (t > 10)
        {
            timeOutTriggered = false;
        }
    }

    public void FadeInTimeRunningOut()
    {
        audioSourceTimer.volume = 0;
        updateAction = FadeInUpdate;
    }

    public void FadeOutTimeRunningOut()
    {
        updateAction = FadeOutUpdate;
    }

    public void RoundEnd()
    {
        int choice = Random.Range(0, roundEndClips.Count);
        audioSourceFX.clip = roundEndClips[choice];
        audioSourceFX.Play();
    }
}
