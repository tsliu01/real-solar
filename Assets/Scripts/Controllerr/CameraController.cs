using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Quaternion orientation;
    public Vector3 m_EulerAngles;
    private string str;
    private Quaternion originOrientation;
    private Quaternion referanceRotation = Quaternion.identity;
    private Quaternion baseOrientation = Quaternion.Euler(90, 0, 0);
    private Quaternion calibration = Quaternion.identity;
    private Quaternion cameraBase = Quaternion.identity;
    private Quaternion currentOrientation;


    AndroidJavaClass jc;
    AndroidJavaObject jo;

    // Use this for initialization
    void Start () {
        originOrientation = transform.rotation;
        //RecalculateReferenceRotation();
        jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
    }

    // Update is called once per frame
    void Update()
    {
        currentOrientation = Input.gyro.attitude;
        //using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        //{
        //    using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
        //    {
        try
        {
            jo.Call("getOrientation");
        }catch(UnityException e)
        {
            Debug.Log("ExMsg:" + e.Message);
        }
        //    }
        //}
    }

	void LateUpdate () {
        //transform.Rotate(new Vector3(orientation.x, orientation.y, orientation.z), Mathf.Acos(orientation.w * 180 / Mathf.PI));
        transform.rotation = orientation;
        //orientation = transform.rotation;
        //transform.eulerAngles = m_EulerAngles;
    }

    private void OnGUI()
    {
        GUILayout.Box(transform.eulerAngles.ToString() + transform.rotation.ToString(), GUILayout.Width(300), GUILayout.Height(100));
        if (GUILayout.Button("send current orientaion", GUILayout.Height(100)))
        {
            Debug.Log("gyroscope: " + currentOrientation);
            sendCurrentOrientation(new Quaternion(0, 0, 0, 1));
        }

    }

    private void sendCurrentOrientation(Quaternion currentOrientation)
    {
        float[] orientation=new float[4];
        orientation[0] = currentOrientation.x;
        orientation[1] = currentOrientation.y;
        orientation[2] = currentOrientation.z;
        orientation[3] = currentOrientation.w;
        jo.Call("setCurrentOrientation", orientation);
    }

    private static Quaternion ConvertRotation(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }

    private void RecalculateReferenceRotation()
    {
        referanceRotation = Quaternion.Inverse(baseOrientation) * Quaternion.Inverse(calibration);
    }

    void ReceiveX(string message)
    {
        orientation.z = -float.Parse(message);
    }

    void ReceiveY(string message)
    {
        orientation.x = -float.Parse(message);
    }

    void ReceiveZ(string message)
    {
        orientation.y = -float.Parse(message);
    }

    void ReceiveW(string message)
    {
        orientation.w = float.Parse(message);
    }

}