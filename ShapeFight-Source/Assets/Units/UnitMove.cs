using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class UnitMove : NetworkBehaviour {
    UnitTeam team;

    Rigidbody rb;

    public float acceleration;
    public float maxSpeed;
    public bool affectedBySand = true;

    Vector3 target;
    
    void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        this.team = this.GetComponent<UnitTeam>();
    }


    void FixedUpdate()
    {
        if (!isServer)
            return;

        if (target != Vector3.zero)
        {
            rb.AddForceAtPosition((target - transform.position).normalized * acceleration * Time.deltaTime,this.transform.position + Vector3.up / 2f);
            float maxSpeedToUse = maxSpeed;
            if (CheckInSand() && affectedBySand)
                maxSpeedToUse *= .25f;
            Vector3 groundVelocity = rb.velocity;
            groundVelocity.y = 0;
            groundVelocity = groundVelocity.normalized * Mathf.Min(groundVelocity.magnitude, maxSpeedToUse);

            rb.velocity = groundVelocity + Vector3.up * rb.velocity.y;
        }
    }

    public void SetTarget(Vector3 target)
    {
        this.target = target;
    }

    bool CheckInSand()
    {
        return (this.transform.position.y < .3f);
    }

    public void OnSelect()
    {
        this.team.ColorAsSelected();
    }

    public void OnDeselect()
    {
        this.team.ColorAsTeam();
    }

    public bool CheckSameTeam(TeamID teamID)
    {
        return (this.team.teamID == teamID);
    }
    //Vector2 randUnitCircle = Random.insideUnitCircle.normalized;
    //thisMove.SetTarget(Commander.inst.baseStructure.transform.position + new Vector3(randUnitCircle.x, 0, randUnitCircle.y) * 3);
}
