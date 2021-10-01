using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircularControl : MonoBehaviour
{
    public GameObject[] dataArr;
    public string[] dataString;

    public Vector2 pointPos;
    public Image pointer;
    private float scalex = 0.5f;
    private float scaley = 0.5f;
    public int selection;

    // Start is called before the first frame update
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float trX = pointPos.x * scalex;
        float trY = pointPos.y * scaley;
        pointer.transform.localPosition = new Vector3(trX, trY, gameObject.transform.localPosition.z);
        //Debug.Log(string.Format("Local: X {0:0.00} ,Y {0:0.00}  Globe: X {0:0.00} ,Y {0:0.00}", pointer.transform.localPosition.x, pointer.transform.localPosition.y));
        
    }
}
