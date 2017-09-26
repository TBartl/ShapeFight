using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class DamageLine : MonoBehaviour {

    float damage;

    bool isServer;
    GameObject g1;
    GameObject g2;
    Vector3 v1;
    Vector3 v2;
    TeamID teamID;

    public float travelSpeed;
    public GameObject hitParticle;
    public AnimationCurve curve;


    // Use this for initialization
    void Start () {
        MeshRenderer mr = this.GetComponent<MeshRenderer>();
        mr.material = ColorManager.inst.Get(teamID);

        this.transform.position = v1;
        this.transform.LookAt(v2);


        StartCoroutine(Run());


    }

    public void Setup(bool isServer, float damage, GameObject g1, GameObject g2, Vector3 v1, Vector3 v2, TeamID teamID)
    {
        this.isServer = isServer;
        this.damage = damage;
        this.g1 = g1;
        this.g2 = g2;
        this.v1 = v1;
        this.v2 = v2;
        this.teamID = teamID;

    }

    IEnumerator Run()
    {
        for (float val = 0; val < 1; val+= travelSpeed * Time.deltaTime)
        {
            this.transform.position = Vector3.Lerp(GetFirstTarget(), GetSecondTarget(), curve.Evaluate(val));
            this.transform.LookAt(v2);

            yield return null;
        }
        Instantiate(hitParticle, v2, Quaternion.identity);
        if (isServer)
        {
            if (g2)
                g2.GetComponent<UnitHealth>().Damage(this.damage);
        }
        Destroy(this.gameObject);
    }

    Vector3 GetFirstTarget()
    {
        if (g1)
            v1 = g1.transform.position;
        return v1;
    }

    Vector3 GetSecondTarget()
    {
        if (g2)
            v2 = g2.transform.position;
        return v2;
    }

    
}
