using UnityEngine;
using System.Collections;

public class playerScript : MonoBehaviour {
	
	public enum pState {
		introrun,
		idle,
		win,
		lose,
		winrun
	};
	
	public bool	playerTwo;
	public float runspeed = 3.5f;
	public pState playerState;
	float misctime;
	
	// Use this for initialization
	void Start () {
		playerState = pState.introrun;
		misctime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		switch (playerState) {
		case pState.introrun:
			transform.position += new Vector3(runspeed * Time.deltaTime,0,0);
			if (Time.time > misctime + 1.5f)
				playerState = pState.idle;
			break;
		case pState.idle:
			break;
		case pState.win:
			break;
		case pState.lose:
			break;
		case pState.winrun:
			break;
		}
		
	}
	
	public void makePlayerTwo() {
		playerTwo = true;
		runspeed = -runspeed;	// runs left instead of right
	}
}