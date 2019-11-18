using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabManager : MonoBehaviour
{
    [SerializeField] GameObject[] tabList;
    [SerializeField] Image[] tabIcons;

    private void Start()
    {
        OnClickTabButton(0);
    }

    public void OnClickTabButton(int buttonIndex)
    {
        if (buttonIndex > tabList.Length - 1) return;

        for (int i = 0; i < tabList.Length; i++)
        {
            if (i == buttonIndex)
            {
                tabList[i].SetActive(true);
                tabIcons[i].color = Color.black;
            }

            else
            {
                tabList[i].SetActive(false);
                tabIcons[i].color = Color.white;
            }
        }
    }


}
