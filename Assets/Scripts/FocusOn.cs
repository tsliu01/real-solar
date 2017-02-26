using UnityEngine;
using System.Collections;

public class FocusOn : MonoBehaviour
{
    //private GameManager gameManager = GameObject.Find("Manager").GetComponent<GameManager>();

    public float cameraRevolutionSpeed = 3.0f;
    public Transform planetTrans;
    public void receiveCommand() { voiceCommand = true; }

    private Vector3 direction, moveStep;
    private string planetName;
    private float timer, distance, accelarance, movetime;
    private bool isMoving, isRotating, autoSwitch, voiceCommand;
    int count = 0;

    KeyCode m_keyCode = 0;

    // Use this for initialization
    void Start()
    {
        planetTrans = GameObject.Find("Sun").GetComponent<Transform>();
        transform.parent = planetTrans;
        transform.localPosition = new Vector3(0, 2, -10);
        transform.LookAt(planetTrans);
        isMoving = false;       //相机移动：关
        isRotating = false;     //相机自转：关
        //autoSwitch = true;      //自动切换：开
        voiceCommand = false;    //语音命令：关
        timer = 2.0f;           //初始化自动切换计时器
        movetime = 0;

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("MoveStep:" + moveStep * 100);
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown((KeyCode)10)||Input.GetKeyDown(KeyCode.Joystick1Button0) || voiceCommand == true)
        {   //按下空格键动作：切换视角，移动开，自转开，移动计时器归零
            voiceCommand = false;
            if (planetTrans.name != GameObject.Find("Arrow").GetComponent<Select>().planetTrans.name)
            {
                SwitchPlanet();
                isMoving = true;
                isRotating = true;
                //if (count == 0)
                //    autoSwitch = false;     //判断是否完成一个循环
                movetime = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape)&& planetTrans.name!="Sun")
        {
            planetTrans = GameObject.Find("Sun").GetComponent<Transform>();
            transform.parent = planetTrans;
            direction = (new Vector3(0, 2, -10) - transform.localPosition).normalized;
            distance = (new Vector3(0, 2, -10) - transform.localPosition).magnitude;
            moveStep = new Vector3(0, 0, 0);
            isMoving = true;
            isRotating = true;
        }
        if (isMoving == true || isRotating == true)     //移动开或自转开都认为是相机没到位
            CameraMove(direction, distance);
        if (isMoving == false && isRotating == false)   //移动和自转都结束则认为相机到位，公转开
        {
            //transform.LookAt(planetTrans);
            //transform.RotateAround(planetTrans.position,planetTrans.up, (cameraRevolutionSpeed + planetTrans.GetComponent<SelfRotate>().Speed) * Time.deltaTime);  //摄像机公转
        }
        if (planetTrans.name == "Sun" && isMoving == false)
            GameObject.Find("Manager").GetComponent<GameManager>().status = GameManager.Status.lookingAtSun;
        else
            GameObject.Find("Manager").GetComponent<GameManager>().status = GameManager.Status.lookingAtPlanet;
    }


    private void SwitchPlanet()
    {
        planetName = GameObject.Find("Arrow").GetComponent<Select>().planetName;
        planetTrans = GameObject.Find(planetName).GetComponent<Transform>();
        transform.parent = planetTrans;
        if (planetName == "Sun")
        {
            //direction = (Vector3.zero - transform.localPosition).normalized;
            direction = (new Vector3(0, 2, -10) - transform.localPosition).normalized;
            distance = (new Vector3(0, 2, -10) - transform.localPosition).magnitude;
        }

        else
        {
            direction = (Vector3.zero - transform.localPosition).normalized;
            distance = (Vector3.zero + new Vector3(0, 0.5f, 0) - transform.localPosition).magnitude - 1.8f;
        }   //计算相机到行星的距离，用于确定移动步长
        //accelarance = 4 * distance / 4.0f;    //加速度用于计算线性加速
        moveStep = new Vector3(0, 0, 0);
    }

    private void CameraMove(Vector3 direction, float distance)
    {
        //float speed=1.5f;
        //if (planetName == "Sun")
        //{
        //    //moveStep = (4 * transform.localPosition.normalized - transform.localPosition) * Time.deltaTime / speed;
        //    moveStep = distance / 2.0f * (Mathf.Cos(6.2831854f * (movetime += Time.deltaTime) / 2.0f) - 1) * direction * Time.deltaTime;
        //    if (transform.localPosition.magnitude >= 3.0f)
        //        isMoving = false;
        //}
        //else    //计算单帧移动步长
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
            moveStep = distance * (1 - Mathf.Cos(6.2831854f * (movetime += Time.deltaTime))) * direction * Time.deltaTime;
            /////正弦加速end

            if (movetime >= 0.8f)
            {
                isMoving = false;
                if (planetTrans.name != "Sun")
                    transform.localPosition = direction * -3f;
                else
                    transform.localPosition = new Vector3(0, 2, -10);
            }
            //Debug.Log("Delta Time: " + Time.deltaTime);
            //Debug.Log("Move Step: " + moveStep);
        }
        if (isMoving == true)
            transform.localPosition += moveStep;
        if (isRotating == true)
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(planetTrans.position - transform.position),
                movetime * 0.5f);
        if (Quaternion.Angle(transform.rotation, Quaternion.LookRotation(planetTrans.position - transform.position)) <= 1)
            isRotating = false;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10.0f, 10.0f, 300.0f, 45.0f), "运行时间：" + Time.time);
        GUI.Label(new Rect(10.0f, 30.0f, 300.0f, 45.0f), "移动时间：" + movetime);
        Event pressKey = Event.current;
        if (pressKey.isKey)
        {
            m_keyCode = pressKey.keyCode;
            Debug.Log("key:" + m_keyCode);
        }
        GUI.Label(new Rect(10.0f, 50.0f, 300.0f, 45.0f), "key:" + m_keyCode);
    }
}
