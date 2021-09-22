using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class miniGame_AudioManager : MonoBehaviour {
    public AudioSource whoopie;

    public AudioReverbFilter reverb;


    public static miniGame_AudioManager S;

    void Awake() {
        if (S == null)
        {
            S = this;
        } 
    }
    
    // Start is called before the first frame update
    void Start() {
        //reverb = GetComponentInChildren<AudioReverbFilter>();
        reverb = whoopie.GetComponent<AudioReverbFilter>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameOverSound() {
        float r = Random.Range(.5f, 1f);
        int reverb_r = Random.Range(-1400, 0);

        reverb.dryLevel = reverb_r;
        
        whoopie.pitch = r;
        whoopie.Play();
    }
}
