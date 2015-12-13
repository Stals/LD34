using UnityEngine;
using System.Collections;

public class MotherSelectionController : MonoBehaviour {

    [SerializeField]
    GameObject boardPanel;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void onConstructPress()
    {
        GetComponent<UITweener>().PlayForward();
        boardPanel.GetComponent<UITweener>().PlayForward();
    }

}
