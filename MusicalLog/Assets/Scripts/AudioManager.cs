using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
	public GameObject noteBlock;
	AudioSource audioSrc;
	float[] spectrumData, spectrumBuffer, shiftedBuffer;
	int sampleSize = 1024;
	int sampleRate = 44100;
	int bufferLength;
	float constant, variance;
	bool isBeat = false;
	int numOfNotes = 3;

	void Start () {
		audioSrc = GetComponent<AudioSource> ();
		bufferLength = (int)(sampleRate / sampleSize);
		spectrumData = new float[sampleSize];
		spectrumBuffer = new float[bufferLength];
		// Initialize buffer to 0 for each element
		for (int i = 0; i < bufferLength; i++) {
			spectrumBuffer[i] = 0.0f;
		}
	}

	void Update () {
		variance = SumVariance () / bufferLength;
		constant = (-0.0025714f * variance) + 1.5142857f;
		audioSrc.GetSpectrumData (spectrumData, 1, FFTWindow.BlackmanHarris);
		if (InstantSoundEnergy() > AverageSoundEnergy() * constant) {
			isBeat = true;
		}

		if (isBeat) {
			noteBlock.GetComponent<MeshRenderer> ().material.color = Color.red;
			PlayNotes ();
			isBeat = false;
		} else {
			noteBlock.GetComponent<MeshRenderer> ().material.color = Color.white;
		} 
	}

	// Current Sound Energy
	float InstantSoundEnergy () {
		float energy = SumFloatArray (spectrumData);
		energy = energy * energy;
		return energy;
	}

	// Local Average Sound Energy
	float AverageSoundEnergy () {
		shiftedBuffer = new float[bufferLength];
		for (int i = 0; i < bufferLength - 1; i++) {
			shiftedBuffer[i+1] = spectrumBuffer[i];
		}
		shiftedBuffer[0] = InstantSoundEnergy ();
		spectrumBuffer = shiftedBuffer;
		float averageEnergy = SumFloatArray (spectrumBuffer) / bufferLength;
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

	// Sum of variance
	float SumVariance () {
		float sum = 0;
		for (int i = 0; i < bufferLength; i++) {
			float x = spectrumBuffer[i] - AverageSoundEnergy();
			sum += x * x;
		}
		return sum;
	}

	void PlayNotes () {
		Debug.Log ("BEAT");
		//int[] notes = DetectNotes ();
		// play note
	}
	 
//	int[] DetectNotes () {
//		int searchSpace = spectrumData.Length / numOfNotes;
//		for (int i = 0; i < numOfNotes; i++) {
//			// check each division for prominent frequencies
//		}
//		return 
//	}
}
