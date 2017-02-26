using UnityEngine;
using System.Collections;

public class MoonMove : MonoBehaviour {
	public Transform Earth;
	public float Speed=1.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround(Earth.position,Earth.up,-Speed*Time.deltaTime);
		transform.RotateAround(transform.position,Earth.up,Speed*Time.deltaTime);
	}
}
