using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayLuncher : Entity {

	protected Ray2D ray;
	protected RaycastHit2D[] hitArray;
	protected GameObject barrel;

	[Header("发射管最大旋转角度")]
	public float angleRange = 90;
	[Header("发射管初始角度")]
	public float initialAngle = 0;
	public float angle;

	private void Awake()
	{
		GameBehavierInit();
		ray = new Ray2D();
		barrel = transform.Find("Barrel").gameObject;
	}

	// Use this for initialization
	void Start () {
		scatteringMode = ScatteringMode.diffuse;
		angle = initialAngle;
		barrel.transform.rotation = Quaternion.Euler(0, 0, initialAngle);
		if(barrel == null)
		{
			Debug.Log(1);
		}
	}
	
	// Update is called once per frame
	void Update () {
		barrel.transform.rotation = Quaternion.Euler(0, 0, angle);
	}

	//生成一个Ray并发射
	void EmitRay()
	{
		ray.direction = barrel.transform.up;
		ray.origin = barrel.transform.up;

		Physics2D.RaycastAll(ray.origin, ray.direction);

		for (int i = 0; i < hitArray.Length; i++)
		{
			
		}

	}

}
