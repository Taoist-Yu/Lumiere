using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayLuncherOperateInterface : OperateInterface {

	private RayLuncher luncher;

	private void Awake()
	{
		luncher = GetComponent<RayLuncher>();
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
