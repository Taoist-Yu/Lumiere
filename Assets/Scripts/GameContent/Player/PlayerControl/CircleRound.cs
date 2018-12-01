using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRound : GameBehaviour {
	[SerializeField]
	float speed = 40f;

	private void Awake()
	{
		GameBehavierInit();
	}

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		transform.RotateAround(transform.position, Vector3.up, speed * Time.deltaTime);
	}
}
