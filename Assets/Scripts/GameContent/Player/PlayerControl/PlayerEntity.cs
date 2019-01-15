using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity {

	#region 与控制器的接口
	tempController playerController;


	#endregion

	#region 初始化

	protected override void Awake()
	{
		//不响应转场,所以不调用base。Player拥有自己的转场响应机制
		scatteringMode = ScatteringMode.transmission;
		playerController = GetComponent<tempController>();
	}

	public override void OnLighting(RaycastHit2D hit, Vector3 direction, RayLight light)
	{
		base.OnLighting(hit, direction, light);
		playerController.OnLighting(hit, direction, light);
	}

	#endregion

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
