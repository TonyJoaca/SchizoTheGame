using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BlackScreenTransition : MonoBehaviour
{
    public static bool _canDoTransition = false;
    [SerializeField] float _speed = 0.5f;
    Image _image;
    Color _curColor;
    [SerializeField] float _delay = 0.5f;
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
        yield return new WaitForSeconds(3.2f);
        SceneManager.LoadScene(1);
    }
}
