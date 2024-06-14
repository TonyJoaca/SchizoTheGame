using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobController : MonoBehaviour
{
    [SerializeField, Range(2f,3f)] private float _amplitude = 2f;
    [SerializeField, Range(8f,12f)] private float _frequency = 8f;
    [SerializeField] private Transform _camera = null;
    [SerializeField] private Transform _cameraHolder = null;
    private readonly float toggleSpeed = 1.5f;
    Vector3 startPos;
    CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        startPos = _camera.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        _amplitude = Mathf.Clamp(_amplitude, 2f, 3f);
        _frequency = Mathf.Clamp(_frequency, 8f, 12f);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _amplitude++;
            _frequency++;
        }else{
            _amplitude--;
            _frequency--;
        }
        CheckMotion();
        ResetPosition();
        _camera.LookAt(FocusTarget());
    }

    void PlayMotion(Vector3 motion){
        _camera.localPosition += motion;
    }

    void CheckMotion(){
        float speed = new Vector3(controller.velocity.x, 0, controller.velocity.z).magnitude;
        if (speed < toggleSpeed) return;
        if(!controller.isGrounded) return;
        PlayMotion(FootStepMotion());
    }

    Vector3 FootStepMotion(){
        Vector3 pos = Vector3.zero;
        pos.y = Mathf.Sin(Time.time * _frequency) * _amplitude;
        pos.x = Mathf.Sin(Time.time * _frequency / 2) * _amplitude/3;
        return pos * Time.deltaTime;
    }

    void ResetPosition(){
        if (_camera.localPosition == startPos) return;
        _camera.localPosition = Vector3.Lerp(_camera.localPosition, startPos, 1 * Time.deltaTime);
    } 
    Vector3 FocusTarget(){
        Vector3 pos = new(transform.position.x, transform.position.y + _cameraHolder.localPosition.y, transform.position.z);
        pos += _cameraHolder.forward * 15.0f;
        return pos;
    }
}
