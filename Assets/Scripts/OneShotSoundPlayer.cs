using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class OneShotSoundPlayer : MonoBehaviour {

    void Awake()
    {
        GetComponent<AudioSource>().pitch = Time.timeScale;
        GetComponent<AudioSource>().Play();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
