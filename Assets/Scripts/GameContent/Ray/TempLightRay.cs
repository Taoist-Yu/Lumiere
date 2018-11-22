using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempLightRay : RayLuncher {

	Vector2 thisRayDirection;
	Vector3 thisRayOrign;
	RayLight rayColor;

	protected override void RayLuncherAwake()
	{
		GameBehavierInit();
		ray = new Ray2D();
	}

	private void Awake()
	{
		RayLuncherAwake();
	}

	protected override void RayLuncherStart()
	{
		scatteringMode = ScatteringMode.diffuse;
	}

	protected override void RayLuncherUpdate()
	{
		
	}

	public void ResetRay(Vector2 rayDirection, Vector3 rayOrign, RayLight color)
	{
		ray.direction = rayDirection;
		ray.origin = rayOrign;
		rayColor = color;
		EmitRay();
	}


	protected override void EmitRay()
	{
		bool flag = false;  //是否检测到挡光实体
							//设置射线检测

		hitArray = Physics2D.RaycastAll(ray.origin, ray.direction);
		lineRenderer = this.GetComponent<LineRenderer>();

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
				other.OnLighting(hitArray[i], -ray.direction, rayColor);
				//如果目标挡光(具有漫反射属性)，截断射线
				if (other.scatteringMode == ScatteringMode.diffuse)
				{
					//lineRenderer.SetPosition(0, ray.origin);
					//lineRenderer.SetPosition(1, hitArray[i].point);
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
