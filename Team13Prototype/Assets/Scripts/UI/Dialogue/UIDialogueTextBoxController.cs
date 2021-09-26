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
        foreach (Button button in SceneConstants.SceneDiaUI.GetComponentsInChildren<Button>())
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

        foreach (Button button in SceneConstants.SceneDiaUI.GetComponentsInChildren<Button>())
        {
            if (button.name == "TalkButton")
                button.interactable = true;
            if (button.name == "ConvertButton")
            {
                if (!SceneConstants.otherAttr.IsConverted)
                {
                    int level = 0;
                    if (SceneConstants.currAttr.Item != null)
                    {
                        if (SceneConstants.currAttr.Item == SceneConstants.otherAttr.ItemWeakness)
                            level += 2;
                    }
                    if (SceneConstants.currAttr.Trait == SceneConstants.otherAttr.Trait)
                        level += 1;
                    else if (SceneConstants.currAttr.TraitWeakness == SceneConstants.otherAttr.Trait)
                        level -= 1;
                    else if (SceneConstants.currAttr.Trait == SceneConstants.otherAttr.TraitWeakness)
                        level += 2;

                    if (level >= SceneConstants.otherAttr.ConversionThreshold)
                        button.interactable = true;
                }
            }
            if (button.name == "GiveButton")
            {
                if (SceneConstants.otherAttr.IsConverted && (SceneConstants.currAttr.Item != null && SceneConstants.currAttr.Item != "")
                    && (SceneConstants.otherAttr.Item == null || SceneConstants.otherAttr.Item == ""))
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

        if ((node.Item != null && node.Item != ""))
        {
            SceneConstants.currAttr.Item = node.Item;

            foreach (TextMeshProUGUI text in SceneConstants.SceneDiaUI.GetComponentsInChildren<TextMeshProUGUI>())
            {
                if (text.name == "CharacterItem")
                {
                    text.text = "Item: " + SceneConstants.currAttr.Item;
                    break;
                }
            }

            foreach (Button button in SceneConstants.SceneDiaUI.GetComponentsInChildren<Button>())
            {
                if (button.name == "GiveButton")
                    button.interactable = true;
            }
        }
    }

    public void Visit(ChoiceDialogueNode node)
    {
        m_ChoicesBoxTransform.gameObject.SetActive(true);

        foreach (DialogueChoice choice in node.Choices)
        {
            UIDialogueChoiceController newChoice = Instantiate(m_ChoiceControllerPrefab, m_ChoicesBoxTransform);
            newChoice.Choice = choice;
        }

        if ((node.Item != null && node.Item != ""))
        {
            SceneConstants.currAttr.Item = node.Item;

            foreach (TextMeshProUGUI text in SceneConstants.SceneDiaUI.GetComponentsInChildren<TextMeshProUGUI>())
            {
                if (text.name == "CharacterItem")
                {
                    text.text = "Item: " + SceneConstants.currAttr.Item;
                    break;
                }
            }

            foreach (Button button in SceneConstants.SceneDiaUI.GetComponentsInChildren<Button>())
            {
                if (button.name == "GiveButton")
                    button.interactable = true;
            }
        }
    }
}