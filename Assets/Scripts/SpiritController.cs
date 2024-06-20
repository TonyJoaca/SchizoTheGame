using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritController : MonoBehaviour
{
    bool _canDispell = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && _canDispell)
            Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other) {
        _canDispell = true;
    }
    private void OnTriggerExit(Collider other) {
        _canDispell = false;
    }
}
