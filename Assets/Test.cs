using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Test : MonoBehaviour {

    private string[] planetNames= {"水星","金星","地球","火星","木星","土星","天王星","海王星","太阳"};

    //请输入一个字符串
    private string stringToEdit = "Please enter a string";
    // Use this for initialization
    void Update()
    {
        //当用户按下手机的返回键或home键退出游戏   
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Home))
        {
            Application.Quit();
        }
    }

    void OnGUI()
    {
        stringToEdit = GUILayout.TextField(stringToEdit, GUILayout.Width(300), GUILayout.Height(100));
        if (GUILayout.Button("login", GUILayout.Height(100)))
        {
            //让代码放置在using中是为了告诉垃圾回收站及时的回收垃圾，建议这样来写代码。
            using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    //调用Android插件中UnityTestActivity中StartActivity0方法，stringToEdit表示它的参数
                    jo.Call("initAsr");
                }

            }
        }
        if (GUILayout.Button("startRecognizer", GUILayout.Height(100)))
        {
            using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    jo.Call("speechRecognizing");
                }

            }
        }
        if (GUILayout.Button("commandRecognizer", GUILayout.Height(100)))
        {
            using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    jo.Call("commandRecognizing");
                }

            }
        }

    }
    void message(string str)
    {
        stringToEdit = str;
        if (str.Contains("离开"))
        {
            stringToEdit = "太阳";
        }
        else
        {
            foreach(string planetName in planetNames)
            {
                if (str.Contains(planetName))
                {
                    stringToEdit = planetName;
                }
            }
        }
    }
}
