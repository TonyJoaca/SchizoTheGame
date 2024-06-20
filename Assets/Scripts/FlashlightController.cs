using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    bool _isFlashlightOn = false;
    bool _gotFlashlight = false;
    public bool GotFlashlight {set{_gotFlashlight = value;} get{return _gotFlashlight;}}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F) && _gotFlashlight)
        {
            switch (_isFlashlightOn)
            {
                case true: 
                    transform.GetChild(0).gameObject.GetComponent<Light>().intensity = 38f;
                    _isFlashlightOn = false;
                    break;
                case false:
                    transform.GetChild(0).gameObject.GetComponent<Light>().intensity = 0f;
                    _isFlashlightOn = true;
                    break;
            }
        }
    }
}
