using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UICtrl : MonoBehaviour {

    public Text text;

    //GameObject text = GameObject.Find("Text");

	// Update is called once per frame
	void Update () {
        text.text= GameObject.Find("Arrow").GetComponent<Select>().planetName;
	}
}
