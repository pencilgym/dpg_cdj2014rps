using UnityEngine;
using System.Collections;

public class GUITest : MonoBehaviour {
	public GUIStyle customButton;
	public AudioClip buttonMusic;

	void OnGUI () {
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect((Screen.width/2)-75,(Screen.height/2)+35,150,50), "", customButton)) {
			audio.PlayOneShot(buttonMusic);
			Application.LoadLevel(1);
		}
	}
}
