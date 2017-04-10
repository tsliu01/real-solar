using UnityEngine;
using System.Collections;

public class CursorActivity : MonoBehaviour
{
    public bool isSelecting;
    public GameObject selectedIcon;
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        //Ray ray = Camera.main.ScreenPointToRay(transform.forward);//从摄像机发出到点击坐标的射线
        RaycastHit hitInfo;
        if(Physics.Raycast(ray, out hitInfo))
        {
            Debug.DrawLine(ray.origin, hitInfo.point);//划出射线，只有在scene视图中才能看到
            selectedIcon = hitInfo.collider.gameObject;
            Debug.Log("click object name is " + selectedIcon.name);
            isSelecting = true;
            //if (icon.tag == "icon")//当射线碰撞目标为icon类型的物品 ，执行操作
            //{

            //}
        }
        else
        {
            isSelecting = false;
        }
    }
}