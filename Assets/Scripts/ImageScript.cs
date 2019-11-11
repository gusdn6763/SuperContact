using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageScript : MonoBehaviour
{
    public Action<Sprite> image;

    public void OnClickImage()
    {
        image(this.gameObject.GetComponent<Image>().sprite);
    }

}
