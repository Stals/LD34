using UnityEngine;
using System.Collections;
using CraftCore;

public class SlotViewController : MonoBehaviour {

	[SerializeField]
	GameObject bg;

	[SerializeField]
	GameObject ramSlotImage;

	[SerializeField]
	GameObject cpuSlotImage;

	[SerializeField]
	GameObject gpuSlotImage;

	[SerializeField]
	GameObject utiSlotImage;

    int x;
    int y;

    public void setup(EnergyType energyType, int _x, int _y)
	{
        x = _x;
        y = _y;

		switch (energyType) {

		case EnergyType.Empty:
			bg.SetActive(false);
			GetComponent<BoxCollider>().enabled = false;
			break;
		case EnergyType.Black:
			utiSlotImage.SetActive(true);
			break;
		case EnergyType.Red:
			gpuSlotImage.SetActive(true);
			break;
		case EnergyType.Green:
			ramSlotImage.SetActive(true);
			break;
		case EnergyType.Blue:
			cpuSlotImage.SetActive(true);
			break;
		}

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}


    void OnDrop(GameObject drag)
    {
        CardViewController cardView = drag.GetComponent<CardViewController>();
        if (!cardView) return;


        bool placed = Game.Instance.getManager().getBoardViewController().onCardPlaced(x, y, cardView.getCard(), cardView);

        if (placed) {
            GetComponent<BoxCollider>().enabled = false;
            cardView.disableTouch();
        }
    }
}
