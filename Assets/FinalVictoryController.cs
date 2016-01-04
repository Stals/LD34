using UnityEngine;
using System.Collections;
using CraftCore;

public class FinalVictoryController : MonoBehaviour {

	[SerializeField]
	VictoryPanelController victoryPanel;

    [SerializeField]
    UILabel resultScoreLabel;

	[SerializeField]
	UILabel serialVictory;

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

		serialVictory.text = "It took you [b][c][7D7157]" + Game.Instance.getPlayer().motherboardNumber + "![-][/c][/b] motherboards to beat the game";
    }

	public void onContinuePress()
	{
		victoryPanel.GetComponent<UITweener> ().PlayForward ();
		GetComponent<UITweener> ().PlayReverse ();
		
		victoryPanel.setup (session);
	}
}
