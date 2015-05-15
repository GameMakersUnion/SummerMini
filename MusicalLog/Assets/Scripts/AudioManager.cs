using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
	GameObject musicCube;
	AudioSource jukeBox;
	float[] samples;
	GameObject[] cubes;
	// Use this for initialization
	void Start () {
		musicCube = (GameObject)Resources.Load ("Prefabs/MusicCube");
		jukeBox = GameObject.Find ("JukeBox").GetComponent<AudioSource> ();
		samples = new float[4096];

		cubes = new GameObject[64];
		for (int i = 0; i < cubes.Length; i++) {
			cubes[i] = (GameObject)Instantiate(musicCube, new Vector3 ((musicCube.transform.localScale.x + 1) * i, 0, 0), Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {
		jukeBox.GetSpectrumData (samples, 0, FFTWindow.Rectangular);

		for(int i = 0; i < cubes.Length; i++) {
			cubes[i].transform.localScale = new Vector3 (1, Mathf.Min (Mathf.Abs(samples[i] * 150) + 1, 40), 1);

			cubes[i].GetComponent<MeshRenderer>().material.color = Color.Lerp (Color.green, Color.red, cubes[i].transform.localScale.y/40) ;


			/*if (i > 0 && i < cubes.Length - 1) {
				if (samples[i] > samples[i+1] && samples[i] > samples[i-1]) {
					cubes[i].GetComponent<MeshRenderer>().material.color = Color.red;
				}
				else {
					cubes[i].GetComponent<MeshRenderer>().material.color = Color.green;
				}
			}*/
		}


	}
}
