using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MusicLoop : MonoBehaviour
{
    public AudioSource musicSource1, musicSource2;
    public AudioClip musicStart;

    // Start is called before the first frame update
    void Start()
    {
        //musicSource.PlayOneShot(musicStart);
        //musicSource.PlayScheduled(AudioSettings.dspTime + musicStart.length);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            musicSource1.Stop();
            musicSource1.PlayOneShot(musicStart);
        }
    }
}
