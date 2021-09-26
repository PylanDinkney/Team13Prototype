using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public Vector2 normalMousePos;
    public float currentAngle;
    public int selection;
    private int previousSelection;

    public GameObject[] menuItems;

    private MenuItemScript menuItemSc;
    private MenuItemScript prevMenuItemSc;

    void Update()
    {
        normalMousePos = new Vector2(Input.mousePosition.x - Screen.width / 2, Input.mousePosition.y - Screen.height / 2);

        currentAngle = Mathf.Atan2(normalMousePos.y, normalMousePos.x) * Mathf.Rad2Deg;
        currentAngle = (currentAngle + 360) % 360;

        selection = (int)currentAngle / 90;

        if(selection != previousSelection)
        {
            prevMenuItemSc = menuItems[previousSelection].GetComponent<MenuItemScript>();
            prevMenuItemSc.Deselect();
            previousSelection = selection;

            menuItemSc = menuItems[selection].GetComponent<MenuItemScript>();
            menuItemSc.Select();
        }

        if (SceneConstants.InSelection && Input.GetMouseButtonDown(0) && !(normalMousePos.x > 740.0 && normalMousePos.y > 333))
        {
            if (menuItems[selection].GetComponent<MenuItemScript>().isActive)
            {
                SceneConstants.currentPossession = menuItems[selection].GetComponent<MenuItemScript>().charName;
                GameObject character = SceneConstants.Possessable[SceneConstants.currentPossession];
                GameObject.Find("Main Camera").GetComponent<CameraFollow>().target = character.transform;
                SceneConstants.currentPossession = menuItems[selection].GetComponent<MenuItemScript>().charName;
                SceneConstants.currAttr = character.GetComponent<playerAttributes>();

                foreach (TextMeshProUGUI text in SceneConstants.PlayerUI.GetComponentsInChildren<TextMeshProUGUI>())
                {
                    if (text.name == "PlayerName")
                        text.text = SceneConstants.currAttr.CharName;
                    if (text.name == "PlayerItem")
                        text.text = "Item: " + SceneConstants.currAttr.Item;
                }
                foreach (Image portrait in SceneConstants.PlayerUI.GetComponentsInChildren<Image>())
                {
                    if (portrait.name == "PlayerPortrait")
                    {
                        portrait.sprite = SceneConstants.currAttr.Portrait;
                        break;
                    }
                }

                SceneConstants.SelectionWheel.SetActive(false);
                SceneConstants.InSelection = false;
            }
        }
    }
}
