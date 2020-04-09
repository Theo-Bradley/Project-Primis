using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerafollow : MonoBehaviour
{
	//veery simple for now
	public Transform target;


	public Vector3 offset;

	void LateUpdate()
	{
		this.transform.position = new Vector3(target.position.x, target.position.y, offset.z);
			
	}
}


