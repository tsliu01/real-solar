using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour {
    public GameObject target;
    public GameObject subObject;

    private Vector3 lastPos;
    private Vector3 nextPos;
    //private Quaternion lastRot;
    //private Quaternion nextRot;

    // Use this for initialization
    void Start () {
	}
	
    void Awake()
    {
        lastPos = target.transform.position;
    }
    // Update is called once per frame
    void Update () {
        if (target.activeSelf == true)
            subObject.SetActive(true);
        else
            subObject.SetActive(false);
        nextPos = target.transform.position;
        transform.position = Vector3.Slerp(transform.position, (nextPos+lastPos)/2, Time.deltaTime * 50);
        transform.rotation = Quaternion.Slerp(transform.rotation, target.transform.rotation, Time.deltaTime * 20);
        lastPos = nextPos;
    }
}
