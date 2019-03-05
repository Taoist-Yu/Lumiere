using System;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;


class CGTriggerEvent : TriggerEvent
{
	//额外的事件信息
	[Header("相机移动")]
	public GameObject moveTarget;
	public float speed = 1.0f;
	public bool isReturned = false;

	//CG脚本实例 
	public CGPlayer cg;

	private void Update()
	{
		transform.rotation = Quaternion.Euler(0, 0, 0);
	}

	//注册事件
	protected override void RegisteEvents()
	{
		base.RegisteEvents();
		eventList += CameraMoveEvent;
	}

	//额外的事件函数
	protected void CameraMoveEvent()
	{
		if(moveTarget != null)
		{
			Vector3 startPos = PlayerCamera.instance.transform.position;
			Vector3 endPos = moveTarget.transform.position + new Vector3(0, 0, PlayerCamera.instance.transform.position.z);
			PlayerCamera.Move(startPos, endPos, isReturned, speed, OnCameraMoveStart, OnCameraMoveEnd);
		}
	}

	void OnCameraMoveStart()
	{
		//解绑玩家与相机
		PlayerCamera.instance.transform.SetParent(null);
		StartCoroutine(InvokePlayCG());
	}

	void OnCameraMoveEnd()
	{
		//防止由于玩家重生调用相机移动
		GameObject.Find("tempPlayer").SetActive(false);
	}

	//延时播放CG
	IEnumerator InvokePlayCG()
	{
		float waitingTime = 0.3f / speed;
		yield return new WaitForSeconds(waitingTime);
		cg.PlayCG();
	}

}
