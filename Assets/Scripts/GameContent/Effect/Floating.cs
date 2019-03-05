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
		initY = transform.localPosition.y;
	}

	private void Update()
	{
		transform.localPosition = new Vector3(transform.localPosition.x, range * Mathf.Sin(speed * Time.time) + initY, transform.localPosition.z);
	}
}
