using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogIntro : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI textDisplay;
    [SerializeField] public Image dialogBox;
    [SerializeField] public GameObject continueButton;
    [SerializeField] public int howManySentences;
    [SerializeField] public string[] sentences;
    [SerializeField] public float typingSpeed;
                     private int index;

    void Start ()
    {
        StartCoroutine (Type());
    }

    void Update ()
    {
        if (textDisplay.text == sentences[index])
        {
            continueButton.SetActive (true);
        }
    }
    
    IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds (typingSpeed);
        }
    }

    public void NextSentence ()
    {
        continueButton.SetActive (false);

        if (index < sentences.Length -1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine (Type());
        }
        else
        {
            textDisplay.text = "";

            continueButton.SetActive (false);
            dialogBox.enabled = false;
        }
    }
}
