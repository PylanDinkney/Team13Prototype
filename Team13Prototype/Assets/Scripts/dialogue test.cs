using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct Conversation
{
    public Dictionary<string, string> responses; //formatted keys : "12" where 1 would represent choosing the first option in the first choice, 2 the second option in the second choice and so on. 
    //fomatted values : string that contains what the npc response is to the choices
    public Dictionary<string, string> choices; //formatted keys : "12" where 1 would represent choosing the first option in the first choice, 2 the second option in the second choice and so on. 
    //fomatted values : string that contains what the player says 
    public Dictionary<int, int> numChoices; //key : what choice are we on starting at 1 being the first choice, a choice of how the player greets this npc
    //value : how many choices for this
}

public class dialoguetest : MonoBehaviour
{

    Conversation conversation1 = new Conversation();
    bool inRange = true;
    bool inChat = true;
    private string conversationIndex = "";
    public string currentConversation = "conversation 1";
    public Dictionary<string, Conversation> conversations = new Dictionary<string, Conversation>(); //name of conversation:dictionary for that conversation
    void Start()
    {
        //add choices to the conversation 
        //find how many choices are in each layer of conversation
        // find all keys, sort by length to find the amt of choices for each layer
        conversation1.numChoices = new Dictionary<int, int>();
        conversation1.responses = new Dictionary<string, string>();

        //---conversation 1
        conversation1.numChoices[1] = 2;
        conversation1.choices.Add("1", "this is the first option the player can choose to say ;)");
        conversation1.choices.Add("2", "this is the second option the player can choose to say");
        conversation1.responses.Add("1", "this is my reponse to the player choosing the first option");
        conversation1.responses.Add("2", "this is my reponse to the player choosing the second option");

        conversation1.numChoices[2] = 4;
        conversation1.choices.Add("11", "this is the first option");
        conversation1.choices.Add("12", "this is the second option");
        conversation1.choices.Add("13", "this is the third option");
        conversation1.choices.Add("14", "this is the fourth option");
        conversation1.responses.Add("11", "this is my reponse to the player choosing the first option then the first option");
        conversation1.responses.Add("12", "this is my reponse to the player choosing the second option then the second option");
        conversation1.responses.Add("13", "this is my reponse to the player choosing the first option then the third option ;)");
        conversation1.responses.Add("14", "this is my reponse to the player choosing the second option then the fourth option");

        conversation1.choices.Add("21", "this is the first option");
        conversation1.choices.Add("22", "this is the second option");
        conversation1.choices.Add("23", "this is the third option");
        conversation1.choices.Add("24", "this is the fourth option");
        conversation1.responses.Add("21", "this is my reponse to the player choosing the first option then the first option");
        conversation1.responses.Add("22", "this is my reponse to the player choosing the second option then the second option");
        conversation1.responses.Add("23", "this is my reponse to the player choosing the first option then the third option");
        conversation1.responses.Add("24", "this is my reponse to the player choosing the second option then the fourth option");

        conversation1.numChoices[3] = 1;
        conversation1.choices.Add("111", "player's last message");
        conversation1.choices.Add("121", "player's last message");
        conversation1.choices.Add("131", "player's last message and add this npc to the cult");
        conversation1.choices.Add("141", "player's last message");
        conversation1.responses.Add("111", "this is my reponse last response to the player");
        conversation1.responses.Add("121", "this is my reponse last response to the player");
        conversation1.responses.Add("131", "I am joining the cult!!!");
        conversation1.responses.Add("141", "this is my reponse last response to the player");

        conversation1.choices.Add("211", "player's last message");
        conversation1.choices.Add("221", "player's last message");
        conversation1.choices.Add("231", "player's last message");
        conversation1.choices.Add("241", "player's last message");
        conversation1.responses.Add("211", "this is my reponse last response to the player");
        conversation1.responses.Add("221", "this is my reponse last response to the player");
        conversation1.responses.Add("231", "this is my reponse last response to the player");
        conversation1.responses.Add("241", "this is my reponse last response to the player");

        conversations.Add("conversation 1", conversation1);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // choice number is the number of choices that the player has peviously made in this conversation +1
    public List<string> DisplayChoices(int choiceNumber) { //displays a list of playerchoices for what they can say in a conversation
        int numOptions = conversations[currentConversation].numChoices[choiceNumber];
        List<string> displaychoices = new List<string>();
        for (int i = 1; i <= numOptions; i++)
        {
            string temp = conversationIndex + "" + i;//get all possible next conversation choices
            displaychoices.Add(conversations[currentConversation].choices[temp]);//get the text for each choice and store it in display choices
        }

        return displaychoices;

    }

    // player choice is the number of the option that the player choose, starts at 1
    public string RespondToPlayer(string playerChoice)// takes in what the player's last response was and gives the npc response
    {
        if (conversationIndex == "141")//this option makes the npc not respond to the player
        {
            //end conversation
            return null;
        }

        conversationIndex = conversationIndex + playerChoice; // append player's choice to the conversation pointer (remember previous choices)
        string message = conversations[currentConversation].responses[conversationIndex];//get the next message that the npc is going to say


        if (conversationIndex == "131") { //if the player gets the correct squence then another thing happens or they join your team
            //player chose option 131 
            //add this character to the list of playable characters
            //add gameObject.name to GameConstants.Possessable list
            GameConstants.Possessable.Add(gameObject.name);
        }
        return message;
    }
    /*
     * called before conversations to reset the index
     * 
     */
    public void resetconversationIndex() {
        conversationIndex = "";
    }
}
