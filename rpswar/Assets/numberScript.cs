using UnityEngine;
using System.Collections;

public class numberScript : MonoBehaviour {

	float timeToTick;	// keep track of time for next tick
	int ticksToGo;

	// Use this for initialization
	void Start () {
		timeToTick = Time.time+1.0f;
		ticksToGo = 4;
	}
	
	// Update is called once per frame
	string[] childnames = {
		"timecounter_0",
		"timecounter_1",
		"timecounter_2",
		"timecounter_3",
		"timecounter_4",
	};

	void Update () {
		if (Time.time > timeToTick) {
			timeToTick = Time.time+1.0f;
			if (--ticksToGo < 0) {
				Destroy (gameObject);
			} else {
				Transform child = transform.Find(childnames[ticksToGo+1]);
				child.gameObject.SetActive(false);
				child = transform.Find(childnames[ticksToGo]);
				child.gameObject.SetActive(true);
			}
		}
	}
}
