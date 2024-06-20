using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingWallController : MonoBehaviour
{
    Transform _tpPoint;
    [SerializeField] GameObject _blackScreen;
    [SerializeField] string[] _textStrings;
    [SerializeField] Text _textBox;
    [SerializeField] float _textDelay;
    bool _showText = true;
    // Start is called before the first frame update
    void Start()
    {
        _tpPoint = transform.GetChild(0).transform;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _blackScreen.GetComponent<BlackScreenTransition>().CanDoTransition = true;
            other.GetComponent<PlayerController>().CanMove = false;
            other.GetComponent<CharacterController>().Move(Vector3.zero);
            other.GetComponent<CharacterController>().enabled = false;
            if (_textStrings.Length > 0 && _showText)
            {
                _showText = false;
                StartCoroutine(TextToScreen(other));
            }
        }
    }

    IEnumerator TextToScreen(Collider _player)
    {
        foreach (string _text in _textStrings)
        {
            _textBox.text = _text;
            yield return new WaitForSeconds(_textDelay);
        }
        if(_tpPoint != null)
            _player.transform.position = _tpPoint.position;
        _textBox.text = "";
        _blackScreen.GetComponent<BlackScreenTransition>().ReverseTransition();
        _player.GetComponent<PlayerController>().CanMove = true;
        _player.GetComponent<CharacterController>().enabled = true;
        HouseDialogue._showText = true;
        gameObject.SetActive(false);
    }
}
