using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //class variables
    private AudioSource audioSource;
    public AudioClip easyLevelClip;
    public AudioClip mediumLevelClip;
    public AudioClip hardLevelClip;
    [SerializeField]
    private float bpm = 138;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        bpm = bpm / 60f;
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
