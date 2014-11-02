using UnityEngine;
using System.Collections;

public class gameplay : MonoBehaviour {
	static	gState gameState;
	public GameObject playerObj;
	public GameObject numberObj;

	GameObject plr1;
	GameObject plr2;

	int screenX = 0;	// backgroud screen we're fighting on
	bool	newWinner = false;	// false = player 2 won, move left, true = player 1 won, move right
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
				plr1 = Instantiate(playerObj, new Vector3(-3.34f, 1.07f, 0), Quaternion.identity) as GameObject;
				plr2 = (GameObject) Instantiate(playerObj, new Vector3(17.3f, 1.07f, 0), Quaternion.identity);
				plr2.GetComponent<playerScript>().makePlayerTwo();
				gameState = gState.intro;
				break;
			case gState.intro:	// run characters in from sides
//				gameState = gState.showresult;
//				newWinner = true;
				if (plr1.GetComponent<playerScript>().playerState == pState.idle) {
					float centerX = screenXs[screenX] + Screen.width/2;
//					Instantiate(numberObj, new Vector3(centerX, Screen.height/2, 0), Quaternion.identity); // spawn #
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
				if (newWinner) {	// player 1 won, scrolling left
					transform.position += new Vector3(scrollSpeed * Time.deltaTime,0,0);
					if (transform.position.x > screenXs[screenX])
						gameState = gState.getready;
				} else {		// player 2 won, scrolling right
					transform.position -= new Vector3(scrollSpeed * Time.deltaTime,0,0);
					if (transform.position.x < screenXs[screenX])
						gameState = gState.getready;
				}
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
	
	void scrollCamera() {

	}


	static public winner determineWinner(choice one, choice two) {
		Debug.Log (one + " " + two);
		if (one == choice.undecided && two == choice.undecided) return winner.tie;
		switch (one) {
			case choice.undecided:
				return winner.p2wins;
			case choice.rock:
				switch (two) {
					case choice.rock:
						return winner.tie;
					case choice.scissors:
						return winner.p1wins;
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
						return winner.p1wins;
					case choice.undecided:
						return winner.p1wins;
					}
			break;
			case choice.paper:
					switch (two) {
					case choice.undecided:
						return winner.tie;
					case choice.rock:
						return winner.p1wins;
					case choice.scissors:
						return winner.p2wins;
					case choice.paper:
						return winner.tie;
					}
			break;
			default: return winner.tie;
		}
		return winner.tie;
	}

}
