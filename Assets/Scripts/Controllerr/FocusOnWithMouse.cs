using UnityEngine;
using System.Collections;

public class FocusOnWithMouse : MonoBehaviour {
    public float cameraRevolutionSpeed = 3.0f;

    private Transform planetTrans;
    private Vector3 direction, moveStep;
    private string planetName;
    private float  timer, distance, accelarance, movetime;
    private bool isMoving, isRotating, autoSwitch;
    int count = 0;

    // Use this for initialization
    void Start () {
        planetTrans = GameObject.Find("Sun").GetComponent<Transform>();
        transform.parent = planetTrans;
        isMoving = false;       //相机移动：关
        isRotating = false;     //相机自转：关
        autoSwitch = true;      //自动切换：开
        timer = 2.0f;           //初始化自动切换计时器
        movetime = 0;

    }

    // Update is called once per frame
    void Update () {
        //Debug.Log("MoveStep:" + moveStep * 100);
        if (autoSwitch == true)     //自动切换视角动作：切换视角，移动开，自转开，倒计时设置，移动计时器归零
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                count++;
                SwitchPlanet();

                isMoving = true;
                isRotating = true;
                timer = 4f;       //定时2.5s
                movetime = 0;
                if (count == 0)
                    autoSwitch = false;     //判断是否完成一个循环
            }
        }
        //else
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown((KeyCode)10))   //按下空格键动作：切换视角，移动开，自转开，移动计时器归零
            {
                count++;
                SwitchPlanet();

                isMoving = true;
                isRotating = true;
                if (count == 0)
                    autoSwitch = false;     //判断是否完成一个循环
                movetime = 0;
            }
        }
        if (isMoving == true || isRotating == true)     //移动开或自转开都认为是相机没到位
        {
            CameraMove(direction, distance);
            transform.GetComponent<MoveControllerWithMouse>().enabled = false;
        }
        if (isMoving == false && isRotating == false)   //移动和自转都结束则认为相机到位，公转开
        {
            transform.GetComponent<MoveControllerWithMouse>().enabled = true;
            //transform.LookAt(planetTrans);
            //transform.RotateAround(planetTrans.position,planetTrans.up, (cameraRevolutionSpeed + planetTrans.GetComponent<SelfRotate>().Speed) * Time.deltaTime);  //摄像机公转
        }
    }


    void SwitchPlanet()
    {
        if (count > 8) count = 0;
        switch (count)
        {
            case 0: planetName = "Sun"; break;
            case 1: planetName = "Mercury"; break;
            case 2: planetName = "Venus"; break;
            case 3: planetName = "Earth"; break;
            case 4: planetName = "Mars"; break;
            case 5: planetName = "Jupiter"; break;
            case 6: planetName = "SaturnSys"; break;
            case 7: planetName = "Uranus"; break;
            case 8: planetName = "Neptune"; break;
        }
        planetTrans = GameObject.Find(planetName).GetComponent<Transform>();
        transform.parent = planetTrans;
        if (planetName=="Sun")
            direction = (Vector3.zero - new Vector3(0, 0.5f, 0) - transform.localPosition).normalized;
        else
            direction = (Vector3.zero + new Vector3(0, 0.1f, 0) - transform.localPosition).normalized;
        distance = (Vector3.zero + new Vector3(0, 0.5f, 0) - transform.localPosition).magnitude-1.8f;   //计算相机到行星的距离，用于确定移动步长
        //accelarance = 4 * distance / 4.0f;    //加速度用于计算线性加速
        moveStep = new Vector3(0, 0, 0);
    }

    void CameraMove(Vector3 direction, float distance)
    {
        float speed = 1.5f;
        if (planetName == "Sun")
        {
            //moveStep = (4 * transform.localPosition.normalized - transform.localPosition) * Time.deltaTime / speed;
            moveStep = distance / 2.0f * (Mathf.Cos(6.2831854f * (movetime += Time.deltaTime) / 2.0f)-1) * direction * Time.deltaTime;
            if (transform.localPosition.magnitude >= 5.0f)
                isMoving = false;
        }
        else    //计算单帧移动步长
        {
            /////线性加速
            //float tempDistance = (Vector3.zero + new Vector3(0, 0.15f, 0) - transform.localPosition).magnitude - 1.0f;
            //if (tempDistance >= distance / 2)
            //    moveStep += accelarance * Time.deltaTime * Time.deltaTime * direction;
            //else
            //    moveStep -= accelarance * Time.deltaTime * Time.deltaTime * direction;
            //moveStep = (Vector3.zero + new Vector3(0, 0.15f, 0) - transform.localPosition) * Time.deltaTime / speed;
            /////线性加速end

            /////正弦加速
            moveStep = distance / 3.0f * (1 - Mathf.Cos(6.2831854f * (movetime += Time.deltaTime) / 3.0f)) * direction * Time.deltaTime;
            /////正弦加速end

            if (transform.localPosition.magnitude <= 2.0f)
                isMoving = false;
            //Debug.Log("Delta Time: " + Time.deltaTime);
            //Debug.Log("Move Step: " + moveStep);
        }
        if (isMoving == true)
            transform.localPosition += moveStep;
        if (isRotating == true)
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(planetTrans.position - transform.position),
                movetime*0.2f);
        if (Quaternion.Angle(transform.rotation, Quaternion.LookRotation(planetTrans.position - transform.position)) <= 1) 
            isRotating = false;
    }

    //void OnGUI()
    //{
    //    GUI.Label(new Rect(10.0f, 10.0f, 300.0f, 45.0f), "运行时间：" + Time.time);
    //    GUI.Label(new Rect(10.0f, 30.0f, 300.0f, 45.0f), "移动时间：" + movetime);
    //    Event pressKey = Event.current;
    //    if (pressKey.isKey)
    //    {
    //        m_keyCode = pressKey.keyCode;
    //        Debug.Log("key:" + m_keyCode);
    //    }
    //    GUI.Label(new Rect(10.0f, 50.0f, 300.0f, 45.0f), "key:" + m_keyCode);
    //}
}
