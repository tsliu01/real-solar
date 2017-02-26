using UnityEngine;
using System.Collections;

public class set : MonoBehaviour {
    public float viewAspect = 1.493f;

    // Use this for initialization
    void Start () {
        GetComponent<Camera>().aspect = viewAspect;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
