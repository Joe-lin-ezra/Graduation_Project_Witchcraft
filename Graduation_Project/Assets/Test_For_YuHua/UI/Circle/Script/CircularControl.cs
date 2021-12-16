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
    public GameObject[] elements;
 
    void Start()
    {
        
        CreateIMG();

    }
    // Update is called once per frame
    void Update()
    {
        float trX = pointPos.x * (scalex + 0.2f);
        float trY = pointPos.y * (scaley + 0.2f);
        pointer.transform.localPosition = new Vector3(trX, trY);//control point

        
    }
    
    private void CreateIMG()
    {

        for(int i = 0; i < amount; i++)
        {
            GameObject el = elements[i];
            Element tmp = el.GetComponent<Element>();
            tmp.selection = i;
            tmp.GetComponentInChildren<RawImage>().texture = monsterTexture[i];

        }

        /*
         * float angle = 360/amount;
        for (int i = 0; i < amount; i++)
        {
              Vector3 loc = RotateRound(new Vector3(0.2f, 0, 0), new Vector3(0, 0), new Vector3(0f, 1f, -0.3f), angle * i);
        Quaternion qua = new Quaternion(0, 15, 75, 0);
        GameObject a = Instantiate(element, panel.transform.position + (loc * 0.4f), qua);
        a.transform.rotation = Quaternion.Euler(0, 0, 0);

        a.transform.localScale = new Vector3(a.transform.localScale.x * scalex, a.transform.localScale.y * scaley, a.transform.localScale.z);

        a.transform.SetParent(Canvas_DrawElement.GetComponentInChildren<Transform>());
        Element tmp = a.GetComponent<Element>();
        tmp.selection =i;
        tmp.GetComponentInChildren<RawImage>().texture = monsterTexture[i];
        }*/

    }

    //round place elements
    public static Vector3 RotateRound(Vector3 position, Vector3 center, Vector3 axis, float angle)
    {
        Vector3 point = Quaternion.AngleAxis(angle, axis) * (position - center);
        Vector3 resultVec3 = center + point;
        return resultVec3;
    }
    
}
