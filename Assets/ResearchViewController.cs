using UnityEngine;
using System.Collections;

public class ResearchViewController : MonoBehaviour
{

    [SerializeField]
    GameObject ResearchItemPrefab;

    // Use this for initialization
    void Start()
    {
        var upgrades = Game.Instance.getPlayer().getStatUpgrades();
        foreach (var upgrade in upgrades)
        {

            GameObject newItem = NGUITools.AddChild(gameObject, ResearchItemPrefab);
            newItem.GetComponent<ResearchItemViewController>().setup(upgrade);
        }

        GetComponent<UIGrid>().Reposition();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onReserchPress()
    {
        UIToggle selectedToggle = UIToggle.GetActiveToggle(34);
        if (!selectedToggle) return;


        var itemController = selectedToggle.GetComponent<ResearchItemViewController>();
        if (itemController.upgrade.isEnabled) return;
        if (itemController.upgrade.cost > Game.Instance.getPlayer().getMoney()) return;

        Game.Instance.getPlayer().addMoney(-itemController.upgrade.cost);
        itemController.upgrade.isEnabled = true;
        itemController.setup(itemController.upgrade);
    }
}
