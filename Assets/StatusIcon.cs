using UnityEngine;
using System.Collections;

public class StatusIcon : MonoBehaviour {
    public bool isActive;
	// Use this for initialization
	void Start () {
        isActive = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (isActive == true)
        {
            GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load(name + "_active", typeof(Sprite));
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load(name + "_idle", typeof(Sprite));
        }
    }
}
