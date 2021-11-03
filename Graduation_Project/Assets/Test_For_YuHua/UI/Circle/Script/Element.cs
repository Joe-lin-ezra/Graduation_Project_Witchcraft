using UnityEngine;
using UnityEngine.UI;

public class Element : MonoBehaviour
{
    public Sprite bg;
    public Texture comp;

    private Image image;
    private RawImage rawImage;

    public int selection;
    void Awake()
    {
        image = gameObject.GetComponent<Image>();
        rawImage = gameObject.GetComponent<RawImage>();
        gameObject.tag  = "CircleElements";
        //image.sprite = bg;
        //rawImage.texture = comp;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Pointer")
        {
            //Debug.Log(string.Format("get: {0}",selection));
        }   
    }
    public int get_selection()
    {
        return this.selection;
    }
}
