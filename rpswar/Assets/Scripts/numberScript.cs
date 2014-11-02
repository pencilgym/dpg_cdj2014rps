using UnityEngine;
using System.Collections;

public class numberScript : MonoBehaviour {
	
	float timeToTick;	// keep track of time for next tick
	int ticksToGo;
	
	// Use this for initialization
	void Start () {
		timeToTick = Time.time+0.5f;
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
		Transform curChild = transform.Find(childnames[ticksToGo]);
		curChild.localScale += new Vector3(.15f, .15f, 0);	// make number larger
		
		if (Time.time > timeToTick) {
			timeToTick = Time.time+0.5f;
			if (--ticksToGo < 0) {
				Camera.main.GetComponent<gameplay>().beginDeclaration();
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
