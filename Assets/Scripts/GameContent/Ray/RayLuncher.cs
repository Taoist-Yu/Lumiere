using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayLuncher : Entity {

	protected Ray2D ray;
	protected RaycastHit2D[] hitArray;
	protected LineRenderer lineRenderer;

	protected MeshRenderer modelMeshRenderer;

	protected bool isEmitting = true;		//是否正在发射光线 
	protected Color color = Color.gray;     //当前发射器的颜色,默认为灰（代表空）
	protected int lightQuantity = 0;		//当前光量

	[Header("发射管最大旋转角度")]
	public float angleRange = 90;
	[Header("发射管初始角度")]
	public float initialAngle = 0;
	[Header("发射点偏转位移")]
	public float offset = 0;
	[Header("")]
	public float angle;

	/*定义生命周期虚函数*/
	protected virtual void RayLuncherAwake()
	{
		GameBehavierInit();
		ray = new Ray2D();
		lineRenderer = GetComponent<LineRenderer>();
		modelMeshRenderer = transform.Find("Model").GetComponent<MeshRenderer>();
	}

	protected virtual void RayLuncherStart()
	{
		scatteringMode = ScatteringMode.diffuse;
		angle = initialAngle;

		//LineRenderer设置
		lineRenderer.positionCount = 2;

	}

	protected virtual void RayLuncherUpdate()
	{
		//重置线渲染器
		lineRenderer.positionCount = 0;
		//更新发射器颜色
		modelMeshRenderer.material.color = color;
		//发射光线
		if (isEmitting)
			EmitRay();
	}

	private void Awake()
	{
		RayLuncherAwake();
	}

	// Use this for initialization
	void Start () {
		RayLuncherStart();
	}
	
	// Update is called once per frame
	void Update () {
		RayLuncherUpdate();
		
	}

	//生成一个Ray并发射
	protected void EmitRay()
	{
		bool flag = false;  //是否检测到挡光实体
		//设置射线检测
		ray.direction = Quaternion.AngleAxis(angle, Vector3.forward) * transform.up;
		ray.origin = (Vector2)transform.position + offset * ray.direction;
		//设置LineRenderer
		lineRenderer.positionCount = 2;
		lineRenderer.startColor = color;
		lineRenderer.endColor = color;

		hitArray = Physics2D.RaycastAll(ray.origin, ray.direction);

		for (int i = 0; i < hitArray.Length; i++)
		{
			//如果检测目标为自身，则跳过
			if (hitArray[i].transform.parent.gameObject == colliderRoot)
				continue;
			//获得目标下的Entity脚本
			Entity other = hitArray[i].transform.parent.parent.GetComponent<Entity>();
			
			if(other != null)
			{
				//激活目标的受光函数
				other.OnLighting(hitArray[i].point, -ray.direction, color);
				//如果目标挡光(具有漫反射属性)，截断射线
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
