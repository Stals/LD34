using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
    
    public AudioSource pickMotherMusic;
    public AudioSource pickPlayMusic;
    public AudioSource finishPlayMusic;


    // Use this for initialization
    void Awake () {
        Game.Instance.musicManager = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
