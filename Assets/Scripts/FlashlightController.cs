using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    bool _isFlashlightOn = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F))
        {
            switch (_isFlashlightOn)
            {
                case true: 
                    transform.GetChild(0).gameObject.SetActive(false);
                    _isFlashlightOn = false;
                    break;
                case false:
                    transform.GetChild(0).gameObject.SetActive(true);
                    _isFlashlightOn = true;
                    break;
            }
        }
    }
}
