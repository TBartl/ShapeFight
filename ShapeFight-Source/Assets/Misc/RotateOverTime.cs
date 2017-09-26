using UnityEngine;
using System.Collections;

public class RotateOverTime : MonoBehaviour {
    public Vector3 rotateAmount;

    
	void Update () {
        this.transform.rotation *= Quaternion.Euler(rotateAmount * Time.deltaTime);	
	}
}
