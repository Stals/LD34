using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CraftCore;
using System;

public class MotherViewController : MonoBehaviour {

    [SerializeField]
    UISprite onSelect;

    [SerializeField]
    UITable previewTiles;

    [SerializeField]
    GameObject tilePrefab;

	[SerializeField]
	UILabel description;

	[SerializeField]
	UISprite bg;

	[SerializeField]
	UILabel nameLabel;

    Motherboard motherboard;
	List<string> names;

    public void setSelected(bool selected) {
        onSelect.alpha = selected ? 255f : 0f;
    }

    // Use this for initialization
    void Start() {
		names = new List<string>();
		names.Add("PRO");
		names.Add("PRO GAMING");
		names.Add("ULTRA");
		names.Add("DELUXE");
		names.Add("HYPE EDITION");
		names.Add("PORTABLE");
		names.Add("COSTLY ED.");
		names.Add("Bemtium");

    }

    // Update is called once per frame
    void Update() {

    }

    public void setup(Motherboard board)
    {
        NGUITools.DestroyChildren(previewTiles.transform);

        motherboard = board;

        for (int x = 0; x < Motherboard.xSize; ++x)
        {
            for (int y = 0; y < Motherboard.ySize; ++y)
            {
                EnergyType energyType = board.GetTyle(x, y);

                GameObject newTIle = NGUITools.AddChild(previewTiles.gameObject, tilePrefab);

                Color tileColor = Color.white;
                switch (energyType)
                {
                    case EnergyType.Empty:
                        tileColor = Color.white;
                        break;
                    case EnergyType.Black:
                        tileColor = new Color(59f/255f, 59f / 255f, 59f / 255f);
                        break;
                    case EnergyType.Red:
                        tileColor = new Color(149f / 255f, 110f / 255f, 44f / 255f);
                        break;
                    case EnergyType.Green:
                        tileColor = new Color(57f / 255f, 115f / 255f, 59f / 255f);
                        break;
                    case EnergyType.Blue:
                        tileColor = new Color(63f / 255f, 192f / 255f, 238f / 255f);

                        break;
                }

                newTIle.GetComponent<UISprite>().color = tileColor;
            }
        }
        previewTiles.Reposition();

		if (motherboard.Achievment != null) {
			description.text = Utils.getColorDescription (motherboard.Achievment.Descrition);
		} else {
			description.text = "";
		}

		bg.spriteName = "mbg" + UnityEngine.Random.Range(1, 4);
		bg.MarkAsChanged ();

		setupName ();
    }

    public Motherboard getMotherboard()
    {
        return motherboard;
    }

	public char GetLetter()
	{
		// This method returns a random lowercase letter.
		// ... Between 'a' and 'z' inclusize.
		int num = UnityEngine.Random.Range (0, 26); // Zero to 25
		char let = (char)('a' + num);
		return let;
	}

	void setupName()
	{
		string resultName = "" + GetLetter () + GetLetter ();
		resultName += "-";
		resultName += UnityEngine.Random.Range (100, 999);
		resultName += " ";
		resultName += names [UnityEngine.Random.Range (0, names.Count)];

		nameLabel.text = resultName;
	}

}
