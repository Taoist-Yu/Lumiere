using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayLuncher : Entity {

	protected Ray2D ray;
	protected RaycastHit2D[] hitArray;
	GameObject barrel;

	[Header("发射管最大旋转角度")]
	public float angleRange = 90;
	[Header("发射管初始角度")]
	public float initialAngle = 0;

	protected override void GameBehavierInit()
	{
		base.GameBehavierInit();
		foreach(GameObject colliderObjert in colliders)
		{
			
		}
	}

	private void Awake()
	{
		GameBehavierInit();
		ray = new Ray2D();
		barrel = GameObject.Find("Barrel");
	}

	// Use this for initialization
	void Start () {
		scatteringMode = ScatteringMode.diffuse;
	}
	
	// Update is called once per frame
	void Update () {
		
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
