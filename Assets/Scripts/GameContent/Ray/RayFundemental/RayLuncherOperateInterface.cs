using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayLuncherOperateInterface : OperateInterface {

	private RayLuncher luncher;

	private void Awake()
	{
		luncher = GetComponent<RayLuncher>();
	}

	public override void Operating(int lightQuantity)
	{
		base.Operating(lightQuantity);
		luncher.angle += luncher.angleSpeed * Time.deltaTime;
	}

	private void Update()
	{
		
	}

}
