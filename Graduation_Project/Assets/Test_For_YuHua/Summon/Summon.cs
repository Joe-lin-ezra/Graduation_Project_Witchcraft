using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//relation with monster things
public class Summon : MonoBehaviour
{
    public GameObject leftHand;
    public GameObject pointer;
    public GameObject player;
    public GameObject CircleControl;

    public List<GameObject> monsterList;

    private Pointer pt;
    // Start is called before the first frame update
    void Start()
    {
        drawCircle();
    }

    // Update is called once per frame
    void Update()
    {
        
        pt = pointer.GetComponent<Pointer>();
        CheckMonster(pt);


    }
    public void CheckMonster(Pointer pt)
    {
        if(pt.getSelection() == 2)
        {
            create(pt.getSelect(0),pt.getSelect(1));
            pt.setSelection(0);
        }
        
    }
    public void create(int a, int b)
    {
        int select = a + b;//somealgo
        Instantiate(monsterList[select]);
        initSelect();
    }
    public void chnage()
    {
        int a = pt.setSelection(pt.getSelection() + 1);
        pt.GetComponent<Image>().color = Color.red;
    }
    public void initSelect()
    {
        pt.setSelect(0, 0);
        pt.setSelect(0, 1);
    }
    void drawCircle()
    {
        CircleControl.GetComponent<CircularControl>().amount = monsterList.Count;
    }
}
