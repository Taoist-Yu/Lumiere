using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectEntity : Entity {

	//用于clone反射光的原型
	public GameObject linePrototype;		

	//反射光结构体
	private struct ReflectLight
	{
		public RayLight light;
		public Ray ray;
		public GameObject line;
	}
	private List<ReflectLight> reflectLights;

	private void Awake()
	{
		GameBehavierInit();
		reflectLights = new List<ReflectLight>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		foreach (ReflectLight reflectLight in reflectLights)
		{
			EmitRay(reflectLight.light, reflectLight.ray, reflectLight.line.GetComponent<LineRenderer>());
			reflectLight.line.GetComponent<ReflectRay>().isUsed = true;
		}

		//重置光线
		foreach (ReflectLight reflect in reflectLights)
		{
//			Destroy(reflect.line,0.01f);
		}
		reflectLights = new List<ReflectLight>();
	}

	public override void OnLighting(RaycastHit2D hit, Vector3 direction, RayLight light)
	{
		base.OnLighting(hit, direction, light);

		//将向量坐标归一化
		direction = direction.normalized;

		//计算反射光方向
		Ray ray = new Ray
		{
			origin = hit.point,
			direction = Vector2.Reflect(-direction, hit.normal)
		};

		//生成一个lineRendererCarrier的实例
		GameObject lineInstance = Instantiate(linePrototype, this.transform);
		lineInstance.AddComponent<ReflectRay>();

		//整合反射光属性
		ReflectLight reflectLight = new ReflectLight
		{
			light = light,
			ray = ray,
			line = lineInstance
		};

		//将反射光添加进链表
		reflectLights.Add(reflectLight);
	}

	protected void EmitRay(RayLight light,Ray ray,LineRenderer lineRenderer)
	{
		bool flag = false;  //是否检测到挡光实体

		//设置LineRenderer
		lineRenderer.positionCount = 2;
		lineRenderer.startColor = light.Color;
		lineRenderer.endColor = light.Color;

		RaycastHit2D[] hitArray = Physics2D.RaycastAll(ray.origin, ray.direction);

		for (int i = 0; i < hitArray.Length; i++)
		{
			//如果检测目标为自身，则跳过
			if (hitArray[i].transform.parent.gameObject == colliderRoot)
				continue;
			//获得目标下的Entity脚本
			Entity other = hitArray[i].collider.transform.parent.parent.GetComponent<Entity>();

			if (other != null)
			{
				//激活目标的受光函数
				other.OnLighting(hitArray[i], -ray.direction, light);
				//如果目标挡光(具有漫反射属性)，截断射线
				if (other.scatteringMode == ScatteringMode.diffuse)
				{
					lineRenderer.SetPosition(0, ray.origin);
					lineRenderer.SetPosition(1, hitArray[i].point);
					flag = true;
					break;
				}
			}
		}

		if (flag == false)
		{
			lineRenderer.SetPosition(0, ray.origin);
			lineRenderer.SetPosition(1, ray.origin + 20 * ray.direction);
		}

	}

}
