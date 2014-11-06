using UnityEngine;
using System.Collections;

public class activateVictory : MonoBehaviour {
	public AudioClip victoryNoise;
	public GUITexture victoryLabel;

	void Start(){
		victoryScreen ();
	}

	public void victoryScreen(){
		audio.PlayOneShot(victoryNoise);
	}


}
