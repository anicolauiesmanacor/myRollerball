using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    private bool isFXEnabled = true;
    private bool isMusicEnabled = true;
    public AudioSource asMusic;
    public AudioSource asFX;
    public AudioClip gameMusic;
    public AudioClip item;
    public AudioClip winner;
    public AudioClip loser;
    public AudioClip jump;

    void Awake() {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("SoundManager");
        if (objs.Length > 1)
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlayFX(int i) {
        asFX.enabled = true;
        asFX.loop = false;
        
        switch(i) {
            case 0:
                asFX.clip = item;
                break;
            case 1:
                asFX.clip = winner;
                break;
            case 2:
                asFX.clip = loser;
                break;
            case 3:
                asFX.clip = jump;
                break;
        }

        if (isFXEnabled)
            asFX.Play();
    }

    public void PlayMusic() {
        asMusic.enabled = true;
        asMusic.loop = true;
        asMusic.clip = gameMusic;

        if (isMusicEnabled)
            asMusic.Play();
    }
}
