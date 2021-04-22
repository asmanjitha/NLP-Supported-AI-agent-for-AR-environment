using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KyleChat : MonoBehaviour
{
    public float delay = 0.1f;
    public bool showAnimation = true;
    public bool typing = false;
    public TextMeshPro  text1;
	// public string fullText;
	// private string currentText = "";

	// Use this for initialization
	void Start () {
		// DefaultTypingAnimation(text1);
	}

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(showAnimation){
            if(!typing){
                StartCoroutine(TypingAnimation(".....", text1));
            }
        }
    }
	
	IEnumerator ShowText(string str, TextMeshPro   text){
        string currentText = "";
		for(int i = 0; i < str.Length + 1; i++){
			currentText = str.Substring(0,i);
			text.text = currentText;
			yield return new WaitForSeconds(delay);
		}
	}

    IEnumerator TypingAnimation(string str, TextMeshPro   text){
        typing = true;
        string currentText = "";
		for(int i = 0; i < str.Length; i++){
			currentText = str.Substring(0,i);
			text.text = currentText;
			yield return new WaitForSeconds(delay);
		}
        typing = false;
	}

    public void TypeWrite(string str){
        showAnimation = false;
        // text1.fontSize = 50;
        delay = 0.1f;
        StartCoroutine(ShowText(str, text1));
    }

    public void ShowTypingAnimation(){
        // text1.fontSize = 100;
        delay = 0.2f;
        showAnimation = true;
    }

}
