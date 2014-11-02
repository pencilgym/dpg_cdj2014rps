using UnityEngine;
using System.Collections;

public class playerScript : MonoBehaviour {

	public float scaleValue = 0.4f;
	public bool	playerTwo;
	public float runspeed = 3.5f;
	public pState playerState;
	private string playerColor;
	float misctime;


	// Use this for initialization
	void Start () {
		Vector3 vect = transform.localScale;
		vect = vect * scaleValue;
		playerState = pState.introrun;
		misctime = Time.time;
		if (playerTwo) {
			vect.x = vect.x*(-1);
		}
		transform.localScale = vect;
	}
	
	// Update is called once per frame
	void Update () {
		switch (playerState) {
		case pState.introrun:
			transform.position += new Vector3(runspeed * Time.deltaTime,0,0);
			if (Time.time > misctime + 1.5f){
				playerState = pState.idle;
				ChangeAnimation(playerState);
			}
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

	public void ChangeAnimation(pState state){
		if(playerTwo){
			playerColor = "blue";
		} else {
			playerColor = "red";
		}
		if (state == pState.idle) {
			playerState = pState.idle;
			animation.Play (playerColor+"_idle");
		}
		if(state == pState.introrun) {
			playerState = pState.introrun;
			animation.Play(playerColor+"_run");
		}
		if(state == pState.winrun){
			playerState = pState.winrun;
			animation.Play (playerColor+"_run");
		}
		if (state == pState.lose){
			playerState = pState.lose;
			animation.Play (playerColor+"_death");
		}
		if (state == pState.win){
			playerState = pState.win;
		}
	}
}