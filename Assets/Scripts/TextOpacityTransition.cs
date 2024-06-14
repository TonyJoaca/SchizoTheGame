using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextOpacityTransition : MonoBehaviour
{
    static bool _start = false;
    public static bool StartTransition {get {return _start;} set {_start = value;}}
    Color _transparent = new(1,1,1,0);
    Color _curColor;
    // Start is called before the first frame update
    void Start()
    {
        _curColor = _transparent;
    }

    // Update is called once per frame
    void Update()
    {
        if(_start){
            StartCoroutine(OpacityChange());
            _start = false;
        }
        GetComponent<Text>().color = Color.Lerp(GetComponent<Text>().color, _curColor, 0.8f * Time.deltaTime);
    }
    IEnumerator OpacityChange(){
        _curColor = Color.white;
        GetComponent<Text>().color = Color.white;
        yield return new WaitForSeconds(2f);
        _curColor = _transparent;
    }
}
