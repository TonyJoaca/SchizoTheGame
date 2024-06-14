using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform _player;
    [SerializeField, Range(100f, 1000f)] private float _MouseSens = 500f;
    float xRotation = 0;
    bool locked = false;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F1))
        {
            locked = !locked;
        }
        float x = 0; float y = 0;
        if (!locked)
        {
            x = Input.GetAxis("Mouse X");
            y = Input.GetAxis("Mouse Y");
        }
        _player.Rotate(0, x * _MouseSens * Time.deltaTime, 0);
        xRotation -= y * _MouseSens * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -85, 85);
        transform.localRotation = Quaternion.Euler(xRotation, transform.rotation.y, transform.rotation.z);
    }
}
