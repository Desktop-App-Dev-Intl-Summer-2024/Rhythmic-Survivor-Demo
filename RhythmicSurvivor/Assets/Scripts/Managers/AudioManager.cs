using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //class variables
    private AudioSource audioSource;
    [SerializeField] private AudioClip easyLevelClip;
    private float bpm = 138;
    private float time = 0;
    private bool hitBeat = false;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        bpm = 60f / bpm;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > bpm)
        {
            hitBeat = true;
            time = 0;
        }
        else
        {
            hitBeat = false;
        }
    }

    //public methods
    public void startGame()
    {
        time = 0;
        audioSource.Play();
    }
    public void pauseMusic()
    {
        audioSource.Pause();
    }
    public void resumeMusic()
    {
        audioSource.UnPause();
    }

    //getters
    public float getBPM()
    {
        return bpm;
    }

    public bool isHitBeat()
    {
        return hitBeat;
    }
}
