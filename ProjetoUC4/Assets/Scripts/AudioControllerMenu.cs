using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControllerMenu : MonoBehaviour
{
    public AudioSource audioSourceMusicBackground;

    public AudioClip[] musicsBackground;
    void Start()
    {
        int IndexMusicBackground = Random.Range(0, musicsBackground.Length);
        AudioClip musicBackgrundThisFase = musicsBackground[IndexMusicBackground];

        audioSourceMusicBackground.clip = musicBackgrundThisFase;
        audioSourceMusicBackground.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
