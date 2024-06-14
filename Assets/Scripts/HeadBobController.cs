using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class HeadBobController : MonoBehaviour
{
    public static float _amplitude = 2f;
    public static float _frequency = 8f;
    [SerializeField] private Transform _camera = null;
    [SerializeField] private Transform _cameraHolder = null;
    private readonly float _toggleSpeed = 1.5f;
    Vector3 _startPos;
    CharacterController _controller;
    [SerializeField] bool _isCutscene;
    float _prevMag;
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _startPos = _camera.localPosition;
        _prevMag = transform.position.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        if(!_isCutscene)
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _amplitude = 3f;
                _frequency = 12f;
            }else{
                _amplitude = 2f;
                _frequency = 8f;
            }
        CheckMotion();
        ResetPosition();
        _camera.LookAt(FocusTarget());
        Debug.Log(transform.position.magnitude);
    }

    void PlayMotion(Vector3 motion){
        _camera.localPosition += motion;
    }

    void CheckMotion(){
        switch(_isCutscene){
            case true:
                if(transform.position.magnitude != _prevMag){
                    _prevMag = transform.position.magnitude;
                    PlayMotion(FootStepMotion());
                }
                break;
            case false:
                float speed = new Vector3(_controller.velocity.x, 0, _controller.velocity.z).magnitude;
                if (speed < _toggleSpeed) return;
                if(!_controller.isGrounded) return;
                    PlayMotion(FootStepMotion());
                break;
        }
    }

    Vector3 FootStepMotion(){
        Vector3 pos = Vector3.zero;
        pos.y = Mathf.Sin(Time.time * _frequency) * _amplitude;
        pos.x = Mathf.Sin(Time.time * _frequency / 2) * _amplitude/3;
        return pos * Time.deltaTime;
    }

    void ResetPosition(){
        if (_camera.localPosition == _startPos) return;
        _camera.localPosition = Vector3.Lerp(_camera.localPosition, _startPos, 1 * Time.deltaTime);
    } 
    Vector3 FocusTarget(){
        Vector3 pos = new(transform.position.x, transform.position.y + _cameraHolder.localPosition.y, transform.position.z);
        pos += _cameraHolder.forward * 15.0f;
        return pos;
    }
}
