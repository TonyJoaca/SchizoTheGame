using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BlackScreenTransition : MonoBehaviour
{
    bool _canDoTransition = false;
    public bool CanDoTransition {set {_canDoTransition = value;}}
    [SerializeField] float _speed = 0.5f;
    Image _image;
    Color _curColor;
    [SerializeField] float _delay = 3.2f;
    [SerializeField] bool _changeScene = false;
    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
        _curColor = _image.color;
    }

    // Update is called once per frame
    void Update()
    {
        _image.color = Color.Lerp(_image.color, _curColor, _speed * Time.deltaTime);
        if(_canDoTransition)
            StartCoroutine(StartTransition());
    }

    IEnumerator StartTransition(){
        _canDoTransition = false;
        _curColor = Color.black;
        yield return new WaitForSeconds(_delay);
        if(_changeScene)
            SceneManager.LoadScene(1);
    }

    public void ReverseTransition(){
        _curColor = Color.clear;
    }
}
