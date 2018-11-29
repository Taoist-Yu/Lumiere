using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperateInterface : MonoBehaviour {

	public int lightNeed = 0;       //需要的光源数量
	public int deltaLightQuantity = 0;      //光量增量

	private void Update()
	{
		
	}

	public virtual void Operate0(int lightQuantity)
	{
		deltaLightQuantity = 0;
	}

	public virtual void Operate1(int lightQuantity)
	{
		deltaLightQuantity = 0;
	}

	public virtual void Operate2(int lightQuantity)
	{
		deltaLightQuantity = 0;
	}

	public virtual void Operating0(int lightQuantity)
	{
		deltaLightQuantity = 0;
	}

	public virtual void Operating1(int lightQuantity)
	{
		deltaLightQuantity = 0;
	}

	public virtual void Operating2(int lightQuantity)
	{
		deltaLightQuantity = 0;
	}

}
