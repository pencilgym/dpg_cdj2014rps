using UnityEngine;
using System.Collections;

public class PlayButtons : MonoBehaviour {
	public GUIStyle paperButton;
	public GUIStyle rockButton;
	public GUIStyle scissorButton;

	public GameObject levelManager;

	void OnGUI(){

		float leftButtonX = (float)(Screen.width / 10) ;
		float rightButtonX = (float)(Screen.width / 1.3) ;
		float buttonYPaper = (float)(Screen.height / 100);
		float buttonYRock = (float) (Screen.height /3);
		float buttonYScissors = (float) (Screen.height / 1.5);

		if (GUI.Button(new Rect(leftButtonX, buttonYPaper, 100, 100), "", paperButton)) {
		
		}
		if (GUI.Button(new Rect(leftButtonX, buttonYRock, 100, 100), "", rockButton)) {
			
		}
		if (GUI.Button(new Rect(leftButtonX, buttonYScissors, 100, 100), "", scissorButton)) {
			
		}
		if (GUI.Button(new Rect(rightButtonX, buttonYPaper, 100, 100), "", paperButton)) {
			
		}
		if (GUI.Button(new Rect(rightButtonX, buttonYRock, 100, 100), "", rockButton)) {
			
		}
		if (GUI.Button(new Rect(rightButtonX, buttonYScissors, 100, 100), "", scissorButton)) {
			
		}

		

	}


}
