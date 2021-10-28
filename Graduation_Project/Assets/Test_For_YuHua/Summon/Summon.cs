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
    public GameObject my_monster;

    public List<GameObject> monsterList;
    public Dictionary<int, GameObject> combineDict;

    private Pointer pt;
    // Start is called before the first frame update
    void Start()
    {
        
        FillDict();
        DrawCircle();
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
            Create(pt.getSelect(0),pt.getSelect(1));
            pt.setSelection(0);
        }
        
    }
    public void Create(int a, int b)
    {
        int select = a + b;//somealgo

        select = 1; //學長你select = a+b 有問題，我怎麼加都是零

        my_monster = Instantiate(monsterList[select]);
        InitSelect();
    }
    public void Chnage()
    {
        int a = pt.setSelection(pt.getSelection() + 1);
        pt.GetComponent<Image>().color = Color.red;
    }
    public void InitSelect()
    {
        pt.setSelect(-1, 0);
        pt.setSelect(-1, 1);
    }
    void DrawCircle()
    {
        CircleControl.GetComponent<CircularControl>().amount = monsterList.Count -1;
    }
    void FillDict()
    {
        combineDict = new Dictionary<int, GameObject>
        {
            { 0, monsterList[0] },
            { 1, monsterList[1] },
            { 2, monsterList[2] },
            { 3, monsterList[3] }
        };

    }
}
