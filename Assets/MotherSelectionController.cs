using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CraftCore;

public class MotherSelectionController : MonoBehaviour {

    [SerializeField]
    GameObject boardPanel;

    [SerializeField]
    List<MotherViewController> selectableMothers;

    [SerializeField]
    BoardViewController boardController;

    [SerializeField]
    UIButton contructButton;

    MotherViewController currentSelected;

    public void deselectAll()
    {
        foreach (var m in selectableMothers) {
            m.setSelected(false);
        }
    }

    EnergyType[,] getRandomMotherboardSetup()
    {
        int r = Random.Range(0, 3);

       
        if (r == 0)
        {
            EnergyType[,] arr =   { { EnergyType.Empty, EnergyType.Empty, EnergyType.Empty, EnergyType.Empty},
                                    { EnergyType.Blue,  EnergyType.Green, EnergyType.Black, EnergyType.Red},
                                    { EnergyType.Blue, EnergyType.Green, EnergyType.Black, EnergyType.Red},
                                    { EnergyType.Empty, EnergyType.Empty, EnergyType.Empty, EnergyType.Empty} };
            return arr;
        }
        if(r == 1) {
            EnergyType[,] arr =   { { EnergyType.Blue,  EnergyType.Green, EnergyType.Black, EnergyType.Red},
                                     { EnergyType.Empty, EnergyType.Empty, EnergyType.Empty, EnergyType.Empty},                                    
                                    { EnergyType.Empty, EnergyType.Empty, EnergyType.Empty, EnergyType.Empty},
                                     { EnergyType.Blue, EnergyType.Green, EnergyType.Black, EnergyType.Red}};
            return arr;
        }
        if (r == 2)
        {
            EnergyType[,] arr =   { { EnergyType.Blue, EnergyType.Blue, EnergyType.Empty, EnergyType.Empty},
                                    { EnergyType.Blue,  EnergyType.Blue, EnergyType.Empty, EnergyType.Empty},
                                    { EnergyType.Empty, EnergyType.Empty, EnergyType.Green, EnergyType.Green},
                                    { EnergyType.Empty, EnergyType.Empty, EnergyType.Green, EnergyType.Green} };
            return arr;
        }

        // never used
        EnergyType[,] arr1 =   { { EnergyType.Blue, EnergyType.Blue, EnergyType.Empty, EnergyType.Empty},
                                    { EnergyType.Blue,  EnergyType.Blue, EnergyType.Empty, EnergyType.Empty},
                                    { EnergyType.Empty, EnergyType.Empty, EnergyType.Green, EnergyType.Green},
                                    { EnergyType.Empty, EnergyType.Empty, EnergyType.Green, EnergyType.Green} };
        return arr1;
    }

    Motherboard getRandomMotherboard()
    {
        Motherboard board = new Motherboard(getRandomMotherboardSetup());
        board.Heat = 8;
        return board;
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

        contructButton.SetState(UIButtonColor.State.Disabled, true);
    }

    // Update is called once per frame
    void Update() {

    }

    public void onConstructPress()
    {
        GetComponent<UITweener>().PlayForward();
        boardPanel.GetComponent<UITweener>().PlayForward();

        // TODO call setup with the selected motherboard
        boardController.setup(currentSelected.getMotherboard());
    }

    public void onMotherPress(MotherViewController mother) {
        contructButton.SetState(UIButtonColor.State.Normal, false);

        deselectAll();
        mother.setSelected(true);

        currentSelected = mother;
    }

}
