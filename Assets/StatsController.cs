using UnityEngine;
using System.Collections;

public class StatsController : MonoBehaviour {

    [SerializeField]
    UILabel redLabel;

    [SerializeField]
    UILabel blueLabel;

    [SerializeField]
    UILabel greenLabel;

    [SerializeField]
    UILabel energyLabel;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        var session = Game.Instance.getManager().getBoardViewController().session;
        if (session == null) return;

        redLabel.text = session.Board.Energy(CraftCore.EnergyType.Red).ToString();
        blueLabel.text = session.Board.Energy(CraftCore.EnergyType.Blue).ToString();
        greenLabel.text = session.Board.Energy(CraftCore.EnergyType.Green).ToString();

        energyLabel.text = session.Board.TotalHeat.ToString();
    }
}
