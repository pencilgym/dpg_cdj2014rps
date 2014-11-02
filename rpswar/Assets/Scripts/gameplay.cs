using UnityEngine;
using System.Collections;

public class gameplay : MonoBehaviour {
	static	gState gameState;
	public GameObject playerObj;
	static public choice player1choice = choice.undecided;
	static public choice player2choice = choice.undecided;
	int screenX = 0;	// backgroud screen we're fighting on
	float destinationX;	// when scrolling to a new screen, X coord we want for camera

	float[] screenXs = {
		-76.8f,-57.6f,-38.4f,-19.2f,0,19.2f,38.4f,57.6f,76.8f
	};
	// Use this for initialization
	void Start () {
		gameState = gState.setupscene;
		screenX = 4;	// middle screen
		transform.position = new Vector3(screenXs[screenX],transform.position.y,transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		switch (gameState) {
			case gState.setupscene:
				// instantiate players
				Instantiate(playerObj, new Vector3(-3.34f, 1.07f, 0), Quaternion.identity);
				GameObject tmpPlr = (GameObject) Instantiate(playerObj, new Vector3(17.3f, 1.07f, 0), Quaternion.identity);
				tmpPlr.GetComponent<playerScript>().makePlayerTwo();
				gameState = gState.intro;
				break;
			case gState.intro:
				break;
			case gState.getready:
				break;
			case gState.choose:
				break;
			case gState.showresult:
				// check to see if final victory achieved, if so go to victory

				// when done showing the result, start scrolling screen
				destinationX = screenXs[screenX];
				gameState = gState.movetonextscene;
				break;
			case gState.movetonextscene:
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


	public winner determineWinner() {
		if (player1choice == choice.undecided && player2choice == choice.undecided)
			return winner.tie;
		switch (player1choice) {
		case choice.undecided:
			return winner.p2wins;
		case choice.rock:
			{
				switch (player2choice) {
				case choice.rock:
					return winner.tie;
				case choice.scissors:
				case choice.undecided:
					return winner.p1wins;
				case choice.paper:
				default:
				return winner.p2wins;
				}
			}
		case  choice.scissors:
			{
			switch (player2choice) {
				case choice.rock:
					return winner.p2wins;
				case choice.scissors:
					return winner.tie;
				case choice.paper:
				case choice.undecided:
					default:
					return winner.p1wins;
				}
			}
		case  choice.paper:
		default:
			{
				switch (player2choice) {
				case choice.undecided:
				case choice.rock:
					return winner.p1wins;
				case choice.scissors:
					return winner.p2wins;
				case choice.paper:
					default:
					return winner.tie;
				}
			}
		}
	}

}
