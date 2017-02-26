using UnityEngine;
using System.Collections;

public class BeingAttracted : MonoBehaviour {
    public GameObject[] m_Objects;  //另一个物体
    public Vector3 m_Speed = new Vector3(0, 0, 0);    //运动速度

    private Vector3 m_AttractiveForce;      //二者之间的引力
    private float m_Mass;     //这个物体的质量
    private float m_ForceCoefficient;     //引力系数 k*M*m

    private const float K = 750.0f;

	// Use this for initialization
	void Start () {
        m_Mass = this.GetComponent<Rigidbody>().mass;
        this.GetComponent<Rigidbody>().velocity = m_Speed;
    }
	
	// Update is called once per frame
	void Update () {
        m_Objects = GameObject.FindGameObjectsWithTag("RigidBody");
        m_AttractiveForce = new Vector3(0, 0, 0);
        foreach (GameObject attractingObject in m_Objects)
            if(attractingObject != this.gameObject)
                GetAttractiveForce(attractingObject);
        this.GetComponent<Rigidbody>().AddForce(m_AttractiveForce);
        //this.GetComponent<Rigidbody>().velocity = m_speed;

    }
    void GetAttractiveForce(GameObject attractingObject){
        Vector3 distanceVector = attractingObject.transform.position - this.transform.position;   //二者间的向量距离
        Vector3 forceDirection = distanceVector.normalized;    //引力方向
        float sqrDistance = distanceVector.sqrMagnitude;
        //if (sqrDistance != 0 && sqrDistance > 2)
        //{
        m_ForceCoefficient = K * attractingObject.GetComponent<Rigidbody>().mass * m_Mass;
        m_AttractiveForce += m_ForceCoefficient / sqrDistance * forceDirection;
        //}
        //Debug.Log("forceDirection" + forceDirection + ";distance" + distanceVector.sqrMagnitude + ";acceleration" + m_AttractiveForce);
    }
    void GetAcceleration(){
        
    }
}
