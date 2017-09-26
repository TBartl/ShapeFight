using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public enum TeamID
{
    red, blue, teal,
    purple, yellow, orange,
    green, pink, grey,
    lightBlue, darkGreen, brown
}



public class UnitTeam : NetworkBehaviour {

    [SyncVar] public TeamID teamID; 

    MeshRenderer meshRenderer;
    public MeshRenderer radarMeshRender;

	protected void Awake ()
    {
        meshRenderer = this.GetComponent<MeshRenderer>();
    }
    
    public override void OnStartClient()
    {
        base.OnStartClient();
        ColorRadar();
        ColorAsTeam();
    }


    public void ColorAsTeam()
    {
        meshRenderer.material = ColorManager.inst.Get(teamID);
    }
    public void ColorAsSelected()
    {
        meshRenderer.material = ColorManager.inst.selectedMat;
    }

    void ColorRadar()
    {
        radarMeshRender.gameObject.SetActive(true);
        radarMeshRender.material = ColorManager.inst.Get(teamID);
    }

}
