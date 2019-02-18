using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleAttribute : MonoBehaviour {

	public Vector3 position = Vector3.zero;
	public Vector3 axis;
	public float anglePerFrame = 0f;
	public bool clockwise = true;

	public CircleAttribute(Vector3 position,float anglePerFrame)
	{
		this.position = position;
		this.anglePerFrame = anglePerFrame;
		axis = new Vector3(position.y, -position.x, position.z);
		if (Random.Range(0.0f, 1.0f) < 0.5f) anglePerFrame = -anglePerFrame;
	}
}
