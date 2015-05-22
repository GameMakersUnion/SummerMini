using UnityEngine;
using System.Collections;

public class PointB : MonoBehaviour {
    GameObject Player;
	// Use this for initialization
    void Start() {
        Player = GameObject.FindGameObjectWithTag("Player1");
	}
	
	// Update is called once per frame
	void Update () {
        
	}
    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "Player2") {
            Player.GetComponent<Player1>().TakeDamage();
            MoveToRandomPositionOnScreen();
            
        }
    }

    void MoveToRandomPositionOnScreen() {
        Vector3 RandomPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height), 0));
        RandomPosition = new Vector3(RandomPosition.x, RandomPosition.y, 0);
        transform.position = RandomPosition;
    }
}
