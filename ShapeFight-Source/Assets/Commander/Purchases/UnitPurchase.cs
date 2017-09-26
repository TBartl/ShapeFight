using UnityEngine;
using System.Collections;

[CreateAssetMenu]
public class UnitPurchase : Purchase
{
    public UnitType type;
    public override void OnPurchase(Shop s)
    {
        s.CmdSpawn(type); 
    }
}
