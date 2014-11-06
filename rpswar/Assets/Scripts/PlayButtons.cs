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

	public AudioClip ac;
	
	//choice p1Press = choice.undecided;
	//choice p2Press = choice.undecided;

	protected ButtonBehavior _b;
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
		if(Input.touchCount > 0)
		{

			for (int i = 0;  i<Input.touchCount;i++)
			{
		//	Application.LoadLevel("Menu");
			Touch t = Input.GetTouch(i);
			if(t.phase == TouchPhase.Began)
				{
					SetChoice (t.position);
				}
			}
		}
		// if no touches then accept a mouse click as a simulated touch
		if (fingerCount == 0 && Input.GetMouseButtonDown (0)) {
			SetChoice (Input.mousePosition);
		}
	}
	
	void SetChoice(Vector2 vect)
	{
		if (!levelManager.canDeclare)	// exit if buttons can not be pressed yet
			return;
		Vector3 v = new Vector3(vect.x, vect.y, 0);
		Ray r = Camera.main.ScreenPointToRay (v);
		RaycastHit hit;
		if (Physics.Raycast (r, out hit)) {
			audio.PlayOneShot(ac);
			Debug.Log ("button hit");
			ButtonBehavior b = hit.transform.GetComponent<ButtonBehavior>() as ButtonBehavior;
			b.GetComponent<SpriteRenderer>().sprite = b.pressedSprite;
			_b = b;
			Invoke ("StupidAnimation", 0.1f);
			if(b.p1) levelManager.player1choice = b.buttonChoice;
			else levelManager.player2choice = b.buttonChoice;
			string debugstring = "p1: " + levelManager.player1choice + "p2: " + levelManager.player2choice;
			Debug.Log (debugstring);
		}
		// determine winner decided in gameplay.cs
		// levelManager.determineWinner (p1Press, p2Press);
		
	}
	void StupidAnimation()
	{
		_b.GetComponent<SpriteRenderer> ().sprite = _b.defaultSprite;
	}
	
}
