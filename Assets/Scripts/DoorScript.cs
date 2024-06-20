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
    [SerializeField] bool _isLocked;
    AudioSource _audioSource;
    AudioClip[] _doorSounds;
    bool _firstTimeUnlock = true;
    bool _keyEventClose;
    bool _npcDoorOpen = false;
    [SerializeField] bool _canNpcOpen = false;
    public bool CanNpcOpen {set{_canNpcOpen = value;}}
    [SerializeField] bool _closeAferPassing = false;
    [SerializeField] bool _npcClosePassed = false;
    bool _closePassed = false;
    [SerializeField] bool _shouldOpenLightsAfterPass = false;
    [SerializeField] bool _passActions = false;
    public bool PassActions {set {_passActions = value;}}
    [SerializeField] bool _startDoor;
    [SerializeField] GameObject _flashlightPrefab;
    [SerializeField] GameObject _phantomLia;
    [SerializeField] bool _lockAfterPass = false;
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
            if(!_isLocked)
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
        if ((Input.GetKeyDown(KeyCode.E) && _enter) || (_keyEventClose && _open) || (_npcDoorOpen && !_open) || (_closePassed && _open)){
            _keyEventClose = false;
            _closePassed = false;
            if(!_doorKey.GetComponent<MeshRenderer>().enabled){
                if(_firstTimeUnlock && _doorKey.GetComponent<KeyController>() != null && !_npcDoorOpen && !_open){
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
            _npcDoorOpen = false;
        }
    }
    // Verificam daca suntem aproape de usa si o putem deschide
    private void OnTriggerEnter(Collider other) {
        _enter = true;
        if(other.gameObject.CompareTag("NPC") && _canNpcOpen){
            _npcDoorOpen = true;
        }
    }
    private void OnTriggerExit(Collider other) {
        _enter = false;
        if(_closeAferPassing && _passActions && _open){
            _closePassed = true;
            StartCoroutine(LockDoor());
            if (_shouldOpenLightsAfterPass)
                LightsController._lightsOn = true;
        }
        if(other.gameObject.CompareTag("NPC") && _npcClosePassed){
            _canNpcOpen = false;
            StartCoroutine(NpcOpenDoorDelay());
            _closePassed = true;
        }
    }
    IEnumerator OpenAfterUnlock(){
        yield return new WaitForSeconds(_doorSounds[2].length);
        _audioSource.PlayOneShot(_doorSounds[0]);
        _open = !_open;
        _currentRotationAngle = transform.localEulerAngles.y;
        _doorOpenAngle = _defaultRotationAngle + (_open ? _defaultDoorOpenAngle : 0);
        _openTime = 0;
        if(_startDoor){
            _flashlightPrefab.SetActive(true);
            HouseDialogue._showText = true;
            _phantomLia.SetActive(true);
        }
    }
    public void CloseDoor(){
        _keyEventClose = true;
        StartCoroutine(LockDoor());
    }

    public void OpenDoor(){
        _doorKey.GetComponent<MeshRenderer>().enabled = false;
    }
    IEnumerator LockDoor(){
        yield return new WaitForSeconds(0.5f);
        _doorKey.GetComponent<MeshRenderer>().enabled = true;
    }

    IEnumerator NpcOpenDoorDelay(){
        yield return new WaitForSeconds(1f);
        _canNpcOpen = true;
    }
}
