using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroDialogue : MonoBehaviour
{
    Text _dialogue;
    [SerializeField] string[] _introDialogue;
    // Start is called before the first frame update
    void Start()
    {
        _dialogue = GetComponent<Text>();
        StartCoroutine(TextToScreen());
    }

    IEnumerator TextToScreen(){
        foreach(string _text in _introDialogue){
            _dialogue.text = _text;
            yield return new WaitForSeconds(4.5f);
        }
    }
}
