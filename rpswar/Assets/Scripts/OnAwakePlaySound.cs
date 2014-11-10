using UnityEngine;
using System.Collections;

public class OnAwakePlaySound : MonoBehaviour {
	public AudioClip sound;

	// Use this for initialization
	void Start(){

	}
	void Update(){

	}
	void OnEnable(){
		audio.Play ();
	}
}
