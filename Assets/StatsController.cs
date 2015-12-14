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
        float avg = (float)sum / 3.0f;

        float da = Mathf.Abs(r - avg) + Mathf.Abs(g - avg) + Mathf.Abs(b - avg);

        if (sum != 0)
        {
            redBar.setValue((0.5f + (((float)r - avg) / da) * 0.5f) * 0.5f + 0.5f * ((r * 1.5f) / sum));
            greenBar.setValue((0.5f + (((float)g - avg) / da) * 0.5f) * 0.5f + 0.5f * ((g * 1.5f) / sum));
            blueBar.setValue((0.5f + (((float)b - avg) / da) * 0.5f) * 0.5f + 0.5f* ((b*1.5f)/ sum));
        }
        else {
            redBar.setValue(0.5f);
            greenBar.setValue(0.5f);
            blueBar.setValue(0.5f);
        }
    }

}
