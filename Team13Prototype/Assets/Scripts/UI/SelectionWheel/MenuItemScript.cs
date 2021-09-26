using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuItemScript : MonoBehaviour
{
    public Color hoverColor;
    public Color baseColor;
    public Image background;
    public bool isActive;
    public string charName;
    public TextMeshProUGUI itemText;

    void Start()
    {
        background.color = baseColor;
    }

    public void Select()
    {
        if (isActive)
            background.color = hoverColor;
    }

    public void Deselect()
    {
        background.color = baseColor;
    }
}
