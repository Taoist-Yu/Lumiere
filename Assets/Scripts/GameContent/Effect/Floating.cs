using System;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;

class Floating : MonoBehaviour
{

	float initY;

	public float speed;
	public float range;

	private void Start()
	{
		initY = transform.position.y;
	}

	private void Update()
	{
		transform.position = new Vector3(transform.position.x, range * Mathf.Sin(speed * Time.time) + initY, transform.position.z);
	}
}
