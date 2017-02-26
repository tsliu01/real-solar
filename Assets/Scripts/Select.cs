using UnityEngine;
using System.Collections;

public class Select : MonoBehaviour {
    public string planetName;
    public Transform planetTrans;
    public void receiveCommand(int index)
    {
        count = index;
        voiceCommand = true;
    }

    private int count = 0;
    private bool voiceCommand;
    private bool isMoving;
    private Vector3 direction, moveStep;
    private float moveTime, distance;
	// Use this for initialization
	void Start () {
        transform.parent = GameObject.Find("Sun").GetComponent<Transform>();
        transform.localPosition = new Vector3(0, 1f, 0);
        isMoving = false;
        voiceCommand = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            count--;
            SwitchPlanet();
            SetMove(planetName);
            isMoving = true;
            moveTime = 0;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            count++;
            SwitchPlanet();
            SetMove(planetName);
            isMoving = true;
            moveTime = 0;
        }
        if (voiceCommand == true)
        {
            voiceCommand = false;
            SwitchPlanet();
            SetMove(planetName);
            isMoving = true;
            moveTime = 0;
            GameObject.Find("Camera").GetComponent<FocusOn>().receiveCommand();
        }
        if (isMoving == true)
            ArrowMove(direction, distance);
    }

    void SwitchPlanet()
    {
        if (count > 8) count = 0;
        if (count < 0) count = 8;
        switch (count)
        {
            case 0: planetName = "Sun"; break;
            case 1: planetName = "Mercury"; break;
            case 2: planetName = "Venus"; break;
            case 3: planetName = "Earth"; break;
            case 4: planetName = "Mars"; break;
            case 5: planetName = "Jupiter"; break;
            case 6: planetName = "Saturn"; break;
            case 7: planetName = "Uranus"; break;
            case 8: planetName = "Neptune"; break;
        }
    }

    void SetMove(string planetName)
    {
        planetTrans = GameObject.Find(planetName).GetComponent<Transform>();
        transform.parent = planetTrans;
        //transform.localPosition = new Vector3(0, 1, 0);
        moveStep = new Vector3(0, 0, 0);
        direction = (Vector3.zero + new Vector3(0, 1f, 0) - transform.localPosition).normalized;
        distance = (Vector3.zero + new Vector3(0, 1f, 0) - transform.localPosition).magnitude;   //计算相机到行星的距离，用于确定移动步长
    }

    void ArrowMove(Vector3 direction, float distance)
    {
        //float speed=1.5f;

        moveStep = distance * 100 * (1 - Mathf.Cos(6.2831854f * (moveTime += Time.deltaTime) / 3.0f)) * direction * Time.deltaTime;
        /////正弦加速end

        if (moveTime>=0.2f)
        {
            isMoving = false;
            transform.localPosition = new Vector3(0, 1, 0);
        }
        //Debug.Log("Delta Time: " + Time.deltaTime);
        //Debug.Log("Move Step: " + moveStep);

        if (isMoving == true)
            transform.localPosition += moveStep;
    }
}
