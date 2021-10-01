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
    private float scalex = 0.2f;
    private float scaley = 0.02f;

    // Start is called before the first frame update
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pointer.transform.localPosition = new Vector3(pointPos.x * gameObject.transform.localPosition.x ,pointPos.y * gameObject.transform.localPosition.y,gameObject.transform.localPosition.z);
    }
}
