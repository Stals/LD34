using UnityEngine;
using System.Collections;

public class CardViewController : MonoBehaviour {

    public void onPress()
    {
        Game.Instance.getManager().getBoardViewController().onCardPress(this);
    }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
