using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperateInterface : MonoBehaviour {

	public int lightNeed = 0;       //需要的光源数量
	public int deltaLightQuantity = 0;      //光量增量

	private void Update()
	{
		
	}

	public virtual void Operate(int lightQuantity)
	{
		deltaLightQuantity = 0;
	}

	public virtual void Operating(int lightQuantity)
	{
		deltaLightQuantity = 0;
	}

}
