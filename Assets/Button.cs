using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {
    private bool isActive;
    private Vector3 idleLocation;
    private Vector3 activeLocation;
    public CursorActivity cursorActivity;
	// Use this for initialization
	void Start () {
        isActive = false;
        idleLocation = transform.localPosition;
        activeLocation = transform.localPosition - new Vector3(0, 0, 1);
	}
	
	// Update is called once per frame
	void Update () {
        if (cursorActivity.isSelecting == true && (cursorActivity.selectedIcon == gameObject||cursorActivity.selectedIcon.transform.IsChildOf(transform)))
        {
            if (transform.localPosition.z > activeLocation.z)
            {
                GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load(name + "_hover",typeof(Sprite));
                transform.localPosition -= new Vector3(0, 0, 0.5f);
                GetComponent<BoxCollider>().center += new Vector3(0, 0, 0.5f);
            }
        }
        else if (transform.localPosition.z < idleLocation.z)
        {
            GetComponent<SpriteRenderer>().sprite = (Sprite)Resources.Load(name + "_idle", typeof(Sprite));
            transform.localPosition += new Vector3(0, 0, 0.5f);
            GetComponent<BoxCollider>().center -= new Vector3(0, 0, 0.5f);
        }
        //if (transform.localPosition.z != idleLocation.z)
        //{
        //    isActive = true;
        //}
        //if (transform.localPosition.z == idleLocation.z)
        //{
        //    isActive = true;
        //}
    }
}
