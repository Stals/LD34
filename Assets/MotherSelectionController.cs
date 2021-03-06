﻿using UnityEngine;
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

	[SerializeField]
	UILabel serialNumberLabel;

    MotherViewController currentSelected;

    MotherBoardsCombiner boardCombiner;
    AchievementProvider provider;

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
        int maxCount = (int)(boardCombiner.Matrices.Count * Game.Instance.getPlayer().getBoardsAvalible());

        Motherboard board = new Motherboard(boardCombiner.Matrices[Random.Range(0, maxCount)]);
        board.Achievment = provider.allBonuses[Random.Range(0, provider.allBonuses.Count - 1)];
        board.Heat = 7;
        return board;
    }


    public void setup()
    {
		Game.Instance.getPlayer ().motherboardNumber += 1;
		serialNumberLabel.text = "#" + Game.Instance.getPlayer ().motherboardNumber.ToString ();

        foreach (var m in selectableMothers)
        {
            m.setup(getRandomMotherboard());
        }

        //unneeded because of the defailt motherboard
        //contructButton.SetState(UIButtonColor.State.Disabled, true);
        //contructButton.isEnabled = false;

        Game.Instance.musicManager.pickMotherMusic.Play();
        Game.Instance.musicManager.pickPlayMusic.Stop();
        Game.Instance.musicManager.finishPlayMusic.Stop();

		foreach (var m in selectableMothers) {
			var tween = m.GetComponent<UITweener>();
			tween.ResetToBeginning();
			tween.PlayForward();
		}

        
        deselectAll();
        //select first one
        selectableMothers[0].setSelected(true);
        onMotherPress(selectableMothers[0]);
    }

    // Use this for initialization
    void Start() {
        boardCombiner = new MotherBoardsCombiner();
        provider = AchievementProvider.load();
    }

    // Update is called once per frame
    void Update() {

    }

    public void onConstructPress()
    {
        GameObject boardPanel = NGUITools.AddChild(transform.parent.gameObject, boardPrefab);
        boardPanel.transform.position = new Vector3(0, 768, 0);

        GetComponent<UITweener>().PlayReverse();
        boardPanel.GetComponent<UITweener>().PlayForward();

        BoardViewController boardController = boardPanel.GetComponent<BoardViewController>();
        // TODO call setup with the selected motherboard
        boardController.setup(currentSelected.getMotherboard());
        Game.Instance.SoundPlayer.PlaySound("Music/Interaction/Vocal-begin-dev");
    }

    public void onMotherPress(MotherViewController mother) {
        contructButton.SetState(UIButtonColor.State.Normal, false);
        contructButton.isEnabled = true;

        deselectAll();
        mother.setSelected(true);

        currentSelected = mother;
    }

}
