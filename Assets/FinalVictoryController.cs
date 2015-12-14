using UnityEngine;
using System.Collections;
using CraftCore;

public class FinalVictoryController : MonoBehaviour {

	[SerializeField]
	VictoryPanelController victoryPanel;

	GameSession session;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setup(GameSession _session)
	{
		session = _session;
	}

	public void onContinuePress()
	{
		victoryPanel.GetComponent<UITweener> ().PlayForward ();
		GetComponent<UITweener> ().PlayReverse ();
		
		victoryPanel.setup (session);
	}
}
