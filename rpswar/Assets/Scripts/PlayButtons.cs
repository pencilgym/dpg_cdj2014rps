using UnityEngine;
using System.Collections;

public class PlayButtons : MonoBehaviour {
	public GUIStyle paperButtonBlue;
	public GUIStyle rockButtonBlue;
	public GUIStyle scissorButtonBlue;
	public GUIStyle paperButtonRed;
	public GUIStyle rockButtonRed;
	public GUIStyle scissorButtonRed;
	
	public gameplay levelManager;
	
	choice p1Press = choice.undecided;
	choice p2Press = choice.undecided;
	
	void Start()
	{
		levelManager = GetComponent<gameplay> ();
	}
	
	void Update()
	{
		checkAllTouches ();
	}
	
	void OnGUI(){
		/*
		float leftButtonX = (float)(Screen.width / 10) ;
		float rightButtonX = (float)(Screen.width / 1.3) ;
		float buttonYPaper = (float)(Screen.height / 100);
		float buttonYRock = (float) (Screen.height /3);
		float buttonYScissors = (float) (Screen.height / 1.5);
		float buttonDiameter = (float)(Screen.height / 3.5);
		
		if (GUI.Button(new Rect(leftButtonX, buttonYPaper, buttonDiameter, buttonDiameter), "", paperButtonBlue)) {
			p1Press = choice.paper;
		}
		if (GUI.Button(new Rect(leftButtonX, buttonYRock, buttonDiameter, buttonDiameter), "", rockButtonBlue)) {
			p1Press = choice.rock;
		}
		if (GUI.Button(new Rect(leftButtonX, buttonYScissors, buttonDiameter, buttonDiameter), "", scissorButtonBlue)) {
			p1Press = choice.scissors;
		}
		if (GUI.Button(new Rect(rightButtonX, buttonYPaper, buttonDiameter, buttonDiameter), "", paperButtonRed)) {
			p2Press = choice.paper;
		}
		if (GUI.Button(new Rect(rightButtonX, buttonYRock, buttonDiameter, buttonDiameter), "", rockButtonRed)) {
			p2Press = choice.rock;
		}
		if (GUI.Button(new Rect(rightButtonX, buttonYScissors, buttonDiameter, buttonDiameter), "", scissorButtonRed)) {
			p2Press = choice.scissors;
		}*/
		
	}

	void checkAllTouches() {
		int fingerCount = 0;
		foreach (Touch touch in Input.touches) {
			//			if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled) {
			if (touch.phase == TouchPhase.Began) {
				SetChoice (touch.position);
				fingerCount++;
			}
		}
		// if no touches then accept a mouse click as a simulated touch
		if (fingerCount == 0 && Input.GetMouseButtonDown (0)) {
				SetChoice (Input.mousePosition);
		}
	}

	void SetChoice(Vector2 vect)
	{

		Vector3 v = new Vector3(vect.x, vect.y, 0);
		Ray r = Camera.main.ScreenPointToRay (v);
		RaycastHit hit;
		if (Physics.Raycast (r, out hit)) {
			Debug.Log ("SHIT");
			ButtonBehavior b =hit.transform.GetComponent<ButtonBehavior>() as ButtonBehavior;
			if(b.p1) p1Press = b.buttonChoice;
			else p2Press = b.buttonChoice;
		}
		levelManager.determineWinner (p1Press, p2Press);

	}
	
}
