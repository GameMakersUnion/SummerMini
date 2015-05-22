using UnityEngine;
using System.Collections;

public class Protopoops : MonoBehaviour {
    private ArrayList platforms;
    public GameObject basePlatform;
    private const int MAX_SIZE = 20;
	// Use this for initialization
	void Start () {
        platforms = new ArrayList(MAX_SIZE + 1);
        for (int i = 0; i < MAX_SIZE; i++)
        {
            GameObject g = Instantiate<GameObject>(basePlatform);
            platforms.Add(g);
            g.transform.position = new Vector3(i * g.transform.localScale.x, 0, 0);
            g.GetComponent<Rigidbody>().velocity = new Vector3(-1, 0, 0);
        }
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void replaceFirst() {
        int iteration = 0;
        foreach (GameObject g in platforms)
        {
            iteration++;
            if (iteration == 1)
            {
                Destroy(g);
            }
            else if (iteration == MAX_SIZE)
            {
                GameObject gObject = Instantiate<GameObject>(basePlatform);
                platforms.Add(gObject);
                gObject.transform.position = new Vector3(g.transform.position.x + g.transform.localScale.x, 0, 0);
                gObject.GetComponent<Rigidbody>().velocity = new Vector3(-1, 0, 0);
                break;
            }
        }
        platforms.RemoveAt(0);
    }

    public void passSample(float[] sample)
    {
        float average = 0;
        for (int i = 0; i < sample.Length; i++)
        {
            average += sample[i];
        }

        average /= sample.Length;

        int iteration = 0;
        int sampleIteration = 0;
        foreach (GameObject g in platforms)
        {
            iteration++;
            if (iteration > MAX_SIZE - sample.Length)
            {
                g.transform.localScale = new Vector3(g.transform.localScale.x, g.transform.localScale.y + (sample[sampleIteration] - average) * 1000, g.transform.localScale.z);
                sampleIteration++;
            }
        }
    }

    public void passFloat(float f)
    {
        int iteration = 0;
         foreach (GameObject g in platforms)
        {
            iteration++;
            if (iteration == MAX_SIZE)
            {
                g.transform.localScale = new Vector3(g.transform.localScale.x, Mathf.Max(10*f,1), g.transform.localScale.z);
            }
        }
    }
}
