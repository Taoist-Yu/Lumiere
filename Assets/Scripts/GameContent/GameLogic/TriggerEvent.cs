using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class TriggerEvent : Entity
{
	//事件触发时激活的对象
	[Header("对话框")]
	public BoxDialog boxDialog;

	//事件触发需要的前置条件
	[Header("前置事件")]
	public TriggerEvent[] preEvents;

	//事件触发对玩家光线颜色的需求
	[Header("对光线颜色是否有需求")]
	public bool isLightColorNeeded = false;
	[Header("需求的光线颜色")]
	public RayLight.LightColor lightColorNeeded;

	//是否已触发
	public bool isUsed
	{
		get;
		private set;
	}

	protected delegate void EventListHandler();
	protected EventListHandler eventList;

	protected override void Awake()
	{
		base.Awake();
		isUsed = false;
		RegisteEvents();
	}

	protected virtual void RegisteEvents()
	{
		eventList = BoxDialogEvent;
	}

	public void ActivateEvent()
	{
		//如果前置条件不满足，则退出
		foreach (TriggerEvent eve in preEvents)
			if (eve.isUsed == false)
				return;
		//如果光的颜色需求不符，则退出
		if(isLightColorNeeded)
		{
			RayLight playerLight = RayLight.GetLight(PlayerParticleController.lightQuantity);
			bool flag = false;
			//若颜色一样
			if (playerLight.lightColor == lightColorNeeded)
			{
				flag = true;
			}
			//反之
			else
			{
				if (playerLight.lightColor == RayLight.LightColor.white
					&& playerLight.LightQuantity != 0)
				{
					flag = true;
				}
				else
				{
					flag = false;
				}
			}
			if (flag == false)
				return;
		}
		//如果已经激活过，则退出
		if (isUsed)
			return;
		isUsed = true;

		//广播事件
		eventList();
	}

	void BoxDialogEvent()
	{
		//对话框事件
		if (boxDialog != null)
		{
			boxDialog.ActivateEvent();
		}
	}

}

