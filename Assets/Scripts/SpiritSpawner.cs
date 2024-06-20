using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class SpiritSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] _spirits;
    [SerializeField] GameObject _key;
    List<Transform> _spawnPoints = new();
    Transform _lastSpawnPoint;
    Transform _spawnPoint;
    int _spiritsGot = -1;
    [SerializeField] int _spiritsNeeded = 3;
    [SerializeField] GameObject _openDoor;
    [SerializeField] string[] _completedTextDialogue;
    [SerializeField] Text _textBox;
    [SerializeField] float _textDelay = 2.5f;
    bool _canSpawn = true;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform _child in transform){
            _spawnPoints.Add(_child);
        }
        _spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
    }

    // Update is called once per frame
    void Update()
    {
        if(_spiritsGot >= _spiritsNeeded){
            _canSpawn = false;
            _spiritsGot = 0;
            _openDoor.GetComponent<DoorScript>().OpenDoor();
            _openDoor.GetComponent<DoorScript>().PassActions = true;
            StartCoroutine(TextStart());
        }
        if(_key.GetComponent<MeshRenderer>().enabled == false && _key.GetComponent<KeyController>().TextEnded == true){
            if(transform.childCount - _spawnPoints.Count == 0){
                _spiritsGot++;
                while(_lastSpawnPoint == _spawnPoint)
                    _spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
                if(_canSpawn)
                    Instantiate(_spirits[Random.Range(0,_spirits.Length)], _spawnPoint.localPosition, Quaternion.identity, transform);
                _lastSpawnPoint = _spawnPoint;
            }
        }
    }
    IEnumerator TextStart(){
        foreach(string _text in _completedTextDialogue){
            _textBox.text = _text;
            yield return new WaitForSeconds(_textDelay);
        }
        _textBox.text = "";
    }
}
