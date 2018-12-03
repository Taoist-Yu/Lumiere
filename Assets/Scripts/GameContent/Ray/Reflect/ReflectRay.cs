using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectRay : MonoBehaviour
{
	//是否被使用过
	public bool isUsed = false;

	private void OnRenderObject()
	{
		if(isUsed)
			Destroy(gameObject);
	}
}