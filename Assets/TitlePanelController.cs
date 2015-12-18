using UnityEngine;
using System.Collections;

public class TitlePanelController : MonoBehaviour {

	[SerializeField]
	GameObject motherBoardSelection;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onNewGameStart()
	{
		GetComponent<UITweener>().PlayForward();
		
		motherBoardSelection.GetComponent<UITweener>().PlayForward();
        motherBoardSelection.GetComponent<MotherSelectionController>().setup();
    }
}
