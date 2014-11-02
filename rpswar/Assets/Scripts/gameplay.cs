using UnityEngine;
using System.Collections;

public class gameplay : MonoBehaviour {
	static	gState gameState;
	public GameObject playerObj;
	public GameObject playerObj2;
	public GameObject numObj;
	
	GameObject plr1;
	GameObject plr2;
	
	int screenX;	// backgroud screen we're fighting on
	bool newWinner = false;	// false = player 2 won, move left, true = player 1 won, move right
	public float scrollSpeed = 6f;
	
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
			break;
		case gState.intro:	// run characters in from sides
			if (plr1.GetComponent<playerScript>().playerState == pState.idle) {
				float centerX = screenXs[screenX] + Camera.main.pixelWidth/2;
				//	Instantiate(numObj, new Vector3(centerX, Camera.main.pixelHeight/2, 0), Quaternion.identity); // spawn #
				Instantiate(numObj, new Vector3(0, 1.2f, 0), Quaternion.identity); // spawn #
				gameState = gState.getready;
			}
			break;
		case gState.getready:	// count down 5..4..3..2..1..
			
			break;
		case gState.choose:
			break;
		case gState.showresult:
			if (newWinner) {	// player 1 wins this battle?
				if (screenX == 7) {
					gameState = gState.victory;
				} else {
					screenX++;
					gameState = gState.movetonextscene;
				}
			} else {	// player 2 wins this battle
				if (screenX == 0) {
					gameState = gState.victory;
				} else {
					screenX--;
					gameState = gState.movetonextscene;
				}
			}
			break;
		case gState.movetonextscene:
			scrollCamera();
			break;
		case gState.victory:
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
	public void beginDeclaration() {
		gameState = gState.choose;
	}
	
	void scrollCamera() {
		if (newWinner) {	// player 1 won, scrolling left
			transform.position += new Vector3(scrollSpeed * Time.deltaTime,0,0);
			if (transform.position.x > screenXs[screenX])
				gameState = gState.getready;
		} else {		// player 2 won, scrolling right
			transform.position -= new Vector3(scrollSpeed * Time.deltaTime,0,0);
			if (transform.position.x < screenXs[screenX])
				gameState = gState.getready;
		}
	}
	
	
	public winner determineWinner(choice one, choice two) {

		playerScript p1 = plr1.GetComponent<playerScript> () as playerScript;
		playerScript p2 = plr2.GetComponent<playerScript> () as playerScript;
		Debug.Log (one + " " + two);
		if (one == choice.undecided && two == choice.undecided) {
			p1.ChangeAnimation(pState.idle);
			p2.ChangeAnimation(pState.idle);
			gameState = gState.getready;
			return winner.tie;
			}
		switch (one) {
		case choice.undecided:
			p2.ChangeAnimation(pState.winrun);
			return winner.p2wins;
		case choice.rock:
			switch (two) {
			case choice.rock:
				p1.ChangeAnimation(pState.idle);
				p2.ChangeAnimation(pState.idle);
				gameState = gState.getready;
				return winner.tie;
			case choice.scissors:
				p1.ChangeAnimation(pState.winrun);
				p2.ChangeAnimation(pState.lose);
				return winner.p1wins;
			case choice.undecided:
				p1.ChangeAnimation(pState.winrun);
				return winner.p1wins;
			case choice.paper:
				p2.ChangeAnimation(pState.winrun);
				p1.ChangeAnimation(pState.lose);
				return winner.p2wins;
			}
			break;
		case choice.scissors:
			switch (two) {
			case choice.rock:
				p2.ChangeAnimation(pState.winrun);
				p1.ChangeAnimation(pState.lose);
				return winner.p2wins;
			case choice.scissors:
				p1.ChangeAnimation(pState.idle);
				p2.ChangeAnimation(pState.idle);
				gameState = gState.getready;
				return winner.tie;
			case choice.paper:
				p1.ChangeAnimation(pState.winrun);
				p2.ChangeAnimation(pState.lose);
				return winner.p1wins;
			case choice.undecided:
				p1.ChangeAnimation(pState.winrun);
				p2.ChangeAnimation(pState.lose);
				return winner.p1wins;
			}
			break;
		case choice.paper:
			switch (two) {
			case choice.undecided:
				p1.ChangeAnimation(pState.idle);
				p2.ChangeAnimation(pState.idle);
				gameState = gState.getready;
				return winner.tie;
			case choice.rock:
				p2.ChangeAnimation(pState.lose);
				p1.ChangeAnimation(pState.winrun);
				return winner.p1wins;
			case choice.scissors:
				p1.ChangeAnimation(pState.lose);
				p2.ChangeAnimation(pState.winrun);
				return winner.p2wins;
			case choice.paper:
				p1.ChangeAnimation(pState.idle);
				p2.ChangeAnimation(pState.idle);
				gameState = gState.getready;
				return winner.tie;
			}
			break;
		default:
			p1.ChangeAnimation(pState.idle);
			p2.ChangeAnimation(pState.idle);
			gameState = gState.getready;
			return winner.tie;
		}
		p1.ChangeAnimation(pState.idle);
		p2.ChangeAnimation(pState.idle);
		gameState = gState.getready;
		return winner.tie;
	}
	
}
