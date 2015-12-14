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

    [SerializeField]
    InterpolatedProgressBar redBar;

    [SerializeField]
    InterpolatedProgressBar blueBar;

    [SerializeField]
    InterpolatedProgressBar greenBar;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        var session = Game.Instance.getManager().getBoardViewController().session;
        if (session == null) return;

        redLabel.text = session.Board.Energy(CraftCore.EnergyType.Red).ToString();
        blueLabel.text = session.Board.Energy(CraftCore.EnergyType.Blue).ToString();
        greenLabel.text = session.Board.Energy(CraftCore.EnergyType.Green).ToString();

        energyLabel.text = session.Board.TotalHeat.ToString();

        updateBars();
    }

    void updateBars()
    {
        var session = Game.Instance.getManager().getBoardViewController().session;
        if (session == null) return;

        int r = session.Board.Energy(CraftCore.EnergyType.Red);
        int b = session.Board.Energy(CraftCore.EnergyType.Blue);
        int g = session.Board.Energy(CraftCore.EnergyType.Green);

        int sum = r + b + g;

        if (sum != 0)
        {
            redBar.setValue((float)r / sum);
            greenBar.setValue((float)g / sum);
            blueBar.setValue((float)b / sum);
        }
        else {
            redBar.setValue(0.5f);
            greenBar.setValue(0.5f);
            blueBar.setValue(0.5f);
        }
    }

}
