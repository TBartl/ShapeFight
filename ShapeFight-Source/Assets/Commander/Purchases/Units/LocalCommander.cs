using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class LocalCommander : UnitTeam {
    public static LocalCommander inst;

    new void Awake()
    {
        base.Awake();

        this.teamID = (TeamID)Random.Range(0, 12);
    } 
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        if (isLocalPlayer)
            inst = this;
    }

    [Command]
    void CmdSetTeam(TeamID teamID)
    {        
        this.teamID = teamID;
    }
}
