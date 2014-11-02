﻿using UnityEngine;
using System.Collections;

public class PlayButtons : MonoBehaviour {
	public GUIStyle paperButton;
	public GUIStyle rockButton;
	public GUIStyle scissorButton;

	public gameplay levelManager;

	choice p1Press = choice.undecided;
	choice p2Press = choice.undecided;

	void Start()
	{
		levelManager = GetComponent<gameplay> ();
	}

	void Update()
	{
		if (p2Press!=choice.undecided && p1Press!=choice.undecided) {
			Debug.Log(gameplay.determineWinner(p1Press, p2Press));
			p1Press = choice.undecided;
			p2Press = choice.undecided;
		}
	}

	void OnGUI(){

		float leftButtonX = (float)(Screen.width / 10) ;
		float rightButtonX = (float)(Screen.width / 1.3) ;
		float buttonYPaper = (float)(Screen.height / 100);
		float buttonYRock = (float) (Screen.height /3);
		float buttonYScissors = (float) (Screen.height / 1.5);
		float buttonDiameter = (float)(Screen.height / 3.5);

		if (GUI.Button(new Rect(leftButtonX, buttonYPaper, buttonDiameter, buttonDiameter), "", paperButton)) {
			p1Press = choice.paper;
		}
		if (GUI.Button(new Rect(leftButtonX, buttonYRock, buttonDiameter, buttonDiameter), "", rockButton)) {
			p1Press = choice.rock;
		}
		if (GUI.Button(new Rect(leftButtonX, buttonYScissors, buttonDiameter, buttonDiameter), "", scissorButton)) {
			p1Press = choice.scissors;
		}
		if (GUI.Button(new Rect(rightButtonX, buttonYPaper, buttonDiameter, buttonDiameter), "", paperButton)) {
			p2Press = choice.paper;
		}
		if (GUI.Button(new Rect(rightButtonX, buttonYRock, buttonDiameter, buttonDiameter), "", rockButton)) {
			p2Press = choice.rock;
		}
		if (GUI.Button(new Rect(rightButtonX, buttonYScissors, buttonDiameter, buttonDiameter), "", scissorButton)) {
			p2Press = choice.scissors;
		}

	}

}
