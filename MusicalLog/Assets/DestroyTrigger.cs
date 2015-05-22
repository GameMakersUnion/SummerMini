using UnityEngine;
using System.Collections;

public class DestroyTrigger : MonoBehaviour {
    public Protopoops test;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter(Collider other) {
        test.replaceFirst();
    }
}
