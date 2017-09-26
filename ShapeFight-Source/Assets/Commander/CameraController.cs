using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[System.Serializable]
public struct CamMove
{
    public Vector2 clamp;
    public float sensitivity;
}

[System.Serializable]
public struct CamRotation
{
    public float x;
    public float y;
    public Vector2 xClamp;
    public float sensitivity;
}

[System.Serializable]
public struct CamScale
{
    public float val;
    public Vector2 clamp;
    public float sensitivity;
}

public class CameraController :  NetworkBehaviour {
    public CamMove move;
    public CamRotation rotation;
    public CamScale scale;
    public Transform cameraPivot;
    public CameraHolder cameraHolder;


	void Start ()
    {
        if (!isLocalPlayer)            
            return;

        cameraPivot.rotation = Quaternion.Euler(rotation.x, rotation.y, 0);
        cameraPivot.localScale = Vector3.one * scale.val;
        cameraHolder.enabled = true;
    }

    void Update() {
        if (!isLocalPlayer)
            return;

        Vector3 inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        inputDirection = Quaternion.Euler(0, rotation.y, 0) * inputDirection;
        cameraPivot.position += inputDirection * move.sensitivity * Time.deltaTime * GetMultiplier();
        cameraPivot.position = new Vector3(
            Mathf.Clamp(cameraPivot.position.x, move.clamp.x, move.clamp.y),
            0,
            Mathf.Clamp(cameraPivot.position.z, move.clamp.x, move.clamp.y));

        if (Input.GetMouseButton(2)) { 
            rotation.x -= Input.GetAxisRaw("Mouse Y") * rotation.sensitivity * Time.deltaTime * GetMultiplier();
            rotation.x = Mathf.Clamp(rotation.x, rotation.xClamp.x, rotation.xClamp.y);

            rotation.y += Input.GetAxisRaw("Mouse X") * rotation.sensitivity * Time.deltaTime * GetMultiplier();

            cameraPivot.rotation = Quaternion.Euler(rotation.x, rotation.y, 0);
        }

        scale.val -= Input.GetAxis("Mouse ScrollWheel") * scale.sensitivity * Time.deltaTime * GetMultiplier();
        scale.val = Mathf.Clamp(scale.val, scale.clamp.x, scale.clamp.y);
        cameraPivot.localScale = Vector3.one * scale.val;

    }

    int GetMultiplier()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            return 3;
        return 1;
    }
}
