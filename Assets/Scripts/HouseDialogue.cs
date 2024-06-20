using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using UnityEngine;
using UnityEngine.UI;

public class HouseDialogue : MonoBehaviour
{
    Text _textBox;
    [SerializeField] string[] _houseDialogue;
    [SerializeField] int _firstTimeEnterStart;
    [SerializeField] int _firstTimeEnterEnd;
    [SerializeField] int _bathroomStart;
    [SerializeField] int _bathroomEnd;
    [SerializeField] int _bathroomWaitStart;
    [SerializeField] int _bathroomWaitEnd;
    [SerializeField] int _flashlightStart;
    [SerializeField] int _flashlightEnd;
    public static bool _showText = true;
    public static int _selectedPart = 0;
    [SerializeField] Animator _liaAnim;
    [SerializeField] float _textDelay = 3.5f;
    [SerializeField] GameObject _player;
    [SerializeField] GameObject[] _npcOpenDoor;
    [SerializeField] GameObject _blackScreen;
    [SerializeField] GameObject[] _keys;
    [SerializeField] GameObject _flashlight;
    // Start is called before the first frame update
    void Start()
    {
        _textBox = GetComponent<Text>();
    }

    void Update(){
        if (_showText){
            _showText = false;
            StartCoroutine(TextToScreen());
        }
    }

    IEnumerator TextToScreen(){
        // Mai multe parti ale _houseDialogue pot fi folosite
        switch(_selectedPart){
            case 0:
                for(int i = _firstTimeEnterStart; i <= _firstTimeEnterEnd; ++i){
                    _textBox.text = _houseDialogue[i];
                    yield return new WaitForSeconds(_textDelay);
                }
                _liaAnim.SetTrigger("Continue");
                _textBox.text = "";
                _selectedPart++;
                break;
            case 1:
                _player.GetComponent<PlayerController>().CanMove = false;
                for(int i = _bathroomStart; i <= _bathroomEnd; ++i){
                    _textBox.text = _houseDialogue[i];
                    yield return new WaitForSeconds(_textDelay);
                }
                _npcOpenDoor[0].GetComponent<DoorScript>().CanNpcOpen = true;
                _liaAnim.SetTrigger("Continue");
                _textBox.text = "";
                yield return new WaitForSeconds(1.5f);
                _blackScreen.GetComponent<BlackScreenTransition>().CanDoTransition = true;
                for(int i = _bathroomWaitStart; i <= _bathroomWaitEnd; ++i){
                    _textBox.text = _houseDialogue[i];
                    yield return new WaitForSeconds(_textDelay);
                }
                _textBox.text = "";
                LightsController._lightsOn = false;
                _blackScreen.GetComponent<BlackScreenTransition>().ReverseTransition();
                _player.GetComponent<PlayerController>().CanMove = true;
                _keys[0].GetComponent<MeshRenderer>().enabled = true;
                _selectedPart++;
                break;
            case 2:
                if(_flashlight.GetComponent<FlashlightController>().GotFlashlight == false){
                    for(int i = _flashlightStart; i <= _flashlightEnd; ++i){
                        _textBox.text = _houseDialogue[i];
                        yield return new WaitForSeconds(_textDelay);
                    }
                    _textBox.text = "";
                    _selectedPart++;
                }
                break;
        }
    }
}
