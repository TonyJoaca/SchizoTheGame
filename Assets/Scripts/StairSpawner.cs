using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairSpawner : MonoBehaviour
{
    [SerializeField] GameObject _stair;
    [SerializeField] bool _jos = false;
    [SerializeField] bool _create = false;
    Vector3 _curPos = new();
    // Start is called before the first frame update
    void Start(){
        if (_create){
            _curPos = transform.position;
            for (int i = 0; i < 120; i++)
            {
                _curPos = new(_curPos.x, _curPos.y + (_jos ? -0.1f : 0.1f), _curPos.z + 0.2f);
                Instantiate(_stair, _curPos, Quaternion.Euler(90f, 90f, 0f), transform);
            }
        }
    }
}
