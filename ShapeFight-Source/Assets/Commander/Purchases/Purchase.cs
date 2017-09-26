using UnityEngine;
using System.Collections;

[CreateAssetMenu]
public class Purchase : ScriptableObject {
    public Sprite icon;
    public int amount;
    public string displayName;

    public virtual void OnPurchase(Shop s)
    {

    }
}



