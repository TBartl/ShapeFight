using UnityEngine;
using System.Collections;

public class CameraHolder : MonoBehaviour {
    
	// Update is called once per frame
	void Update ()
    {
        Camera.main.transform.position = this.transform.position;
        Camera.main.transform.rotation = this.transform.rotation;
    }
}
