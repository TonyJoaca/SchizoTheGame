using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhantomTriggerWalls : MonoBehaviour
{
    [SerializeField] GameObject _phantomLia;
    [SerializeField] string _textString;
    [SerializeField] Text _textBox;
    [SerializeField] float _textDelay;
    private void OnTriggerEnter(Collider other) {
        if(_phantomLia.activeInHierarchy){
            _phantomLia.GetComponent<AnimatorController>().PhantomCanAdvance = true;
            StartCoroutine(TextToScreen());
        }
    }
    IEnumerator TextToScreen(){
        _textBox.text = _textString;
        yield return new WaitForSeconds(_textDelay);
        _textBox.text = "";
        Destroy(gameObject);
    }
}
