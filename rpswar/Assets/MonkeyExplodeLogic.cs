using UnityEngine;
using System.Collections;

public class MonkeyExplodeLogic : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy (this, 3f);
	}
}
