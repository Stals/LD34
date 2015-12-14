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
    }

    public Motherboard getMotherboard()
    {
        return motherboard;
    }
}
