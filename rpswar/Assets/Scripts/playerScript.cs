using UnityEngine;
using System.Collections;

public class playerScript : MonoBehaviour {

	public bool	playerTwo;
	public float runspeed = 3.5f;
	public pState playerState;
	private string playerColor;
	float misctime;
	

	private bool done = false;
	// Use this for initialization
	void Start () {
		Vector3 vect = transform.localScale;
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
		//	transform.position += new Vector3(runspeed * Time.deltaTime,0,0);
			if (Time.time > misctime + 1.5f){
				playerState = pState.idle;

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
		ChangeAnimation(playerState);
	}
	
	public void makePlayerTwo() {
		playerTwo = true;
		runspeed = -runspeed;	// runs left instead of right
	}
	
	public void ChangeAnimation(pState state){

//		Debug.Log (gameObject.name);

		playerState = state;
		if(playerTwo){
			playerColor = "blue";
		} else {
			playerColor = "red";
		}
		if (state == pState.idle) {
			animation.wrapMode = WrapMode.Loop;
			animation.Play (playerColor+"_idle");
		}
		if(state == pState.introrun) {
			animation.wrapMode = WrapMode.Loop;
			animation.Play(playerColor+"_run");
		}
		if(state == pState.winrun){
			animation.wrapMode = WrapMode.Loop;
			animation.Play(playerColor+"_run");
		}
		if (state == pState.lose){
			animation.wrapMode = WrapMode.Once;
			animation.Play (playerColor+"_death");
		}
		if (state == pState.victory){
			animation.wrapMode = WrapMode.Loop;
			animation.Play (playerColor+"_victory");
		}
		if (state == pState.defeat && !done){
			animation.wrapMode = WrapMode.Once;
			animation.Play (playerColor+"_crouch");
			done = true;
		}
	}

	void FinishWinner()
	{
		playerState = pState.idle;
	}
}