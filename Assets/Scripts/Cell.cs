using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface ICell
{
    void DidSelectCell(Cell cell);
    void DidSelectDeleteCell(Cell cell);
}

public class Cell : MonoBehaviour, IComparable<Cell>
{
    [SerializeField] public Image iconImage;
    [SerializeField] Text name;
    [SerializeField] Text phoneNumber;
    [SerializeField] Text email;
    [SerializeField] Button deleteButton;

    public ICell cellDelegate;
    Button cellButton;

    private void Awake()
    {
      
    }

    public int CompareTo(Cell other)
    {
        return this.name.text.CompareTo(other.name.text);
    }

    public string Name
    {
        get
        {
            return this.name.text;
        }
        set
        {
            // title에 대한 유효성 체크
            this.name.text = value;
        }
    }

    public string PhoneNuber
    {
        get
        {
            return this.phoneNumber.text;
        }
        set
        {
            this.phoneNumber.text = value;
        }
    }

    public string Email
    {
        get
        {
            return this.email.text;
        }
        set
        {
            this.email.text = value;
        }
    }

    public Image Icon
    {
        get
        {
            return this.iconImage;
        }
        set
        {
            this.iconImage = value;
        }
    }

    public bool ActiveDelete
    {
        get 
        {
            return deleteButton.gameObject.activeSelf;
        }
        set
        {
            deleteButton.gameObject.SetActive(value);

            if (value)
            {
                cellButton.interactable = false;
            }
            else
            {
                cellButton.interactable = true;
            }

//            cellButton.interactable = !value;
        }
    }

    private void Start() 
    {
        cellButton = GetComponent<Button>();
        this.ActiveDelete = false;
        iconImage.preserveAspect = true;
    }

    public void OnClick()
    {
        cellDelegate.DidSelectCell(this);
    }

    public void OnClickDelete()
    {
        cellDelegate.DidSelectDeleteCell(this);
    }


}
