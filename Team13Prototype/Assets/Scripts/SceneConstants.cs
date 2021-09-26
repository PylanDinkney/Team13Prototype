using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneConstants : MonoBehaviour
{
    public static string currentPossession;
    public static Dictionary<string, GameObject> Possessable = new Dictionary<string, GameObject>();
    public static playerAttributes currAttr;
    public static playerAttributes otherAttr;
    public static GameObject SceneDiaUI;
    public static bool InDialouge;
    public static bool InConversation;
    public static GameObject PlayerUI;
    public static GameObject SelectionWheel;
    public static bool InSelection;
    public static List<Image> conversionBar;
}
