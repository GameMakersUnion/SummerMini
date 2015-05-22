using UnityEngine;
using System.Collections;

public class Player2 : MonoBehaviour {
    public float SpeedOfMotion;
    Rigidbody RBody;
	// Use this for initialization
	void Start () {
        RBody = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetKey(("w"))) {
            RBody.velocity = new Vector3(RBody.velocity.x, SpeedOfMotion, 0);
        }
        else if (Input.GetKey(("s"))) {
            RBody.velocity = new Vector3(RBody.velocity.x, -SpeedOfMotion, 0);
        }
        else {
            RBody.velocity = new Vector3(RBody.velocity.x, 0, 0);
        }
        if (Input.GetKey(("d"))) {
            RBody.velocity = new Vector3(SpeedOfMotion, RBody.velocity.y, 0);
        }
        else if (Input.GetKey(("a"))) {
            RBody.velocity = new Vector3(-SpeedOfMotion, RBody.velocity.y, 0);
        }
        else {
            RBody.velocity = new Vector3(0, RBody.velocity.y, 0);
        }
        
        
	}
}
