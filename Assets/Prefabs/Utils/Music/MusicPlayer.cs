using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
    
    public AudioSource pickMotherMusic;
    public AudioSource pickPlayMusic;
    public AudioSource finishPlayMusic;


    // Use this for initialization
    void Awake () {
        Game.Instance.musicManager = this;

        pickMotherMusic.volume = 0.8f;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
