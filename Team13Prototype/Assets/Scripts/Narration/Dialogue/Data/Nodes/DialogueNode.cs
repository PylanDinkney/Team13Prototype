using UnityEngine;

public abstract class DialogueNode : ScriptableObject
{
    [SerializeField]
    private string m_Item;
    public string Item => m_Item;

    [SerializeField]
    private NarrationLine m_DialogueLine;
    public NarrationLine DialogueLine => m_DialogueLine;

    public abstract bool CanBeFollowedByNode(DialogueNode node);
    public abstract void Accept(DialogueNodeVisitor visitor);
}
