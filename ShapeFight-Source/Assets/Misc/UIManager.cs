using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public static UIManager inst;

    public List<Image> shopIcons;
    public List<Text> shopCosts;
    public List<Text> shopDisplayNames;


    public Text moneyText;

    void Awake() {
        inst = this;
        this.gameObject.SetActive(false);
    }

    public void SetupPurchaseUI(ref List<Purchase> purchases){
        for (int index = 0; index < 6; index++)
        {
            shopIcons[index].sprite = purchases[index].icon;
            shopCosts[index].text = purchases[index].amount.ToString();
            shopDisplayNames[index].text = purchases[index].displayName;
        }
    }
    public void SetMoney(float money)
    {
        moneyText.text = Mathf.RoundToInt(money).ToString();
    }

    public void OpenUI ()
    {
        this.gameObject.SetActive(true);
    }

    public void CloseUI ()
    {
        this.gameObject.SetActive(false);
    }
}
