using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
	public GameObject noteBlock;
	AudioSource audioSrc;
	float[] spectrumData, spectrumBuffer;
	int sampleSize = 1024;
	int sampleRate = 44100;
	float constant = 1;
	bool isBeat = false;

	void Start () {
		audioSrc = GetComponent<AudioSource> ();
		spectrumData = new float[sampleSize];
		spectrumBuffer = new float[(int)(sampleRate / sampleSize)];
	}

	void Update () {
		if (InstantSoundEnergy() > AverageSoundEnergy() * constant) {
			isBeat = true;
		}

		if (isBeat) {
			noteBlock.transform.localScale = new Vector3 (1, 2, 1);
			isBeat = false;
		} else {
			noteBlock.transform.localScale = new Vector3 (1, 1, 1);
		}
	}

	// Current Sound Energy
	float InstantSoundEnergy () {
		audioSrc.GetSpectrumData (spectrumData, 1, FFTWindow.BlackmanHarris);
		float energy = 0.0f;
		for (int i = 0; i < spectrumData.Length; i++) {
			energy += spectrumData[i];
		}
		return energy;
	}

	// Local Average Sound Energy
	float AverageSoundEnergy () {
		float averageEnergy = 0.0f;
		for (int i = 0; i < spectrumBuffer.Length; i++) {
			averageEnergy += spectrumData[i];
		}
		averageEnergy /= spectrumBuffer.Length;
		return averageEnergy;
	}


//	GameObject musicCube;
//	AudioSource jukeBox;
//	float[] samples;
//	GameObject[] cubes;
//	// Use this for initialization
//	void Start () {
//		musicCube = (GameObject)Resources.Load ("Prefabs/MusicCube");
//		jukeBox = GameObject.Find ("JukeBox").GetComponent<AudioSource> ();
//		samples = new float[4096];
//
//		cubes = new GameObject[64];
//		for (int i = 0; i < cubes.Length; i++) {
//			cubes[i] = (GameObject)Instantiate(musicCube, new Vector3 ((musicCube.transform.localScale.x + 1) * i, 0, 0), Quaternion.identity);
//		}
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		jukeBox.GetSpectrumData (samples, 0, FFTWindow.Rectangular);
//
//		for(int i = 0; i < cubes.Length; i++) {
//			cubes[i].transform.localScale = new Vector3 (1, Mathf.Min (Mathf.Abs(samples[i] * 150) + 1, 40), 1);
//
//			cubes[i].GetComponent<MeshRenderer>().material.color = Color.Lerp (Color.green, Color.red, cubes[i].transform.localScale.y/40) ;
//
//
//			/*if (i > 0 && i < cubes.Length - 1) {
//				if (samples[i] > samples[i+1] && samples[i] > samples[i-1]) {
//					cubes[i].GetComponent<MeshRenderer>().material.color = Color.red;
//				}
//				else {
//					cubes[i].GetComponent<MeshRenderer>().material.color = Color.green;
//				}
//			}*/
//		}
//
//
//	}
}
