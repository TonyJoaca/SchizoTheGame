using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetFlashlight : MonoBehaviour
{
	[SerializeField] Vector3 _vectOffset;
	[SerializeField] GameObject _goFollow;
	[SerializeField] float _speed = 1.0f;

	void Start()
	{
		_vectOffset = transform.position - _goFollow.transform.position;
	}

	void Update()
	{
		transform.SetPositionAndRotation(_goFollow.transform.position + _vectOffset, Quaternion.Slerp(transform.rotation, _goFollow.transform.rotation, _speed * Time.deltaTime));
    }
	public void CallThunder(){
		LightsController._thunder = true;
		BlackScreenTransition._canDoTransition = true;
	}
}