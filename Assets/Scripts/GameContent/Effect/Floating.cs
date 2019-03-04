using System;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;

class Floating : MonoBehaviour
{

	Vector3 initPos;

	public float speed;
	public float range;

	private void Start()
	{
		initPos = transform.position;
	}

	private void Update()
	{
		transform.position = initPos + new Vector3(0, range * Mathf.Sin(speed * Time.time), 0);
	}
}
