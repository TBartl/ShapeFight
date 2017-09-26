using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class UnitHealth : NetworkBehaviour {
    public float health;
    public GameObject deathParticles;

    bool isApplicationQuitting = false; //Used to make sure we don't spawn 

    public void Damage(float damage)
    {
        health -= damage;
        if (health <= 0)
            NetworkServer.Destroy(this.gameObject);
    }
    void OnApplicationQuit()
    {
        isApplicationQuitting = true;
    }

    void OnDestroy()
    {
        if (deathParticles != null && isApplicationQuitting == false)
        {
            GameObject temp = (GameObject)Instantiate(deathParticles, this.transform.position, Quaternion.identity);
            ParticleSystemRenderer pr = temp.GetComponent<ParticleSystemRenderer>();
            pr.material = ColorManager.inst.Get(this.GetComponent<UnitTeam>().teamID);

        }
    }

}
