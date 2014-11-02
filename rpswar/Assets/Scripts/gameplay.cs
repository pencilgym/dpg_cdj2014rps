using UnityEngine;
using System.Collections;

public class gameplay : MonoBehaviour {

	enum gState {
		setupscene,
		intro,
		getready,
		choose,
		showresult,
		movetonextscene,
		victory,
	};

	public enum choice {
		undecided,
		rock,
		scissors,
		paper
	};

	enum winner {
		p1wins,
		p2wins,
		tie
	}

	static	gState gameState;
	public GameObject playerObj;
	static public choice player1choice = choice.undecided;
	static public choice player2choice = choice.undecided;

	// Use this for initialization
	void Start () {
		gameState = gState.setupscene;
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

	winner determineWinner() {
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
