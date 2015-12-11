using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class HGLocalization {
	static Dictionary<string, string> loc;

	private static HGLocalization instance;
	private HGLocalization() {
		loc = new Dictionary<string, string>();
		parse ();
	}

	public static HGLocalization Instance
	{
		get 
		{
			if (instance == null)
			{
				instance = new HGLocalization();
			}
			return instance;
		}
	}

	void parse()
	{
		XmlDocument xmlDoc = new XmlDocument(); // xmlDoc is the new xml document.
		
		//xmlDoc.Load("Assets/data/buildings.xml"); // load the file.
		
		var PrnFile = Resources.Load("localization/rus") as TextAsset;
		xmlDoc.LoadXml(PrnFile.text); // load the file.


		XmlNode node = xmlDoc.GetElementsByTagName ("Localization") [0];

		XmlNodeList list = node.ChildNodes;
		foreach (XmlNode line in list) {
			parseLine(line);
		}
		
		//difficulties = getDifficultiesFromXML ();
		
		//List<Building> buildings = getBuildingsFromXML (xmlDoc.GetElementsByTagName ("Buildings") [0]);
	}

	private void parseLine(XmlNode node)
	{
		string tag = node.Attributes ["tag"].Value;
		string text = node.InnerText;

        loc [tag] = text;
	}

	public string getByTag(string tag){
		string outValue = "";
		bool found = loc.TryGetValue(tag, out outValue);

		if (found) {
			return outValue;
		} else {
			return "";		
		}
	}

}
