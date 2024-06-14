using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class KeyController : MonoBehaviour
{
    bool _canPickUp = false;
    [SerializeField] GameObject _closeDoor;
    [SerializeField] string[] _dialogueArray;
    Text _dialogueText;
    [SerializeField] bool _shouldCloseLights = false;
    private void Start() {
        gameObject.AddComponent<MeshRenderer>();
        _dialogueText = GameObject.Find("Dialogue").GetComponent<Text>();
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.E) && _canPickUp){
            if(_closeDoor != null)
                _closeDoor.GetComponent<DoorScript>().CloseDoor();
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            if(_shouldCloseLights)
                StartCoroutine(CloseLights());
            if(_dialogueArray.Count() > 0)
                StartCoroutine(StartDialogue());
            // gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other) {
        _canPickUp = true;
    }
    private void OnTriggerExit(Collider other) {
        _canPickUp = false;
    }

    IEnumerator CloseLights(){
        yield return new WaitForSeconds(2f);
        LightsController._lightsOn = false;
    }
    IEnumerator StartDialogue(){
        _dialogueText.color = Color.white;
        foreach(string _dialogue in _dialogueArray){
            _dialogueText.text = _dialogue;
            yield return new WaitForSeconds(2f);
        }
        _dialogueText.color = new(1,1,1,0);
    }
}
