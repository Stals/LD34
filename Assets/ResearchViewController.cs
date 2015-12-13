using UnityEngine;
using System.Collections;

public class ResearchViewController : MonoBehaviour {

	[SerializeField]
	GameObject ResearchItemPrefab;

	// Use this for initialization
	void Start () {
		var upgrades = Game.Instance.getPlayer ().getStatUpgrades ();
		foreach (var upgrade in upgrades) {

			GameObject newItem = NGUITools.AddChild(gameObject, ResearchItemPrefab);
			newItem.GetComponent<ResearchItemViewController>().setup(upgrade);
		}

		GetComponent<UIGrid> ().Reposition ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
