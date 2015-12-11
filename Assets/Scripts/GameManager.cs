using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.ImageEffects;
using InputHelp;

public enum LevelState{
	Running,
	Ended,
    Rewinding
}

public class GameManager : MonoBehaviour {

    [SerializeField]
	CameraShake cameraShake;

	// Use this for initialization
	void Start () {
        Application.runInBackground = true;
		Game.Instance.init (this);

		cameraShake = Camera.main.gameObject.AddComponent<CameraShake>() as CameraShake;
	}
	
	public void shake()
	{
		cameraShake.Shake (0.1f, 0.02f);
	}
}
