using UnityEngine;
using UnityEngine.AI;

namespace Mirror.Examples.Tanks
{
    public class Tank : NetworkBehaviour
    {
        [Header("Components")]
        public NavMeshAgent agent;
        public Animator animator;

        [Header("Movement")]
        public float rotationSpeed = 100;

        [Header("Firing")]
        public KeyCode shootKey = KeyCode.Space;
        public GameObject projectilePrefab;
        public Transform projectileMount;
        public GameObject bullet = null;
        //public GameObject fireControllor;

        public override void OnStartLocalPlayer(){
            //fireControllor = GameObject.Find("FireControllor");
        }

        void Update()
        {
            // movement for local player
            if (!isLocalPlayer) return;

            // rotate
            float horizontal = Input.GetAxis("Horizontal");
            transform.Rotate(0, horizontal * rotationSpeed * Time.deltaTime, 0);

            // move
            float vertical = Input.GetAxis("Vertical");
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            agent.velocity = forward * Mathf.Max(vertical, 0) * agent.speed;
            animator.SetBool("Moving", agent.velocity != Vector3.zero);

            // shoot
            /*if (Input.GetKeyDown(shootKey))
            {
                CmdFire();
            }*/
        }

        // this is called on the server
        [Command]
        public void CmdFire()
        {
            //NetworkServer.Spawn(bullet);
            RpcOnFire();
        }

        // this is called on the tank that fired for all observers
        [ClientRpc]
        void RpcOnFire()
        {
            bullet = Instantiate(projectilePrefab, projectileMount.position, transform.rotation , transform.GetChild(0).GetChild(2));
            
            //bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
            animator.SetTrigger("Shoot");
        }

        [Command]
        public void CmdFly(){
            //bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
            RpcFly();
        }
        [ClientRpc]
        void RpcFly(){
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
        }
    }
}
