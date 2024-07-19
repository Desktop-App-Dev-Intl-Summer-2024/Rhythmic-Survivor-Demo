using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //class variables
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip easyLevelClip;
    [SerializeField]
    private float bpm = 138;
    private float time;
    private bool hitBeat;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        bpm = 60f / bpm;
        time = 0;
        hitBeat = false;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > bpm)
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
    public void startEasyLevelMusic()
    {
        audioSource.clip = easyLevelClip;
        audioSource.Play();
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
