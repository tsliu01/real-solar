using UnityEngine;
using System.Collections;

public class SelfRotate : MonoBehaviour {
	public float Speed=1.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3(0, -Time.deltaTime * Speed, 0));
	}
}
