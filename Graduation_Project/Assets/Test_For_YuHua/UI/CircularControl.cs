using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircularControl : MonoBehaviour
{   //Data
    public GameObject[] dataArr;
    public string[] dataString;

    //posision
    public Vector2 pointPos;
    public Image pointer;
    private float scalex = 0.2f;
    private float scaley = 0.2f;
    public int selection;

    //elements
    public GameObject element;
    public int amount;
    public GameObject Canvas_DrawElement;
    public GameObject panel;
    
    void Start()
    {
        amount = 5;
        createIMG();

    }

    // Update is called once per frame
    void Update()
    {
        float trX = pointPos.x * scalex;
        float trY = pointPos.y * scaley;
        pointer.transform.localPosition = new Vector3(trX, trY);
        //Debug.Log(string.Format("Local: X {0:0.00} ,Y {0:0.00}  Globe: X {0:0.00} ,Y {0:0.00}", pointer.transform.localPosition.x, pointer.transform.localPosition.y));
        //Debug.Log(selection);
    }
    private void createIMG()
    {
        float angle = 360/amount;
        Debug.Log(angle);
        for (int i = 0; i < amount; i++)
        {
            Vector3 loc = RotateRound(new Vector3(0.2f,0,0), new Vector3(0,0),new Vector3(0,1,0),angle * i);
            Quaternion qua = new Quaternion(-45, 0, 0, 0);
            GameObject a =  Instantiate(element, pointer.transform.localPosition + loc, qua);
            a.GetComponent<Element>().selection = i;
            
            //Create

            a.transform.rotation = Quaternion.Euler(90, 0, 0);

            //Rotate

            a.transform.localScale = new Vector3(a.transform.localScale.x * scalex, a.transform.localScale.y * scaley, a.transform.localScale.z);

            //Scle
            a.transform.SetParent(Canvas_DrawElement.GetComponentInChildren<Transform>());
        }
       
    }
    public static Vector3 RotateRound(Vector3 position, Vector3 center, Vector3 axis, float angle)
    {
        Vector3 point = Quaternion.AngleAxis(angle, axis) * (position - center);
        Vector3 resultVec3 = center + point;
        return resultVec3;
    }
    private void scaleElement(GameObject g)
    {

    }
    //private int findPos(double pt1,double pt2,double x1,double x2,double y1,double y2)
    //{
    //    double tmp = (y1 - y2) * pt1 + (x2 - x1) * pt2 + x1 * y2 - x2 * y1;
    //    if(tmp > 0) return 1;
    //    if(tmp < 0) return -1;
    //    return 0;
    //}
    //private int Locate(Vector2 now)
    //{
    //    // 8 selection with one error
    //    if(now.x >= 0 && now.y > 0) //section1
    //    {
    //        if(findPos(now.x,now.y,0,1,0,1) < 0)return 0;
    //        return 1;
    //    }
    //    else if (now.x < 0 && now.y >= 0)//section2
    //    {
    //        if(findPos(now.x, now.y, 0, 1, 0, 1) < 0)return 2;
    //        return 3;
    //    }
    //    else if (now.x <= 0 && now.y < 0)//section3
    //    {
    //        if(findPos(now.x, now.y, 0, 1, 0, 1) < 0)return 4;
    //        return 5;

    //    }
    //    else if (now.x > 0 && now.y <= 0)//section4
    //    {
    //        if(findPos(now.x, now.y, 0, 1, 0, 1) < 0)return 6;
    //        return 7;
    //    }
    //    return 8;
    //}
}
