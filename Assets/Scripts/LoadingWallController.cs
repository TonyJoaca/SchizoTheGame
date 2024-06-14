using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingWallController : MonoBehaviour
{
    Transform _tpPoint;
    // Start is called before the first frame update
    void Start()
    {
        _tpPoint = transform.GetChild(0).transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        // Debug.Log("Atins");
        other.GetComponent<CharacterController>().enabled = false;
        other.transform.position = new Vector3(other.transform.position.x, other.transform.position.y, _tpPoint.position.z);
        other.GetComponent<CharacterController>().enabled = true;
    }
}
