using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class CinemaTextScript : MonoBehaviour
{
    [SerializeField] string[] _dialogue;
    TextMeshPro _text;
    [SerializeField] Transform _player;
    bool _lookBehind = false;
    [SerializeField] GameObject _phantomLia;
    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        if((Mathf.Abs(180f - _player.rotation.eulerAngles.y) <= 10f) && _lookBehind){
            _text.text = _dialogue[1];
            _phantomLia.GetComponent<AnimatorController>().PhantomCanAdvance = true;
        }
    }
    private void OnTriggerEnter(Collider other) {
        _text.text = _dialogue[0];
        _lookBehind = true;
        _phantomLia.GetComponent<AnimatorController>().PhantomCanAdvance = true;
    }
}
