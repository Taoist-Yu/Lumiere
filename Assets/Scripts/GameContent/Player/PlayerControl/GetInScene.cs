using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetInScene : GameBehaviour
{
	//int getInScenceTime = 0;
	public static bool inScence = true;
	public float speed = 2f;
	[SerializeField]
	float targetPositionX = 0;
	[SerializeField]
	float targetPositionY = 3;
	[SerializeField]
	float targetPositionZ = -15;

	private void Awake()
	{
		GameBehavierInit();
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (!inScence)
		{
			float step = speed * Time.deltaTime;
			transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPositionX, targetPositionY, targetPositionZ), step);
			if (transform.position.x == targetPositionX && transform.position.y == targetPositionY)
			{
				inScence = true;
				//this.GetComponent<Camera>().orthographic = true;
			}
			/*if (transform.rotation.x > 0)
			{
				transform.rotation = Quaternion.AngleAxis(-0.05f, transform.right) * transform.rotation;
			}*/
		}
	}
}
