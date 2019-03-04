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

