using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFloat : MonoBehaviour {

	GameObject Players;
	float radian = -1.6f;
	float perRadian = 0.02f;
	float radius = 0.4f;
	Vector3 oldPos;

	// Use this for initialization
	void Start () {
		Players = GameObject.Find("tempPlayer");
		oldPos = transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		radian += perRadian; 
		float dy = Mathf.Cos(radian) * radius;
		transform.Translate(new Vector3(0, dy, 0)*Time.deltaTime);
	}
}
