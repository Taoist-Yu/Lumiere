using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayLuncherOperateInterface : OperateInterface {

	RayOriginalLuncher luncher;

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

}
