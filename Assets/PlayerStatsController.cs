using UnityEngine;
using System.Collections;

public class PlayerStatsController : MonoBehaviour {

    [SerializeField]
    UILabel moneyLabel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        moneyLabel.text = Game.Instance.getPlayer().getMoney().ToString();
    }
}
