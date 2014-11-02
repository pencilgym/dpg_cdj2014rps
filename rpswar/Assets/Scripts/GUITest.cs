using UnityEngine;
using System.Collections;

public class GUITest : MonoBehaviour {
	public GUIStyle customButton;
	public AudioClip buttonSound;
	void OnGUI () {
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(370,200,100,50), "", customButton)) {
			audio.PlayOneShot(buttonSound);
			Application.LoadLevel(1);
		}
	}
}
