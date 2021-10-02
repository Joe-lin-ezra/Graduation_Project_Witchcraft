using UnityEngine;
using UnityEngine.UI;

public class Element : MonoBehaviour
{
    public Sprite bg;
    public Texture comp;

    private Image image;
    private RawImage rawImage;

    private Collider collider;
    public int selection;
    void Start()
    {
        image = gameObject.GetComponent<Image>();
        rawImage = gameObject.GetComponent<RawImage>();
        collider = gameObject.GetComponent<Collider>();
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
}
