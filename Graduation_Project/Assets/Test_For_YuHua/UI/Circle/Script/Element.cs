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
        //image.sprite = bg;
        //rawImage.texture = comp;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Pointer")
        {
            Debug.Log(selection);
        }   
    }
    public int get_selection()
    {
        return this.selection;
    }
}
