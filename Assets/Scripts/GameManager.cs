using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManager : MonoBehaviour {

    //三种状态：注视行星，注视太阳，空闲
    public enum Status
    {
        lookingAtPlanet,
        lookingAtSun,
        idle
    };
    public Status status = Status.lookingAtSun;

    private string[] planetNames = { "太阳", "水星", "金星", "地球", "火星", "木星", "土星", "天王星", "海王星" };
    private string[] planetNames_eng = { "Sun", "Mercury", "Venus", "Earth", "Mars", "Jupiter", "Saturn", "Uranus", "Neptune" };
    private string name;
    private string stringToEdit = "Please enter a string";

    void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.fullScreen = true;
        //Screen.SetResolution(1600, 600, true);
        Cursor.visible = false;

        ////////////////////
        //using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        //{
        //    using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
        //    {
        //        jo.Call("speechRecognizing");
        //    }

        //}
        //////////////
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && status == Status.lookingAtSun)
        {
            status = Status.idle;
            Cursor.visible = true;
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && status == Status.idle)
        {
            Cursor.visible = true;
            //Application.Quit();
        }
        if (Input.GetKeyDown((KeyCode)130)||Input.GetKeyDown(KeyCode.Menu))
        {
            GetCommand();
        }

    }

    //void AsrInit()
    //{
    //    //让代码放置在using中是为了告诉垃圾回收站及时的回收垃圾，建议这样来写代码。
    //    using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
    //    {
    //        using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
    //        {
    //            //调用Android插件中UnityTestActivity中StartActivity0方法，stringToEdit表示它的参数
    //            jo.Call("initAsr");
    //        }
    //    }
    //}

    void message(string str)
    {
        stringToEdit = str;
        GameObject.Find("Camera").GetComponent<FocusOn>().receiveCommand();
        Debug.Log("Copy message0:" + stringToEdit);

        if (str.Contains("离开"))
        {
            Debug.Log("Copy message1:" + stringToEdit);

            GameObject.Find("Arrow").GetComponent<Select>().receiveCommand(0);
        }
        else
        {
            Debug.Log("Copy message2:" + stringToEdit);

            foreach (string planetName in planetNames)
            {
                if (str.Contains(planetName))
                {
                    stringToEdit = str;
                    Debug.Log("Copy message3:" + stringToEdit);

                    GameObject.Find("Arrow").GetComponent<Select>().receiveCommand(System.Array.IndexOf(planetNames, planetName));
                    name = planetNames_eng[System.Array.IndexOf(planetNames, planetName)];
                    stringToEdit = System.Array.IndexOf(planetNames, planetName).ToString();
                    Debug.Log("Copy message4:" + name);
                }
            }
        }
    }

    void GetCommand()
    {
        using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                jo.Call("commandRecognizing");
            }

        }
    }
    void OnGUI()
    {
        stringToEdit = GUILayout.TextField(stringToEdit, GUILayout.Width(300), GUILayout.Height(30));
        GUI.Label(new Rect(10.0f, 10.0f, 300.0f, 45.0f), "status：" + status);
        GUI.Label(new Rect(10.0f, 30.0f, 300.0f, 45.0f), "planet：" + name);

        //if (GUILayout.Button("speechRecognizing", GUILayout.Height(100)))
        //{
        //    UnityEngine.Debug.Log("UnityStartRecognizing");
        //    using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        //    {
        //        using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
        //        {
        //            jo.Call("speechRecognizing");
        //        }
        //    }
        //}
        if (GUILayout.Button("commandRecognizer", GUILayout.Height(100)))
        {
            GetCommand();
        }

    }

}
