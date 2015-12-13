using UnityEngine;
using System.Collections;
using CraftCore;
using System;

public class MotherViewController : MonoBehaviour {

    [SerializeField]
    UISprite onSelect;

    [SerializeField]
    UITable previewTiles;

    [SerializeField]
    GameObject tilePrefab;

    Motherboard motherboard;

    public void setSelected(bool selected) {
        onSelect.alpha = selected ? 255f : 0f;
    }

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void setup(Motherboard board)
    {
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
                        tileColor = Color.gray;
                        break;
                    case EnergyType.Red:
                        tileColor = Color.red;
                        break;
                    case EnergyType.Green:
                        tileColor = Color.green;
                        break;
                    case EnergyType.Blue:
                        tileColor = Color.blue;

                        break;
                }

                newTIle.GetComponent<UISprite>().color = tileColor;
            }
        }
    }

    public Motherboard getMotherboard()
    {
        return motherboard;
    }
}
