using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneConstants : MonoBehaviour
{
    public static int currentPossession = 0;
    public static List<string> Possessable = new List<string>();
    public static playerAttributes currAttr;
    public static playerAttributes otherAttr;
    public static GameObject SceneDiaUI;
    public static bool InDialouge;
    public static bool InConversation;
    public static List<Image> conversionBar;
}
