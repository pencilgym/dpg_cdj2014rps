using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gameplay : MonoBehaviour {
	static	gState gameState;
	public GameObject playerObj;
	public GameObject playerObj2;
	public GameObject numObj;
	public float timeToDeclare = 2.0f;
	
	GameObject plr1;
	GameObject plr2;
	
	public choice player1choice;
	public choice player2choice;

	public List<Transform> movePoints = new List<Transform>();
	public float p2OffSet = 5;
	public float moveSpeed = 6;


	int screenX;	// backgroud screen we're fighting on
	bool newWinner = false;	// false = player 2 won, move left, true = player 1 won, move right
	public float scrollSpeed = 6f;
	public bool canDeclare = false;
	float toomuchtime; // keep track of time when it's too late to choose


	public GameObject Victory;
	float[] screenXs = {
		-76.8f,-57.6f,-38.4f,-19.2f,0,19.2f,38.4f,57.6f,76.8f
	};
	// Use this for initialization
	void Start () {
		gameState = gState.setupscene;
		screenX = 4;	// start at middle screen
		transform.position = new Vector3(screenXs[screenX],transform.position.y,transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		switch (gameState) {
		case gState.setupscene:
			// instantiate players
			plr1 = Instantiate(playerObj, new Vector3(-10f, -3.5f, 0), Quaternion.identity) as GameObject;
			plr2 = Instantiate(playerObj2, new Vector3(10f, -3.5f, 0), Quaternion.identity) as GameObject;
			plr2.GetComponent<playerScript>().makePlayerTwo();
			gameState = gState.intro;

			iTween.MoveTo (plr1, movePoints [screenX - 1].position, 1.5f);
			iTween.MoveTo (plr2, Generate2ndOffset(movePoints[screenX-1].position), 1.5f);
			break;
		case gState.intro:	// run characters in from sides
			if (plr1.GetComponent<playerScript>().playerState == pState.idle) {
				beginCountdown ();
			}
			break;
		case gState.getready:	// count down 5..4..3..2..1..
			
			break;
		case gState.choose:		// end after .5 seconds or when both players have declared.
			if (Time.time > toomuchtime || (player1choice != choice.undecided && player2choice != choice.undecided)) {
				canDeclare = false;
				playerScript p1 = plr1.GetComponent<playerScript> () as playerScript;
				playerScript p2 = plr2.GetComponent<playerScript> () as playerScript;
				winner thewinner = determineWinner (player1choice, player2choice);
				if (thewinner == winner.p1wins) {
					newWinner = true;
					gameState = gState.showresult;
					p1.ChangeAnimation(pState.winrun);
					p2.ChangeAnimation(pState.lose);
				} else if (thewinner == winner.p2wins) {
					newWinner = false;
					gameState = gState.showresult;
					p2.ChangeAnimation(pState.winrun);
					p1.ChangeAnimation(pState.lose);
				} else {
					p1.ChangeAnimation(pState.idle);
					p2.ChangeAnimation(pState.idle);
					beginCountdown();
				}
			}
			break;
		case gState.showresult:
			if(screenX == 1 || screenX == 6) OnFinalVictory();
			if (newWinner) {	// player 1 wins this battle?
				if (screenX++ == 7) // p1 wins?
					gameState = gState.victory;
				else
					OnVictory(newWinner);
					gameState = gState.movetonextscene;
			} else {	// player 2 wins this battle
				if (screenX-- == 0)	// p2 wins?
					gameState = gState.victory;
				else
					OnVictory(newWinner);
					gameState = gState.movetonextscene;
			}
			break;
		case gState.movetonextscene:
			if (scrollCamera())
				gameState = gState.getready;
			if(!(screenX == 0 || screenX == 7))Invoke ("beginCountdown", 6f);
			break;
		case gState.victory:
			if (scrollCamera())	// scroll Camera until at final screen
				gameState = gState.finalvictory;
			break;
		case gState.finalvictory:
			Debug.Log("We done son");
			break;
		default:
			break;
		}
		// ...
	}
	
	/*
	void OnGUI() {
		string output;
		// output = "X:" + Input.GetAxisRaw("Mouse X") + ", Y:" + Input.GetAxisRaw("Mouse Y");
		output = "Mouse: " + Input.mousePosition;

		GUI.Label (new Rect (10, 600, 200, 20), output);
	}
	*/
	
	void beginCountdown() {
		float centerX = movePoints[screenX-1].position.x+3;
		//	Instantiate(numObj, new Vector3(centerX, Camera.main.pixelHeight/2, 0), Quaternion.identity); // spawn #
		Instantiate(numObj, new Vector3(centerX, 1.2f, 0), Quaternion.identity); // spawn #
		canDeclare = false;
		gameState = gState.getready;
		
	}
	
	public void beginDeclaration() {
		Debug.Log ("beginDeclaration()");
		gameState = gState.choose;
		toomuchtime = Time.time + timeToDeclare;	// time to choose
		player1choice = choice.undecided;
		player2choice = choice.undecided;
		canDeclare = true;
	}
	
	bool scrollCamera() {
		Vector3 v = movePoints [screenX-1].position;
		v.z = Camera.main.transform.position.z;
		v.y = Camera.main.transform.position.y;
		v.x += 2;

		Hashtable hash =  new Hashtable();
		hash.Add("time", moveSpeed);
		hash.Add("position", v);
		hash.Add ("delay", 2);
		iTween.MoveTo(gameObject, hash);
		return true;
	}
	
	
	public winner determineWinner(choice one, choice two) {
		Debug.Log ("determineWinner()");
		canDeclare = false;
		if (one == choice.undecided && two == choice.undecided) {
			return winner.tie;
		}
		switch (one) {
		case choice.undecided:
			return winner.p2wins;
		case choice.rock:
			switch (two) {
			case choice.rock:
				return winner.tie;
			case choice.scissors:
			case choice.undecided:
				return winner.p1wins;
			case choice.paper:
				return winner.p2wins;
			}
			break;
		case choice.scissors:
			switch (two) {
			case choice.rock:
				return winner.p2wins;
			case choice.scissors:
				return winner.tie;
			case choice.paper:
			case choice.undecided:
				return winner.p1wins;
			}
			break;
		case choice.paper:
			switch (two) {
			case choice.undecided:
			case choice.rock:
				return winner.p1wins;
			case choice.scissors:
				return winner.p2wins;
			case choice.paper:
				return winner.tie;
			}
			break;
		default:
			return winner.tie;
		}
		return winner.tie;
	}


	void OnVictory(bool p1Victor)
	{
		if (p1Victor) {
			Hashtable hash =  new Hashtable();
			hash.Add("time", moveSpeed);
			hash.Add("position", movePoints [screenX - 1].position);
			hash.Add("oncomplete", "FinishWinner");
			hash.Add ("easetype", "linear");
			hash.Add ("delay", 0.5);
			iTween.MoveTo (plr1, hash);
		}
		else{
			Hashtable hash =  new Hashtable();
			hash.Add("time", moveSpeed);
			hash.Add("position", Generate2ndOffset(movePoints[screenX-1].position));
			hash.Add("oncomplete", "FinishWinner");
			hash.Add ("easetype", "linear");
			hash.Add ("delay", 0.5);
			iTween.MoveTo (plr2, hash);
		}
		Invoke ("TransportLoser", 1.3f);
	}

	Vector3 Generate2ndOffset(Vector3 vect)
	{
		Vector3 v = vect;
		v.x += p2OffSet;
		return v;
	}

	void TransportLoser()
	{
		if (newWinner) {
			Debug.Log("p2 lost");
			GameObject.Destroy(plr2);
			plr2=Instantiate(playerObj2,  Generate2ndOffset (movePoints [screenX - 1].position), Quaternion.identity) as GameObject;
				} else {
			GameObject.Destroy(plr1);
			plr1=Instantiate(playerObj,   movePoints [screenX - 1].position, Quaternion.identity) as GameObject;
		}
	}

	void OnFinalVictory()
	{
		foreach (Transform ch in GetComponentsInChildren<Transform>()) {

			if(!(transform == ch))ch.gameObject.SetActive(false);
		}
		Debug.Log ("Add endgame code");
		Invoke ("FuckFuckFuck", 5);
		Invoke ("GoBackToMenu", 10);
	}

	void GoBackToMenu()
	{
		Application.LoadLevel("Menu");
	}

	void FuckFuckFuck()
	{
		Debug.Log ("HUEHUEHU");
		Victory.SetActive (true);
	}
}