using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

	/*注册事件*/
	public delegate void LeveLRotateBeginHandler();
	public event LeveLRotateBeginHandler LevelRotateBeginEvent;
	public delegate void LevelRotateEndHandler();
	public event LevelRotateEndHandler LevelRotateEndEvent;

	/*组件相关*/
	private Animator animator;

	//旋转动画相关
	public bool isAnimating = false;
	public bool isLateAimating = false;
	private float rotateOldValue;
	private Quaternion rotateInitValue;
	private int rotateDirection;
	public float rotateValue;

	//当前朝向(当前是哪个面面向玩家)
	public int perspective = 0;


	private void Awake()
	{
		//获取组件
		animator = GetComponent<Animator>();
	}

	void Start () {
		
	}
	
	void Update () {
		//场景旋转的输入获取
		if(isAnimating)
		{
			if(!isLateAimating)
			{
				RotateUpdate(rotateValue - rotateOldValue);
				rotateOldValue = rotateValue;
			}
		}
		else
		{
			if(GetInput.LeftRotate)
			{
				perspective = (perspective + 1) % 4;
				rotateDirection = 1;
				StartRotate();
			}
			if(GetInput.RightRotate)
			{
				perspective = (perspective + 3) % 4;
				rotateDirection = -1;
				StartRotate();
			}
		}

	}

	//开始场景旋转
	void StartRotate()
	{
		//数据初始化
		isAnimating = true;
		rotateValue = rotateOldValue = 0;
		rotateInitValue = transform.rotation;
		//开始旋转
		animator.Play("LevelRotate");
		//广播事件
		LevelRotateBeginEvent();
	}

	//结束场景旋转
	void EndRotate()
	{
		//对最终数据取整
		transform.rotation = rotateInitValue;
		transform.Rotate(Vector3.up, 90.0f * rotateDirection);
		//停止动画
		isLateAimating = true;
		Invoke("EndAnimation", 0.1f);
		animator.Play("EmptyState");
		//广播事件
		LevelRotateEndEvent();

	}

	//设置场景动画状态
	void EndAnimation()
	{
		isAnimating = false;
		isLateAimating = false;
	}

	//场景旋转过程
	void RotateUpdate(float deltaRotate)
	{
		transform.Rotate(Vector3.up, deltaRotate * rotateDirection);
	}

}
