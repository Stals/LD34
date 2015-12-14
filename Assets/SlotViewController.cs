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

    void PlayPlaced(EnergyType type)
    {
        switch (type)
        {
            case EnergyType.Black:
                Game.Instance.SoundPlayer.PlaySound("Music/Interaction/Place-Utility");
                break;
            case EnergyType.Red:
                Game.Instance.SoundPlayer.PlaySound("Music/Interaction/Place-GPU");
                break;
            case EnergyType.Green:
                Game.Instance.SoundPlayer.PlaySound("Music/Interaction/Place-RAM");
                break;
            case EnergyType.Blue:
                Game.Instance.SoundPlayer.PlaySound("Music/Interaction/Place-CPU");
                break;
        }
    }
    
    void OnDrop(GameObject drag)
    {
        CardViewController cardView = drag.GetComponent<CardViewController>();
        if (!cardView) return;


        bool placed = Game.Instance.getManager().getBoardViewController().onCardPlaced(x, y, cardView.getCard(), cardView);

        if (placed) {
            GetComponent<BoxCollider>().enabled = false;
            cardView.disableTouch();
            PlayPlaced(cardView.getCard().Type);
        } else
        {
            Game.Instance.SoundPlayer.PlaySound("Music/Interaction/Reject-Insert");
        }
    }
}
