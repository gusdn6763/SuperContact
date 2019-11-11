using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class SCInputField
{
    public static void SetImmutable(this InputField inputField, bool editMode)
    {
        if (editMode)
        {
            inputField.transform.Find("Text").GetComponent<Text>().color
                = Color.black;
        }
        else
        {
            inputField.transform.Find("Text").GetComponent<Text>().color
                = Color.white;
        }

        inputField.transform.Find("Placeholder").gameObject.SetActive(editMode);
        inputField.GetComponent<InputField>().interactable = editMode;
        inputField.GetComponent<Image>().enabled = editMode;
    }
}

public class DetailViewManager : ViewManager
{
    [SerializeField] InputField nameInputField;
    [SerializeField] InputField phoneNumberInputField;
    [SerializeField] InputField emailInputField;
    [SerializeField] Button saveButton;
    [SerializeField] GameObject thirdViewPrefab;
    [SerializeField] Image icon;
    [SerializeField] Button iconButton;
    [SerializeField] GameObject addPhotoPopupViewPrefab;

    public enum AnimationType { TYPE1, TYPE2 };

    public AnimationType animationType;

    public delegate void DetailViewManagerSaveDelegate(Contact contact);
    public DetailViewManagerSaveDelegate saveDelegate;

    public Contact? contact;

    bool editMode = true;

    void ToggleEditMode(bool updateInputField=false)
    {

        editMode = !editMode;

        // 저장버튼
        saveButton.gameObject.SetActive(editMode);

        nameInputField.SetImmutable(editMode);
        phoneNumberInputField.SetImmutable(editMode);
        emailInputField.SetImmutable(editMode);
        iconButton.interactable = (editMode);

        if (editMode)
        {
            rightNavgationViewButton.SetTitle("Cancle");
        }
        else
        {
            rightNavgationViewButton.SetTitle("Edit");

            // 데이터 화면 출력
            if (contact.HasValue && !updateInputField)
            {
                Contact contactValue = contact.Value;
                nameInputField.text = contactValue.name;
                phoneNumberInputField.text = contactValue.phoneNumber;
                emailInputField.text = contactValue.email;
                icon.sprite = contactValue.sp;
            }
        }
    }

    private void Awake()
    {
        // Title 지정
        title = "Detail screen";

        // Add 버튼 지정
        rightNavgationViewButton = Instantiate(buttonPrefab).GetComponent<SCButton>();
        rightNavgationViewButton.SetTitle("Edit");
        rightNavgationViewButton.SetOnClickAction(() =>
        {
            ToggleEditMode();
        });
        icon.preserveAspect = true;
    }

    private void Start() 
    {
        ToggleEditMode();
    }

    private void OnDestroy()
    {
        Destroy(rightNavgationViewButton.gameObject);
    }

    public void Save()
    {
        // TODO: 사용자가 입력한 정보를 바탕으로 Contact 객체 만들기
        // 만들어진 Contact 객체를 ScrollView에게 전달하면 끝!
        // 편집 모드를 해제

        Contact newContact = new Contact();
        newContact.name = nameInputField.text;
        newContact.phoneNumber = phoneNumberInputField.text;
        newContact.email = emailInputField.text;
        newContact.sp = icon.sprite;
        saveDelegate?.Invoke(newContact);

        ToggleEditMode(true);
    }

    public void Next()
    {
        ThirdViewManager thirdViewManager = 
            Instantiate(thirdViewPrefab).GetComponent<ThirdViewManager>();

        mainManager.PresentViewManager(thirdViewManager);
    }

    public void OnclickIcon()
    {
        AddPhotoPopupViewManager addPohotoPopViewManager = Instantiate(addPhotoPopupViewPrefab, this.gameObject.transform.parent.transform.parent).GetComponent<AddPhotoPopupViewManager>();
        addPohotoPopViewManager.Open(PopupViewManager.AnimationType.TYPE2);
        addPohotoPopViewManager.sprite = Test;
    }

    public void Test(Sprite sp)
    {
        icon.sprite = sp;
    }
}
