using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] Transform _player;
    Animator _anim;
    bool _canTrigger = false;
    bool _phantomCanAdvance = false;
    public bool PhantomCanAdvance {set {_phantomCanAdvance = value;}}
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(gameObject.name){
            case "LiaNormal":
                if((Vector3.Distance(_player.position, transform.position) < 3f) && _canTrigger){
                    _canTrigger = false;
                    _anim.SetTrigger("Continue");
                }
                break;
            case "LiaPhantom":
                if(_phantomCanAdvance){
                    _phantomCanAdvance = false;
                    _anim.SetTrigger("Continue");
                }
                break;
        }
        
    }
    public void TriggerSet(){
        _canTrigger = true;
    }
}
