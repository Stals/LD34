using UnityEngine;
using System.Collections;
using CraftCore;

public class FinalVictoryController : MonoBehaviour {

	[SerializeField]
	VictoryPanelController victoryPanel;

    [SerializeField]
    UILabel resultScoreLabel;

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

        resultScoreLabel.text = session.ResultScore().ToString("n2") + "!";
    }

	public void onContinuePress()
	{
		victoryPanel.GetComponent<UITweener> ().PlayForward ();
		GetComponent<UITweener> ().PlayReverse ();
		
		victoryPanel.setup (session);
	}
}
