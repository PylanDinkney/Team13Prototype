using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


struct Conversation
{
    public Dictionary<string, string> conversation; //formatted keys : "12" where 1 would represent choosing the first option in the first choice, 2 the second option in the second choice and so on. 
    //fomatted values : string that contains what the npc response is to the choices
    public Dictionary<int, int> choices; //key : what choice are we on starting at 1 being the first choice after the player greets this npc
    //value : how many choices for this
}

public class dialoguetest : MonoBehaviour
{

    Conversation conversation1 = new Conversation();
    bool inRange = true;
    bool inChat = true;
    private string conversationIndex = "";
    public string currentConversation = "conversation 1";
    Dictionary<string, Conversation> conversations = new Dictionary<string, Conversation>(); //name of conversation:dictionary for that conversation
    void Start()
    {
        //add choices to the conversation 
        //find how many choices are in each layer of conversation
        // find all keys, sort by length to find the amt of choices for each layer
        conversation1.choices = new Dictionary<int, int>();
        conversation1.conversation = new Dictionary<string, string>();

        //---conversation 1
        conversation1.choices[1] = 2;
        conversation1.conversation.Add("1", "this is my reponse to the player choosing the first option");
        conversation1.conversation.Add("2", "this is my reponse to the player choosing the second option");

        conversation1.choices[2] = 4;
        conversation1.conversation.Add("11", "this is my reponse to the player choosing the first option then the first option");
        conversation1.conversation.Add("12", "this is my reponse to the player choosing the second option then the second option");
        conversation1.conversation.Add("13", "this is my reponse to the player choosing the first option then the third option");
        conversation1.conversation.Add("14", "this is my reponse to the player choosing the second option then the fourth option");

        conversation1.conversation.Add("21", "this is my reponse to the player choosing the first option then the first option");
        conversation1.conversation.Add("22", "this is my reponse to the player choosing the second option then the second option");
        conversation1.conversation.Add("23", "this is my reponse to the player choosing the first option then the third option");
        conversation1.conversation.Add("24", "this is my reponse to the player choosing the second option then the fourth option");

        conversation1.choices[3] = 1;
        conversation1.conversation.Add("111", "this is my reponse last response to the player");
        conversation1.conversation.Add("121", "this is my reponse last response to the player");
        conversation1.conversation.Add("131", "this is my reponse last response to the player");
        conversation1.conversation.Add("141", "this is my reponse last response to the player");

        conversation1.conversation.Add("211", "this is my reponse last response to the player");
        conversation1.conversation.Add("221", "this is my reponse last response to the player");
        conversation1.conversation.Add("231", "this is my reponse last response to the player");
        conversation1.conversation.Add("241", "this is my reponse last response to the player");

        conversations.Add("conversation 1", conversation1);

    }

    // Update is called once per frame
    void Update()
    {
        //if (inRange && !inChat) {
        //    StartDialogue("1");
        //}
        
    }

    public void StartDialogue(string playerChoice)// event handler? takes in what the player's last response was and gives the npc response
    {
        if (conversationIndex == "141")//this option makes the npc not respond to the player
        {
            //end conversation
            return;
        }

        conversationIndex = conversationIndex + playerChoice; // append player's choice to the conversation pointer
        string message = conversations[currentConversation].conversation[conversationIndex];//get the next message that the npc is going to say

        print(message); //TODO display dialogue for the modified conversation index

        if (conversationIndex == "131") { //if the player gets the correct squence then another thing happens or they join your team
            //player chose option 131 
            //add this character to the list of playable characters
            //add to GameConstants.currentPossession list
        }
        


    }
    /*
     * called before conversations to reset the index
     * 
     */
    public void resetconversationIndex() {
        conversationIndex = "";
    }
}
