using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayOriginalLuncherOperateInterface : OperateInterface {

	private RayOriginalLuncher luncher;

	private void Awake()
	{
		luncher = GetComponent<RayOriginalLuncher>();
	}

	public override void Operate(int lightQuantity)
	{
		base.Operate(lightQuantity);
		Light light = Light.GetLight(lightQuantity);
		deltaLightQuantity = luncher.ChangeEmitStatus(light);
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
