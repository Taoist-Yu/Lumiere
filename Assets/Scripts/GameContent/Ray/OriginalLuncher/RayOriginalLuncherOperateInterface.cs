using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayOriginalLuncherOperateInterface : OperateInterface {

	private RayOriginalLuncher luncher;

	private void Awake()
	{
		luncher = GetComponent<RayOriginalLuncher>();
	}

	public override void Operate0(int lightQuantity)
	{
		base.Operate0(lightQuantity);
		RayLight light = RayLight.GetLight(lightQuantity);
		deltaLightQuantity = luncher.ChangeEmitStatus(light);
	}

	public override void Operating1(int lightQuantity)
	{
		base.Operating1(lightQuantity);
		luncher.angle += luncher.angleSpeed * Time.deltaTime;
	}

	public override void Operating2(int lightQuantity)
	{
		base.Operating2(lightQuantity);
		luncher.angle -= luncher.angleSpeed * Time.deltaTime;
	}

	private void Update()
	{
		
	}

}
