using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour {

	void Update () {
		if(transform.position.y < -5)
		{
			transform.SetPositionAndRotation(
					new Vector3(
						transform.position.x,
						-5,
						transform.position.z
						),
					transform.rotation
				);
		}
	}

}
