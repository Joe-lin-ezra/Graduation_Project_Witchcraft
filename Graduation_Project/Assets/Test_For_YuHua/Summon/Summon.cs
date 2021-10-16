using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//relation with monster things
public class Summon : MonoBehaviour
{
    public GameObject leftHand;
    public GameObject pointer;
    public GameObject player;

    public List<GameObject> monsterList;

    private Pointer pt;
    // Start is called before the first frame update
    void Start()
    {
        monsterList = new List<GameObject>();        
    }

    // Update is called once per frame
    void Update()
    {
        
        pt = pointer.GetComponent<Pointer>();
        
    }
    public void CheckMonster(Pointer pt)
    {
        int[] select = pt.getSelect();
        if(select[0] != -1)
        {
            if(select[1] != -1)
            {
                Debug.Log(string.Format("A = {0}, B = {1}", select[0], select[1]));
                //create(select[0], select[1]);
                select[0] = -1;
                select[1] = -1;
            }
        }
        
    }
    public void create(int a, int b)
    {
        int select = a + b;//somealgo
        Instantiate(monsterList[select]);
    }
}
