using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneEnvironmentController : MonoBehaviour
{
    [SerializeField] GameObject _blackScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallThunder(){
		LightsController._thunder = true;
	}
	public void Running(){
        HeadBobController._amplitude = 3f;
        HeadBobController._frequency = 12f;
		CutsceneFootStepController._delay = 0.5f;
	}

    public void Walking(){
        HeadBobController._amplitude = 2f;
        HeadBobController._frequency = 7f;
		CutsceneFootStepController._delay = 0.8f;
	}
    public void DoTransition(){
		_blackScreen.GetComponent<BlackScreenTransition>().CanDoTransition = true;
    }
}
