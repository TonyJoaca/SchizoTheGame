using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController controller;
    float _speed = 6f;
    Vector3 move;
    [SerializeField] float _gravity = -9.81f;
    Vector3 velocity;
    [SerializeField] Transform _groundCheck;
    [SerializeField] LayerMask _ground;
    bool isGrounded = false;
    [SerializeField] float _jumpHeight;
    bool nebun = false;
    float teleportDistance = 10f;
    [SerializeField] AudioSource _source;
    [SerializeField] AudioClip[] _footsteps;
    float timer;
    float _delay = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(_groundCheck.position, 0.5f, _ground);
        if (!isGrounded)
        {
            velocity.y += _gravity * Time.deltaTime;
        }
        else
        {
            velocity.y = -2f;
        }
        // if (Input.GetButton("Jump") && isGrounded)
        // {
        //     velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
        // }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        move = x * transform.right + z * transform.forward + velocity;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _delay = 0.5f;
            _speed = 9f;
        }else{
            _delay = 0.8f;
            _speed = 6f;
        }
        controller.Move(_speed * Time.deltaTime * move.normalized);
        //controller.Move(velocity * Time.deltaTime);
        timer += Time.deltaTime;
        if (new Vector3(controller.velocity.x, 0, controller.velocity.z).magnitude > 0.1f)
        {
            if (timer > _delay)
            {
                timer = 0;
                _source.PlayOneShot(_footsteps[0]);
                (_footsteps[1], _footsteps[0]) = (_footsteps[0], _footsteps[1]);
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            controller.enabled = false;
            nebun = !nebun;
            transform.position = new Vector3(transform.position.x, transform.position.y + teleportDistance, transform.position.z);
            teleportDistance *= -1;
            controller.enabled = true;
        }
        if (Input.GetKeyUp(KeyCode.P))
        {
            if (LightsController._lightsOn)
            {
                LightsController._lightsOn = false;
            }else{
                LightsController._lightsOn = true;
            }
        }
    }
}
