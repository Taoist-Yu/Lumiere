using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationOperateInterface : OperateInterface {

	private Destination destination;

	byte lastOperate = 0;

	private void Awake()
	{
		destination = GetComponent<Destination>();
	}

	public override void Operate1(int lightQuantity)
	{
		base.Operate1(lightQuantity);
		if (!destination.operatedAllowed) return;
		if (lastOperate == 1) return;

		destination.AddProgress();

		lastOperate = 1;
	}

	public override void Operate2(int lightQuantity)
	{
		base.Operate2(lightQuantity);
		if (!destination.operatedAllowed) return;
		if (lastOperate == 0) return;

		destination.AddProgress();

		lastOperate = 0;
	}
}
