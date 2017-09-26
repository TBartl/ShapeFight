using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Shop : NetworkBehaviour {
    UnitTeam team;

    float money;
    public float income;
    public List<Purchase> unitPurchases;
    public List<Purchase> upgradePurchases;

    void Awake()
    {
        this.team = this.GetComponent<UnitTeam>();
    }

    void Start()
    {
        if (!isLocalPlayer)
            return;

        UIManager.inst.OpenUI();
        UIManager.inst.SetupPurchaseUI(ref unitPurchases);
    }

	void Update ()
    {
        if (!isLocalPlayer)
            return;

        money += income * Time.deltaTime;
        UIManager.inst.SetMoney(money);

        if (Input.GetKeyDown(KeyCode.LeftShift))
            UIManager.inst.SetupPurchaseUI(ref upgradePurchases);
        if (Input.GetKeyUp(KeyCode.LeftShift))
            UIManager.inst.SetupPurchaseUI(ref unitPurchases);



        if (Input.GetKey(KeyCode.O) && Input.GetKey(KeyCode.P))
            money += 100 * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TryPurchase(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TryPurchase(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            TryPurchase(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            TryPurchase(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            TryPurchase(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            TryPurchase(5);
        }
    }

    void TryPurchase(int index)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (money >= upgradePurchases[index].amount)
            {
                upgradePurchases[index].OnPurchase(this);
                money -= upgradePurchases[index].amount;
            }
        }else
        {
            if (money >= unitPurchases[index].amount)
            {
                unitPurchases[index].OnPurchase(this);
                money -= unitPurchases[index].amount;
            }
        }
        
    }

    [Command]
    public void CmdSpawn(UnitType unitType)
    {
        GameObject temp = (GameObject)Instantiate(UnitDatabase.inst.Get(unitType),
            this.transform.position + Vector3.up * 3, Quaternion.identity);
        temp.GetComponent<UnitTeam>().teamID = this.team.teamID;

        Rigidbody tempRB = temp.GetComponent<Rigidbody>();
        if (tempRB != null)
            tempRB.velocity = Vector3.up * 4;
        UnitMove tempUnitMove = temp.GetComponent<UnitMove>();
        Vector2 randUnitCircle = Random.insideUnitCircle;
        Vector3 target = this.transform.position + this.transform.forward * 5f + new Vector3(randUnitCircle.x, 0, randUnitCircle.y) * 2.5f;
        if (tempUnitMove != null)
            tempUnitMove.SetTarget(target);


        NetworkServer.Spawn(temp);
    }
}
