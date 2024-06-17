using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneFootStepController : MonoBehaviour
{
    float _prevMag;
    float _timer;
    public static float _delay = 1f;
    AudioSource _source;
    [SerializeField] AudioClip[] _footsteps;
    [SerializeField] bool _onGrass = false;
    // Start is called before the first frame update
    void Start()
    {
        _prevMag = transform.position.magnitude;
        _source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if (transform.position.magnitude != _prevMag)
        {
            _prevMag = transform.position.magnitude;
            if (_timer > _delay)
            {
                _timer = 0;
                switch(_onGrass){
                    case true:
                        _source.PlayOneShot(_footsteps[2]);
                        (_footsteps[3], _footsteps[2]) = (_footsteps[2], _footsteps[3]);
                        break;
                    case false:
                        _source.PlayOneShot(_footsteps[0]);
                        (_footsteps[1], _footsteps[0]) = (_footsteps[0], _footsteps[1]);
                        break;
                }
            }
        }
    }
}
