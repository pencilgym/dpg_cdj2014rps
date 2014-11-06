using UnityEngine;
using System.Collections;

public class TerrainMover : MonoBehaviour {
	public int TerrainPieces = 7;
	public int startVal = 4;
	public GameObject terrain;
	public float shiftSpeed = 1.0f;
	public float Position = 0;

	private float individualSize;
	// Use this for initializatio
	void Start () {
		individualSize = (terrain.GetComponent<SpriteRenderer> ().sprite.rect.width) / TerrainPieces;
		Debug.Log (individualSize);
	}

	public void ShiftLevel(int i)
	{
		startVal += i;

	}

	// Update is called once per frame
	void Update () {
		Shift (Position);
	}

	void Shift(float to)
	{
		float t = to;
		if(to<0) t=t*(-1);
		//"to" MUST BE DIVISBLE BY SHIFTSPEED
		if(Camera.main.transform.position.x!=to)
		{ 
			Vector3 v = Camera.main.transform.position;
			v.x += shiftSpeed;
			Camera.main.transform.position = v;
		}
	}
	
}
