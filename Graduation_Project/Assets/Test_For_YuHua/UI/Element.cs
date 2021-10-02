using UnityEngine;
using UnityEngine.UI;

public class Element : MonoBehaviour
{
    public Sprite bg;
    public Texture comp;

    private Image image;
    private RawImage rawImage;
    void Start()
    {
        image = gameObject.GetComponent<Image>();
        rawImage = gameObject.GetComponent<RawImage>();
        image.sprite = bg;
        rawImage.texture = comp;
    }
}
