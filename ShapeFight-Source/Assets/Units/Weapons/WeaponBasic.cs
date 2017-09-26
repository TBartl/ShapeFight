using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class WeaponBasic : NetworkBehaviour {
    UnitTeam team;

    public GameObject damageLine;
    public float damage;
    public float range;
    public float reloadTime;

    float timeUntilNextAttack;
    float checkAttackTime;

    void Awake()
    {
        team = GetComponent<UnitTeam>();
    }

	// Use this for initialization
	void Start () {
        if (!isServer)
            return;
        timeUntilNextAttack = reloadTime;
	
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isServer)
            return;


        timeUntilNextAttack -= Time.deltaTime;
        checkAttackTime -= Time.deltaTime;
        if (timeUntilNextAttack <= 0 && checkAttackTime <= 0)
        {
            checkAttackTime = .10f;

            float smallestDist = 9999;
            int closestUnit = -1;
            Collider[] colliders = Physics.OverlapSphere(this.transform.position, range, (1 << 9) + (1 << 10));
            for (int index = 0; index < colliders.Length; index++)
            {

                UnitTeam u = colliders[index].GetComponent<UnitTeam>();
                if (u.teamID != team.teamID)
                {
                    float thisDist = Vector3.Distance(this.transform.position, u.transform.position);
                    if (thisDist < smallestDist)
                    {
                        smallestDist = thisDist;
                        closestUnit = index;
                    }
                }
            }

            if (closestUnit != -1)
            {
                RpcDrawDamageLine(colliders[closestUnit].gameObject, colliders[closestUnit].transform.position);
                timeUntilNextAttack = reloadTime;
            }

        }
    }

    [ClientRpc]
    void RpcDrawDamageLine(GameObject g2, Vector3 v2)
    {
        GameObject temp = (GameObject)Instantiate(damageLine);
        DamageLine lineTemp = temp.GetComponent<DamageLine>();
        lineTemp.Setup(isServer, this.damage, this.gameObject, g2, this.transform.position, v2, this.team.teamID);
    }
}
