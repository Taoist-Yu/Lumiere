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

	//是否已触发
	bool isUsed = false;

	protected override void Awake()
	{
		base.Awake();
	}

	public void ActivateEvent()
	{
		//如果已经激活过，则退出
		if (isUsed)
			return;
		isUsed = true;

		if (boxDialog != null)
		{
			boxDialog.ActivateEvent();
		}
	}

}

