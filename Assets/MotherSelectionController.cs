using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CraftCore;

public class MotherSelectionController : MonoBehaviour {

    [SerializeField]
    GameObject boardPrefab;

    [SerializeField]
    List<MotherViewController> selectableMothers;

    [SerializeField]
    UIButton contructButton;

    MotherViewController currentSelected;

    MotherBoardsCombiner boardCombiner;

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
                                    { EnergyType.Empty,  EnergyType.Blue, EnergyType.Black, EnergyType.Empty},
                                    { EnergyType.Black, EnergyType.Red, EnergyType.Green, EnergyType.Black},
                                    { EnergyType.Empty, EnergyType.Empty, EnergyType.Empty, EnergyType.Empty} };
            return arr;
        }
        if(r == 1) {
            EnergyType[,] arr =   { { EnergyType.Empty,  EnergyType.Red, EnergyType.Empty, EnergyType.Empty},
                                     { EnergyType.Empty, EnergyType.Green, EnergyType.Blue, EnergyType.Blue},                                    
                                    { EnergyType.Empty, EnergyType.Red, EnergyType.Empty, EnergyType.Black},
                                     { EnergyType.Empty, EnergyType.Empty, EnergyType.Empty, EnergyType.Empty}};
            return arr;
        }
        if (r == 2)
        {
            EnergyType[,] arr =   { { EnergyType.Empty,  EnergyType.Empty, EnergyType.Empty, EnergyType.Empty},
                                     { EnergyType.Empty, EnergyType.Red, EnergyType.Empty, EnergyType.Empty},
                                    { EnergyType.Blue, EnergyType.Green, EnergyType.Green, EnergyType.Blue},
                                     { EnergyType.Empty, EnergyType.Empty, EnergyType.Empty, EnergyType.Empty}};
            return arr;
        }

        // never used
        EnergyType[,] arr1 =   { { EnergyType.Empty,  EnergyType.Empty, EnergyType.Empty, EnergyType.Empty},
                                     { EnergyType.Empty, EnergyType.Empty, EnergyType.Empty, EnergyType.Empty},
                                    { EnergyType.Empty, EnergyType.Empty, EnergyType.Empty, EnergyType.Empty},
                                     { EnergyType.Empty, EnergyType.Empty, EnergyType.Empty, EnergyType.Empty}};
        return arr1;
    }

    Motherboard getRandomMotherboard()
    {
        Motherboard board = new Motherboard(boardCombiner.Matrices[Random.Range(0, boardCombiner.Matrices.Count)]);
        board.Heat = 8;
        return board;
    }


    public void setup()
    {
        deselectAll();

        foreach (var m in selectableMothers)
        {
            m.setup(getRandomMotherboard());
        }

        contructButton.SetState(UIButtonColor.State.Disabled, true);
        contructButton.isEnabled = false;
    }

    // Use this for initialization
    void Start() {
        boardCombiner = new MotherBoardsCombiner();
        setup();

    }

    // Update is called once per frame
    void Update() {

    }

    public void onConstructPress()
    {
        GameObject boardPanel = NGUITools.AddChild(transform.parent.gameObject, boardPrefab);
        boardPanel.transform.position = new Vector3(0, 768, 0);

        GetComponent<UITweener>().PlayForward();
        boardPanel.GetComponent<UITweener>().PlayForward();

        BoardViewController boardController = boardPanel.GetComponent<BoardViewController>();
        // TODO call setup with the selected motherboard
        boardController.setup(currentSelected.getMotherboard());
    }

    public void onMotherPress(MotherViewController mother) {
        contructButton.SetState(UIButtonColor.State.Normal, false);
        contructButton.isEnabled = true;

        deselectAll();
        mother.setSelected(true);

        currentSelected = mother;
    }

}
