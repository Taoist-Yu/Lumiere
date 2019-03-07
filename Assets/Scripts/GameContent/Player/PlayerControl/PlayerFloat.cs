using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFloat : MonoBehaviour {

	GameObject Players;
	float radian = -1.6f;
	float perRadian = 0.02f;
	float radius = 0.4f;
	bool down = false;
	Vector3 oldPos;

	// Use this for initialization
	void Start () {
		Players = GameObject.Find("tempPlayer");
		oldPos = transform.position;
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if (down && radian > 4.71f)
		{
			perRadian = 0.02f;
			radius = 0.4f;
			down = false;
		}
		radian += perRadian; 
		float dy = Mathf.Cos(radian) * radius;

		Vector3 targetPosition;
		if (dy > 0)
		{
			targetPosition = new Vector3(0, 2*radius, 0);
		}
		else targetPosition = new Vector3(0, 0, 0);

		if(transform.localPosition.y > 2 * radius)
		{
			transform.localPosition = new Vector3(0, 0.79f, 0);
		}
		else if(transform.localPosition.y < 0)
		{
			transform.localPosition = new Vector3(0, 0.01f, 0);
		}
		transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, Mathf.Abs(dy) * Time.fixedDeltaTime);
	}

	public void GoUp()
	{
		radian = 3.12f;
		perRadian = 0.1f;
		radius = 2f;
		down = true;
	}
}
