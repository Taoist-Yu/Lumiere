using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notice : MonoBehaviour
{
	GameObject noticeRenderer;

	bool isActive;
	float scale = 0;
	Vector3 initScale;

	[Header("是否为单一面的提示框")]
	public bool isSingle = false;
	[Header("是否是一直存在的提示框")]
	public bool isAlwaysEnable = false;

	[Header("缩放速度")]
	public float scaleSpeed = 1;

	private void Awake()
	{
		noticeRenderer = transform.Find("Renderer").gameObject;
	}

	// Use this for initialization
	void Start()
	{
		initScale = noticeRenderer.transform.localScale;
	}

	// Update is called once per frame
	void Update()
	{
		//提示框的出现与消失
		if(isAlwaysEnable == false)
		{
			if (isActive == true)
			{
				scale += Time.deltaTime * scaleSpeed;
			}
			else
			{
				scale -= Time.deltaTime * scaleSpeed;
			}
			if (scale > 1)
				scale = 1;
			if (scale < 0)
				scale = 0;

			noticeRenderer.transform.localScale = initScale * scale;
		}
		//提示框的旋转，用来适应转场
		if(isSingle)
		{
			int flag = (int)Mathf.Abs(transform.rotation.eulerAngles.y) % 360;
			if(flag <270 && flag > 90)
			{
				noticeRenderer.SetActive(false);
			}
			else
			{
				noticeRenderer.SetActive(true);
			}
		}
		else
		{
			transform.rotation = Quaternion.Euler(0, 0, 0);
		}

		//保证提示框显示在屏幕前方
		noticeRenderer.transform.position = new Vector3(
			transform.position.x,
			transform.position.y,
			-100
		);
	}

	#region 触发相关代码

	private void OnTriggerEnter2D(Collider2D collision)
	{
		switch (collision.tag)
		{
			case "Player":
				isActive = true;
				break;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		switch (collision.tag)
		{
			case "Player":
				isActive = false;
				break;
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		switch (collision.tag)
		{
			case "Player":
				isActive = true;
				break;
		}
	}

	#endregion

}

