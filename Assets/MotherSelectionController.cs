using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CraftCore;

public class MotherSelectionController : MonoBehaviour {

    [SerializeField]
    GameObject boardPanel;

    [SerializeField]
    List<MotherViewController> selectableMothers;

    public void deselectAll()
    {
        foreach (var m in selectableMothers) {
            m.setSelected(false);
        }
    }

    EnergyType[,] getRandomMotherboardSetup()
    {
        EnergyType[,] arr =  {      {EnergyType.Empty, EnergyType.Empty, EnergyType.Empty, EnergyType.Empty},
                                    {EnergyType.Blue,  EnergyType.Green, EnergyType.Black, EnergyType.Red},
                                    {EnergyType.Blue, EnergyType.Green, EnergyType.Black, EnergyType.Red},
                                    {EnergyType.Empty, EnergyType.Empty, EnergyType.Empty, EnergyType.Empty}};
        return arr;
    }

    Motherboard getRandomMotherboard()
    {
        return new Motherboard(getRandomMotherboardSetup());
    }


    void setup()
    {
        foreach (var m in selectableMothers)
        {
            m.setup(getRandomMotherboard());
        }       
    }


    // Use this for initialization
    void Start() {
        setup();
    }

    // Update is called once per frame
    void Update() {

    }

    public void onConstructPress()
    {
        GetComponent<UITweener>().PlayForward();
        boardPanel.GetComponent<UITweener>().PlayForward();
    }

    public void onMotherPress(MotherViewController mother) {
        deselectAll();
        mother.setSelected(true);
    }

}
