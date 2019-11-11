using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddPhotoPopupViewManager : PopupViewManager
{
    [SerializeField] ScrollRect scrollRect;

    [SerializeField] GridLayoutGroup gridLayoutGroup;

    [SerializeField] GameObject baseImageCell;

    Sprite[] sprites;


    [SerializeField] Transform content;
    [SerializeField] Image[] tempImage;
    [SerializeField] public AddPopupViewManager addPopupViewManager;

    public Action<Sprite> sprite;

    protected override void Awake()
    {
        base.Awake();

        sprites = Resources.LoadAll<Sprite>("photo");

        for(int i =0; i< sprites.Length;i++)
        {
            ImageScript imagescript = Instantiate(baseImageCell, content.transform).GetComponent<ImageScript>();
            imagescript.image +=OnClickIcon;
        }

        tempImage = content.transform.GetComponentsInChildren<Image>();

        for (int i = 0; i < sprites.Length; i++)
        {
            tempImage[i+1].sprite = sprites[i];
            tempImage[i+1].preserveAspect = true;
        }
        float cellHeight = (gridLayoutGroup.cellSize.y + gridLayoutGroup.spacing.y) * (sprites.Length/gridLayoutGroup.constraintCount)        
        +gridLayoutGroup.padding.top + gridLayoutGroup.padding.bottom;
        scrollRect.content.sizeDelta = new Vector2(0, cellHeight);
    }

    public void OnclickClose()
    {
        Close(AnimationType.TYPE2);
    }

    public void OnClickIcon(Sprite sp)
    {
        sprite(sp);       
        Close(AnimationType.TYPE2);
    }
}
