using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDialogueTextBoxController : MonoBehaviour, DialogueNodeVisitor
{
    [SerializeField]
    private TextMeshProUGUI m_DialogueText;

    [SerializeField]
    private RectTransform m_ChoicesBoxTransform;
    [SerializeField]
    private UIDialogueChoiceController m_ChoiceControllerPrefab;

    [SerializeField]
    private DialogueChannel m_DialogueChannel;

    private bool m_ListenToInput = false;
    private DialogueNode m_NextNode = null;

    private void Awake()
    {
        m_DialogueChannel.OnDialogueNodeStart += OnDialogueNodeStart;
        m_DialogueChannel.OnDialogueNodeEnd += OnDialogueNodeEnd;

        m_ChoicesBoxTransform.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        m_DialogueChannel.OnDialogueNodeEnd -= OnDialogueNodeEnd;
        m_DialogueChannel.OnDialogueNodeStart -= OnDialogueNodeStart;
    }

    private void Update()
    {
        if (m_ListenToInput && Input.GetMouseButtonDown(0))
        {
            m_DialogueChannel.RaiseRequestDialogueNode(m_NextNode);
        }
    }

    private void OnDialogueNodeStart(DialogueNode node)
    {
        gameObject.SetActive(true);
        SceneConstants.InConversation = true;
        foreach (UnityEngine.UI.Button button in SceneConstants.SceneDiaUI.GetComponentsInChildren<UnityEngine.UI.Button>())
        {
            button.interactable = false;
        }

        m_DialogueText.text = node.DialogueLine.Text;

        node.Accept(this);
    }

    private void OnDialogueNodeEnd(DialogueNode node)
    {
        m_NextNode = null;
        m_ListenToInput = false;
        SceneConstants.InConversation = false;

        GameObject curr = null;
        GameObject other = null;
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Character"))
        {
            if (player.GetComponent<playerAttributes>().CharName == SceneConstants.Possessable[SceneConstants.currentPossession])
                curr = player;
            else if (player == SceneConstants.DiaCharacter)
                other = player;
        }

        playerAttributes currAttr = curr.GetComponent<playerAttributes>();
        playerAttributes otherAttr = other.GetComponent<playerAttributes>();

        foreach (UnityEngine.UI.Button button in SceneConstants.SceneDiaUI.GetComponentsInChildren<UnityEngine.UI.Button>())
        {
            if (button.name == "TalkButton")
                button.interactable = true;
            if (button.name == "ConvertButton")
            {
                if (!otherAttr.IsConverted)
                {
                    int level = 0;
                    if (currAttr.Item == otherAttr.ItemWeakness)
                        level += 2;
                    if (currAttr.Trait == otherAttr.Trait)
                        level += 1;
                    else if (currAttr.TraitWeakness == otherAttr.Trait)
                        level -= 1;
                    else if (currAttr.Trait == otherAttr.TraitWeakness)
                        level += 2;

                    if (level >= otherAttr.ConversionThreshold)
                        button.interactable = true;
                }
            }
            if (button.name == "GiveButton")
            {
                if (otherAttr.IsConverted && currAttr.Item != "" && otherAttr.Item == "")
                    button.interactable = true;
            }
        }

        foreach (Transform child in m_ChoicesBoxTransform)
        {
            Destroy(child.gameObject);
        }

        m_ChoicesBoxTransform.gameObject.SetActive(false);
    }

    public void Visit(BasicDialogueNode node)
    {
        m_ListenToInput = true;
        m_NextNode = node.NextNode;
    }

    public void Visit(ChoiceDialogueNode node)
    {
        m_ChoicesBoxTransform.gameObject.SetActive(true);

        foreach (DialogueChoice choice in node.Choices)
        {
            UIDialogueChoiceController newChoice = Instantiate(m_ChoiceControllerPrefab, m_ChoicesBoxTransform);
            newChoice.Choice = choice;
        }
    }
}