using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

namespace Mirror.Examples.Tanks
{
    public class FireControllor : MonoBehaviour
    {
        public GameObject tank = null;
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
                        // shoot
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    tank = NetworkClient.localPlayer.gameObject;
                    tank.GetComponent<Tank>().CmdFire();
                    /*if (tank == null){
                            tank = NetworkClient.localPlayer.gameObject;
                            tank.GetComponent<Tank>().CmdFire();                       
                        }

                    else
                        tank.GetComponent<Tank>().CmdFire();*/
                }
                if( Input.GetKeyDown(KeyCode.Mouse0) && tank.GetComponent<Tank>().bullet != null){
                    tank.GetComponent<Tank>().CmdFly();
                }
        }
    }
}
