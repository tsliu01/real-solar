using UnityEngine;
using System.Collections;

public class AddTense : MonoBehaviour {
    public GameObject m_GrabedObject;
    public float K = 125.0f;
         
	// Use this for initialization
	void Start () {

    }

    // Update is called once per frame
    void Update () {
        //if (this.gameObject.activeSelf == false)
        //    m_GrabedObject.SetActive(false);
        //if (this.gameObject.activeInHierarchy == true)
        //    m_GrabedObject.SetActive(true);
        //m_GrabedObject = this.GetComponent<GetRelatedObject>().m_ObjectInHierarchy;
        Vector3 distanceVector = this.transform.position - m_GrabedObject.transform.position;     //物体间的距离向量
        Vector3 forceDirection = distanceVector.normalized;     //归一化的引力方向
        float distance = distanceVector.magnitude;      //距离绝对值
        m_GrabedObject.GetComponent<Rigidbody>().AddForce(K * forceDirection * distance);
    }
}
