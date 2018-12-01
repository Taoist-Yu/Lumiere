using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightElementOperateInterface : OperateInterface {

	public override void Operate0(int lightQuantity)
	{
		base.Operate0(lightQuantity);
		deltaLightQuantity = 1;
		Destroy(gameObject, 0.1f);
	}

}
