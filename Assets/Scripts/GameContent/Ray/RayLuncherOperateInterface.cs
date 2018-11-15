using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayLuncherOperateInterface : OperateInterface {

	RayOriginalLuncher luncher;

	private void Awake()
	{
		luncher = GetComponent<RayOriginalLuncher>();
	}

	public override void Operate(int numOfLight)
	{
		base.Operate(numOfLight);
		Color color = new Color();
		switch (numOfLight % 6)
		{
			case 2:
				color = new Color(85f / 255f, 233f / 255f, 255f / 255f);
				break;
			case 3:
				color = new Color(166f / 255f, 255f / 255f, 165f / 255f);
				break;
			case 4:
				color = new Color(255f / 255f, 204f / 255f, 149f / 255f);
				break;
			case 5:
				color = new Color(255f / 255f, 133f / 255f, 132f / 255f);
				break;
			case 0:
				color = Color.white;
				break;
			case 1:
				color = new Color(109f / 255f, 109f / 255f, 109f / 255f);
				break;
			default: break;
		}
		
		luncher.ChangeEmitStatus(color);
		LightExpend = numOfLight;
	}

}
