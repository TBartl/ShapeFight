using UnityEngine;
using System.Collections;

[System.Serializable]
public enum UpgradeType
{
    health, offense, mobility
}

[CreateAssetMenu]
public class UpgradePurchase : Purchase
{
    public UpgradeType type;

    public override void OnPurchase(Shop s)
    {

    }
}




