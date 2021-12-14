using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(new Vector3(25, 0, 25), Vector3.up, 20 * Time.deltaTime);
        gameObject.transform.LookAt(new Vector3(25, 0, 25));
    }
}
