using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageButton : MonoBehaviour
{
    [SerializeField]  Image buttonImage;
    [SerializeField] GameObject addPhotoPopupViewPrefab;

    Button cachedButton;
    Button CachedButton
    {
        get
        {
            if(cachedButton ==null)
            {
                cachedButton = GetComponent<Button>();
            }
            return cachedButton;
        }
    }

    public Sprite Image
    {
        get
        {
            return buttonImage.sprite;
        }
        set
        {
            buttonImage.sprite = value;
        }
    }

    public bool Editable
    {
        get
        {
            return this.CachedButton.interactable;
        }
        set
        {
            this.CachedButton.interactable = value;
        }
    }

    public void OnClick()
    {
        if(addPhotoPopupViewPrefab)
        {
            AddPhotoPopupViewManager addPhotoPopupViewManager = Instantiate(addPhotoPopupViewPrefab,MainManager.instance.transform).GetComponent<AddPhotoPopupViewManager>();

            addPhotoPopupViewManager.Open(PopupViewManager.AnimationType.TYPE2);
            addPhotoPopupViewManager.didSelectImage = (sprite) =>
            {
                this.Image = sprite;
            };
        }
    }
}
