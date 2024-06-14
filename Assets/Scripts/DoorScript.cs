using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] AnimationCurve _openSpeedCurve = new(new Keyframe[] { new(0, 1, 0, 0), new(0.8f, 1, 0, 0), new(1, 0, 0, 0) }); // Usa se va deschide repede la inceput iar aproape de sfarsit va incetini
    [SerializeField] float _openSpeedMultiplier = 2.0f; // Viteza de deschidere
    [SerializeField] float _defaultDoorOpenAngle = 90.0f; // Unghiul care se va adauga la unghiul initial al usii
    float _doorOpenAngle = 0.0f; // Unghiul la care trebuie sa se deschida usa
    bool _open = false; // Deschisa sau inchisa
    bool _enter = false; // Se poate deschide?
    float _defaultRotationAngle; // Unghiul initial
    float _currentRotationAngle; // Unghiul curent
    float _openTime = 0; // Timpul de miscare al usii
    [SerializeField] GameObject _doorKey;
    AudioSource _audioSource;
    AudioClip[] _doorSounds;
    bool _firstTimeUnlock = true;
    bool _keyEventClose;
    private void Awake() {
        _doorSounds = Resources.LoadAll<AudioClip>("DoorSounds");
    }

    void Start()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
        _audioSource.volume = 0.3f;
        _defaultRotationAngle = transform.localEulerAngles.y;
        _currentRotationAngle = transform.localEulerAngles.y;
        if(_doorKey == null){
            _doorKey = new GameObject("Key");
            _doorKey.AddComponent<MeshRenderer>();
            _doorKey.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_openTime < 1)
        {
            _openTime += Time.deltaTime * _openSpeedMultiplier * _openSpeedCurve.Evaluate(_openTime);
        }
        // Miscarea usii
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Mathf.LerpAngle(_currentRotationAngle, _defaultRotationAngle + (_open ? _doorOpenAngle : 0), _openTime), transform.localEulerAngles.z);
        // Verificam daca putem deschide usa
        if ((Input.GetKeyDown(KeyCode.E) && _enter) ||(_keyEventClose && _open)){
            _keyEventClose = false;
            if(!_doorKey.GetComponent<MeshRenderer>().enabled){
                if(_firstTimeUnlock && _doorKey.GetComponent<KeyController>() != null){
                    _firstTimeUnlock = false;
                    _audioSource.PlayOneShot(_doorSounds[2]);
                    StartCoroutine(OpenAfterUnlock());
                }else{
                    _audioSource.PlayOneShot(_doorSounds[0]);
                    _open = !_open;
                    _currentRotationAngle = transform.localEulerAngles.y;
                    _doorOpenAngle = _defaultRotationAngle + (_open ? _defaultDoorOpenAngle : 0);
                    _openTime = 0;
                }
            }else{
                _audioSource.PlayOneShot(_doorSounds[1]);
                if(!TextOpacityTransition.StartTransition)
                    TextOpacityTransition.StartTransition = true;
            }
        }
    }
    // Verificam daca suntem aproape de usa si o putem deschide
    private void OnTriggerEnter(Collider other) {
        _enter = true;
    }
    private void OnTriggerExit(Collider other) {
        _enter = false;
    }
    IEnumerator OpenAfterUnlock(){
        yield return new WaitForSeconds(_doorSounds[2].length);
        _audioSource.PlayOneShot(_doorSounds[0]);
        _open = !_open;
        _currentRotationAngle = transform.localEulerAngles.y;
        _doorOpenAngle = _defaultRotationAngle + (_open ? _defaultDoorOpenAngle : 0);
        _openTime = 0;
    }
    public void CloseDoor(){
        _keyEventClose = true;
        StartCoroutine(LockDoor());
    }
    IEnumerator LockDoor(){
        yield return new WaitForSeconds(0.5f);
        _doorKey.GetComponent<MeshRenderer>().enabled = true;
    }
}
