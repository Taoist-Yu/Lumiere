using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectEntity : RayLuncher {

	[Header("反射器的颜色属性")]
	public Color color = Color.yellow;		//RayLuncher中颜色是由light决定，该类中重写了此规则

	protected override void RayLuncherUpdate()
	{
		base.RayLuncherUpdate();
		isEmitting = false;
		light = null;
		modelMeshRenderer.material.color = color;       //在base中颜色被修改，这里将颜色重设
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

	public override void OnLighting(RaycastHit2D hit, Vector3 direction, RayLight light)
	{
		base.OnLighting(hit, direction, light);

		//将向量坐标归一化
		direction = direction.normalized;

		//计算反射光
		ray.origin = hit.point;
		ray.direction = Vector2.Reflect(-direction, hit.normal);
		this.light = light;

		//发射光线
		isEmitting = true;
	}

	protected override void EmitRay()
	{
		/*方法完全重写,从而保证ray属性由OnLighting确定*/

		bool flag = false;  //是否检测到挡光实体
		//设置LineRenderer
		lineRenderer.positionCount = 2;
		lineRenderer.startColor = light.Color;
		lineRenderer.endColor = light.Color;

		hitArray = Physics2D.RaycastAll(ray.origin, ray.direction);

		for (int i = 0; i < hitArray.Length; i++)
		{
			//如果检测目标为自身，则跳过
			if (hitArray[i].transform.parent.gameObject == colliderRoot)
				continue;
			//获得目标下的Entity脚本
			Entity other = hitArray[i].transform.parent.parent.GetComponent<Entity>();

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
