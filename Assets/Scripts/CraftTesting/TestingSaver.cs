using UnityEngine;
using System.Collections;
using Newtonsoft.Json;
using CraftCore;

public class TestingSaver : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Card card = new Card();
        card.Type = EnergyType.Black;
        card.setLevelEnergy(1, 2, 3);
        string json = JsonConvert.SerializeObject(card);
        Debug.Log(json);

        EnergyType[,] arr = { {EnergyType.Empty, EnergyType.Empty, EnergyType.Empty, EnergyType.Empty},
        {EnergyType.Blue, EnergyType.Green, EnergyType.Black, EnergyType.Red},
        {EnergyType.Empty, EnergyType.Empty, EnergyType.Empty, EnergyType.Empty},
        {EnergyType.Empty, EnergyType.Empty, EnergyType.Empty, EnergyType.Empty}};
        Motherboard board = new Motherboard(arr);
        json = JsonConvert.SerializeObject(board);
        Debug.Log(json);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
