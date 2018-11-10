using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayLuncher : Entity {

	protected Ray2D ray;
	protected RaycastHit2D[] hitArray;
	protected LineRenderer lineRenderer;

	protected bool isEmitting = true;	//是否正在发射光线 

	[Header("发射管最大旋转角度")]
	public float angleRange = 90;
	[Header("发射管初始角度")]
	public float initialAngle = 0;
	[Header("发射点偏转位移")]
	public float offset = 0;
	[Header("")]
	public float angle;

	private void Awake()
	{
		GameBehavierInit();
		ray = new Ray2D();

		lineRenderer = GetComponent<LineRenderer>();
	}

	// Use this for initialization
	void Start () {
		scatteringMode = ScatteringMode.diffuse;
		angle = initialAngle;

		//LineRenderer设置
		lineRenderer.positionCount = 2;
	}
	
	// Update is called once per frame
	void Update () {
		//重置线渲染器
		lineRenderer.positionCount = 0;
		if(isEmitting)
			EmitRay();

	}

	//生成一个Ray并发射
	void EmitRay()
	{
		bool flag = false;  //是否检测到挡光实体

		ray.direction = Quaternion.AngleAxis(angle, transform.forward) * transform.up;
		ray.origin = (Vector2)transform.localPosition + offset * ray.direction;
		lineRenderer.positionCount = 2;

		hitArray = Physics2D.RaycastAll(ray.origin, ray.direction);

		for (int i = 0; i < hitArray.Length; i++)
		{

			if (hitArray[i].transform.parent.gameObject == colliderRoot)
				continue;
			Entity other = hitArray[i].transform.parent.parent.GetComponent<Entity>();
			
			if(other != null)
			{
				if(other.scatteringMode == ScatteringMode.diffuse)
				{
					lineRenderer.SetPosition(0, ray.origin);
					lineRenderer.SetPosition(1, hitArray[i].point);
					flag = true;
					break;
				}
			}
		}

		if(flag == false)
		{
			lineRenderer.SetPosition(0, ray.origin);
			lineRenderer.SetPosition(1, ray.origin + 20*ray.direction);
		}

	}

}
