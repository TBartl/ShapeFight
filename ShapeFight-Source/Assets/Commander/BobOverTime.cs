using UnityEngine;
using System.Collections;

public class BobOverTime : MonoBehaviour {

    public Vector2 range;
    public float speed;

    float amount;

	void Start () {
	
	}
	
	void Update () {
        amount = amount + Time.deltaTime * speed;
        amount = amount % (Mathf.PI * 2);
        float percent = (Mathf.Sin(amount) + 1) /2;
        this.transform.localPosition = new Vector3(0,percent * range.x + (1-percent) * range.y,0);	
	}
}
