using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorfulPlatform : MonoBehaviour {

	[Header("平台的颜色")]
	public RayLight.LightColor platformColor;

	//动画相关的计时
	float timeVal;
	//动画状态机
	Animator anim;

	// Use this for initialization
	void Start () {
		anim = transform.Find("Model").GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(timeVal > 0)
		{
			timeVal -= Time.deltaTime;
		}
		else
		{
			anim.SetInteger("state", 2);
		}
	}

	/// <summary>
	/// 当颜色不同的玩家踩踏时触发，用于收缩平台
	/// </summary>
	public void OnScene()
	{
		timeVal = 1.5f;
		anim.SetInteger("state", 1);
	}

}
