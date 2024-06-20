using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightPickup : MonoBehaviour
{
    bool _canPickup = false;
    [SerializeField] GameObject _flashlight;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && _canPickup){
            _flashlight.GetComponent<FlashlightController>().GotFlashlight = true;
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other) {
        _canPickup = true;
    }
    private void OnTriggerExit(Collider other) {
        _canPickup = false;
    }
}
