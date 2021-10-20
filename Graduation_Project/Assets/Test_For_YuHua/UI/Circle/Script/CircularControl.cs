using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircularControl : MonoBehaviour
{   

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

    public Texture[] monsterTexture;
    void Start()
    {
        
        createIMG();

    }
    // Update is called once per frame
    void Update()
    {
        float trX = pointPos.x * (scalex + 0.2f);
        float trY = pointPos.y * (scaley + 0.2f);
        pointer.transform.localPosition = new Vector3(trX, trY);
        //Debug.Log(string.Format("Local: X {0:0.00} ,Y {0:0.00}  Globe: X {0:0.00} ,Y {0:0.00}", pointer.transform.localPosition.x, pointer.transform.localPosition.y));
        //Debug.Log(selection);
    }
    
    private void createIMG()
    {
        float angle = 360/amount;
        for (int i = 0; i < amount; i++)
        {
            Vector3 loc = RotateRound(new Vector3(0.2f,0,0), new Vector3(0,0),new Vector3(0f,1f,-0.3f),angle * i);
            Quaternion qua = new Quaternion(0, 0, 90, 0);
            GameObject a =  Instantiate(element, panel.transform.position + (loc * 0.4f), qua);           
            //Create

            a.transform.rotation = Quaternion.Euler(75, 0, 0);

            //Rotate

            a.transform.localScale = new Vector3(a.transform.localScale.x * scalex, a.transform.localScale.y * scaley, a.transform.localScale.z);

            //Scle
            a.transform.SetParent(Canvas_DrawElement.GetComponentInChildren<Transform>());

            //Element setting
            Element tmp = a.GetComponent<Element>();
            tmp.selection = i;
            tmp.GetComponentInChildren<RawImage>().texture = monsterTexture[0];
        }
       
    }
    
    //round place elements
    public static Vector3 RotateRound(Vector3 position, Vector3 center, Vector3 axis, float angle)
    {
        Vector3 point = Quaternion.AngleAxis(angle, axis) * (position - center);
        Vector3 resultVec3 = center + point;
        return resultVec3;
    }
    
}
