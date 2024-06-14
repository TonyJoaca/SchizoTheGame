using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetFlashlight : MonoBehaviour
{
	[SerializeField] Vector3 _vectOffset;
	[SerializeField] GameObject _goFollow;
	[SerializeField] float _speed = 1.0f;
	[SerializeField] bool _isCutscene = false;

	void Start()
	{
		_vectOffset = transform.position - _goFollow.transform.position;
	}

	void Update()
	{
		if(!_isCutscene)
			transform.SetPositionAndRotation(_goFollow.transform.position + _vectOffset, Quaternion.Slerp(transform.rotation, _goFollow.transform.rotation, _speed * Time.deltaTime));
    }
}