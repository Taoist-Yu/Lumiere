using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftingPlatform : Organ {

	GameObject platform;
	LiftingPlatformEffect effect;

	public float speed = 10.0f;
	public float upperLimit = 3.0f;
//	[Header("位移方向")]
//	public Vector2 direction;				

	private float displacement = 0;             //当前升降台的位移
	private float originY;					//初始位置

	protected override void Awake()
	{
		base.Awake();
		platform = transform.parent.Find("Platform").gameObject;
		effect = transform.Find("Model").GetComponent<LiftingPlatformEffect>();
		effect.rayLight = lightNeed;
		originY = platform.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		displacement -= Time.deltaTime * speed;
		if (displacement < 0) displacement = 0;

		Vector3 pos = platform.transform.position;
		pos.y = originY + displacement;
		platform.transform.position = pos;
	}

	public override void OnLighting(RaycastHit2D hit, Vector3 direction, RayLight light)
	{
		base.OnLighting(hit, direction, light);
		displacement += 2 * Time.deltaTime * speed;
		if (displacement > upperLimit) displacement = upperLimit;
	}

}
