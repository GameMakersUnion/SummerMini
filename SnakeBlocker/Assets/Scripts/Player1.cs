using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player1 : MonoBehaviour {
    Rigidbody RBody;
    static List<GameObject> Tail;
    public int TailSize;
    public GameObject TailObject;
    public int StartingLives;
    int CurrentLives;
    GameObject clone;
    public float SpawnGap;
    public float TailSpeed;
    public int GrowthPerFood;
    // Use this for initialization
    void Start() {
        RBody = gameObject.GetComponent<Rigidbody>();
        CurrentLives = StartingLives;
        Tail = new List<GameObject>();
        for (int i = 0; i < TailSize; i++) {
            clone = (GameObject)Instantiate(TailObject, transform.position, Quaternion.identity);
            Tail.Add(clone);

        }
    }

    // Update is called once per frame
    void Update() {
        Vector3 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MoveHeadTo(MousePos);
        MoveTail();

        
    }

    public void MoveTail() {
        for (int i = 0; i < Tail.Count; i++) {
            GameObject CurrentNode = Tail[i];
            if (CurrentNode != null) {
                if (i == 0) {
                    CurrentNode.GetComponent<Rigidbody>().velocity = TailSpeed * (transform.position - CurrentNode.transform.position);
                }
                else {
                    GameObject PreviousNode = Tail[i - 1];
                    CurrentNode.GetComponent<Rigidbody>().velocity = TailSpeed * (PreviousNode.transform.position - CurrentNode.transform.position);
                }
            }
            else break;
        }
    }

    public void MoveHeadTo(Vector3 NewPosition) {
        NewPosition = new Vector3(NewPosition.x, NewPosition.y, 0);
        gameObject.transform.position = NewPosition;
        RBody.velocity = Vector3.zero;
    }


    public void TakeDamage() {
        //size of the tail after damage has been taken
        CurrentLives--;
        int EventualTailSize = Tail.Count - TailSize / StartingLives;
        for (int i = Tail.Count - 1; i >= EventualTailSize; i--) {
            if (i < 0 || Tail[i] == null) {
                Debug.Log("Snake Loses");
                break;
            }
            GameObject tmp = Tail[i];
            Tail.Remove(tmp);
            Destroy(tmp);
        }
    }

    public void EatFood() {
        for (int i = 0; i < GrowthPerFood; i++) {
            clone = (GameObject)Instantiate(TailObject, transform.position, Quaternion.identity);
            Tail.Insert(0, clone);
        }
    }

    public   bool IsAtMaxHealth() {
        return CurrentLives == StartingLives;
    }
}
