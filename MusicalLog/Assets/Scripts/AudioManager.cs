using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
	public GameObject noteBlock;
	AudioSource audioSrc;
	float[] spectrumData, spectrumBuffer, shiftedBuffer;
	int sampleSize = 1024;
	int sampleRate = 44100;
	bool isBeat = false;

	void Start () {
		audioSrc = GetComponent<AudioSource> ();
		spectrumData = new float[sampleSize];
		spectrumBuffer = new float[(int)(sampleRate / sampleSize)];
		// Initialize buffer to 0 for each element
		for (int i = 0; i < spectrumBuffer.Length; i++) {
			spectrumBuffer[i] = 0.0f;
		}
	}

	void Update () {
		if (InstantSoundEnergy() > AverageSoundEnergy() * 1f) {
			isBeat = true;
		}

		if (isBeat) {
			Debug.Log ("BEAT");
			isBeat = false;
		}
	}

	// Current Sound Energy
	float InstantSoundEnergy () {
		audioSrc.GetSpectrumData (spectrumData, 1, FFTWindow.BlackmanHarris);
		float energy = SumFloatArray (spectrumData);
		return energy;
	}

	// Local Average Sound Energy
	float AverageSoundEnergy () {
		shiftedBuffer = new float[spectrumBuffer.Length];
		for (int i = 0; i < shiftedBuffer.Length - 1; i++) {
			shiftedBuffer[i+1] = spectrumBuffer[i];
		}
		shiftedBuffer[0] = InstantSoundEnergy ();
		spectrumBuffer = shiftedBuffer;
		float averageEnergy = SumFloatArray (spectrumBuffer) / spectrumBuffer.Length;
		return averageEnergy;
	}

	// Sum of all array elements
	float SumFloatArray (float[] array) {
		float sum = 0.0f;
		for (int i = 0; i < array.Length; i++) {
			sum += array[i];
		}
		return sum;
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
