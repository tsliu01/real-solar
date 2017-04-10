using UnityEngine;
using System.Collections;

public class MoveControllerWithMouse : MonoBehaviour {
	public float Speed = 125.0f;
    //public float RotateSpeed = 25.0f;

    //public void aScript otherscript;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 translation = new Vector3(Input.GetAxis("Horizontal") * Speed, 0, Input.GetAxis("Vertical") * Speed);
        //float rotation = Input.GetAxis("Horizontal") * Speed;
        translation *= Time.deltaTime;
        //rotation *= Time.deltaTime;
        transform.Translate(translation, Space.Self);
        //transform.Rotate(new Vector3(0, rotation, 0), Space.Self);
    }
}
