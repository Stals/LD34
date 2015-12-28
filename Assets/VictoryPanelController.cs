using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CraftCore;
using System;
using DG.Tweening;

public class VictoryPanelController : MonoBehaviour {

    [SerializeField]
    UILabel resultScoreLabel;

    [SerializeField]
    UILabel newMoneyLabel;

    [SerializeField]
    List<GameObject> cardHolders;

    [SerializeField]
    GameObject cardPrefab;

    [SerializeField]
    MotherSelectionController boarSelectionController;

    [SerializeField]
    UILabel tipLabel;

    List<string> tips;
    GameSession session;

    [SerializeField]
    DG.Tweening.DOTweenAnimation moneyAnimation;

    [SerializeField]
    UILabel penaltyImbalanced;

    [SerializeField]
    UILabel penaltyZeroStat;

    // Use this for initialization
    void Start() {
        tips = new List<string>();
        tips.Add("Get an extra +1 if the color of a component and slot match");
        tips.Add("'Adj' means adjacent components in a cross pattern");
        tips.Add("Get a score of 45 to complete the game");
        tips.Add("Balance CPU, GPU and RAM to get more money");
        tips.Add("UTI cards don't cost anything to place");
        tips.Add("You will get a lower score if any stat is 0");
        tips.Add("Penalties are already deducted from the final score");

        moneyAnimation._tween.OnUpdate(moneyUpdateStep);
    }

    // Update is called once per frame
    void Update() {

    }

    void setupTip()
    {
        string text = "TIP: [000000][c]" + tips[UnityEngine.Random.Range(0, tips.Count)] + "[/c][-]";
        tipLabel.text = Utils.getColorDescription(text);
    }

    int convertScoreToMoney(float score) {
        return (int)(score * 4);
    }

    List<DeckCombiner.CardDublicating> shuffleCards(List<DeckCombiner.CardDublicating> alpha) {
        for (int i = 0; i < alpha.Count; i++)
        {
            DeckCombiner.CardDublicating temp = alpha[i];
            int randomIndex = UnityEngine.Random.Range(i, alpha.Count);
            alpha[i] = alpha[randomIndex];
            alpha[randomIndex] = temp;
        }
        return alpha;
    }

    public void moneyUpdateStep() {

        float step = moneyAnimation._tween.ElapsedPercentage();

        int reward = (int)((convertScoreToMoney(session.ResultScore())) * step);
        newMoneyLabel.text = "+" + reward.ToString() + "$";
    }

    public void updatePenalties()
    {
        // penalties
        penaltyZeroStat.alpha = 0f;
        penaltyImbalanced.alpha = 0f;

        int red = session.Board.Energy(CraftCore.EnergyType.Red);
        int green = session.Board.Energy(CraftCore.EnergyType.Green);
        int blue = session.Board.Energy(CraftCore.EnergyType.Blue);

        if (red == 0 ||
            green == 0 ||
            blue == 0)
        {
            penaltyZeroStat.alpha = 1f;
        }

        float perfectScore = (red + green + blue);
        float deltaScore = perfectScore - session.ResultScore();

        /*Debug.Log("perfectScore");
        Debug.Log(perfectScore);
        Debug.Log("deltaScore");
        Debug.Log(deltaScore);*/

        if (deltaScore > 0) {
            penaltyImbalanced.alpha = 1f;
            penaltyImbalanced.text = (-deltaScore).ToString("F1") + " for imbalanced stats";
        }

    }

    public void setup(GameSession _session)
    {
        session = _session;

        // clear previous
        foreach (var holder in cardHolders)
        {
            var card = holder.GetComponentInChildren<CardViewController>();
            if (card != null) {
                NGUITools.Destroy(card.gameObject);
            }
        }

        updatePenalties();



        resultScoreLabel.text = session.ResultScore().ToString("n2") + "!";

        int reward = convertScoreToMoney(session.ResultScore());
        newMoneyLabel.text = "+" + reward.ToString() + "$";
        Game.Instance.getPlayer().addMoney(reward);

        var cards = shuffleCards(Game.Instance.getDeckCombiner().Cards);

        int i = 0;
        foreach (var holder in cardHolders) {
            Card card = cards[i].card;

            var cardController = NGUITools.AddChild(holder, cardPrefab).GetComponent<CardViewController>();
            cardController.setup(card);
            cardController.disableTouch();

            ++i;
        }

        foreach (var holder in cardHolders)
        {
            UIButton buyButton = holder.GetComponentsInChildren<UIButton>(true)[0];
            buyButton.gameObject.SetActive(true);
        }

        updateButtons();
		setupTip ();

        Game.Instance.musicManager.pickMotherMusic.Stop();
        Game.Instance.musicManager.pickPlayMusic.Stop();
        Game.Instance.musicManager.finishPlayMusic.Play();

        moneyAnimation.DORestart();
        moneyAnimation.DOPlay();
    }

    void updateButtons()
    {
        foreach (var holder in cardHolders)
        {

            Card card = holder.GetComponentInChildren<CardViewController>().getCard();
            UIButton buyButton = holder.GetComponentInChildren<UIButton>();
            if (!buyButton) continue;

            // reset buttons
            buyButton.SetState(UIButtonColor.State.Normal, true);
            buyButton.isEnabled = true;

            if (card.UpgradeLevel == 2) { // is max
                buyButton.SetState(UIButtonColor.State.Disabled, true);
                buyButton.isEnabled = false;
                buyButton.GetComponentInChildren<UILabel>().text = "";
                continue;
            }

            int upgradeCost = getPriceForUpgrade(card.UpgradeLevel);
            if (Game.Instance.getPlayer().getMoney() < upgradeCost)
            {
                buyButton.SetState(UIButtonColor.State.Disabled, true);
                buyButton.isEnabled = false;
            }

            buyButton.GetComponentInChildren<UILabel>().text = upgradeCost + "$";
        }
    }


    int getPriceForUpgrade(int level)
    {
        if (level == 0)
        {
            return 50;
        }
        else {
            return 350;
        }
    }


    public void onPurchasePressed(GameObject cardHolder)
    {
        Card card = cardHolder.GetComponentInChildren<CardViewController>().getCard();
        int upgradeCost = getPriceForUpgrade(card.UpgradeLevel);
        if (Game.Instance.getPlayer().getMoney() >= upgradeCost)
        {
            cardHolder.GetComponentInChildren<CardViewController>().getCard().UpgradeLevel += 1;
            cardHolder.GetComponentInChildren<CardViewController>().setup(card);

            cardHolder.GetComponentInChildren<UIButton>().SetState(UIButtonColor.State.Disabled, true);            

            cardHolder.GetComponentInChildren<UIButton>().gameObject.SetActive(false);
            // TOdo update card visuals

            Game.Instance.getPlayer().addMoney(-upgradeCost);
            Game.Instance.SoundPlayer.PlaySound("Music/Interaction/Upgrade-click");
        }
        else {
            //TODO because buttons are disabled - impossible
        }

        updateButtons();
    }

    public void onContinuePressed() {
        GetComponent<UITweener>().PlayReverse();

        boarSelectionController.GetComponent<UITweener>().PlayForward();
        boarSelectionController.setup();
    }
}
