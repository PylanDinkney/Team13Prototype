using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialouge : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float textSpeed;

    public void NextDialogue(string text) {
        textComponent.text = string.Empty;
        StartCoroutine(TypeLine(text));
    }

    IEnumerator TypeLine(string text)
    {
        foreach (char c in text.ToCharArray()) {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
