using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConstants : MonoBehaviour
{
    public static string currentPossession = "";
    public static string interactibleInRange = "";
    public static List<string> Possessable = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        currentPossession = "StartingPossession";
        Possessable.Add("StartingPossession");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
