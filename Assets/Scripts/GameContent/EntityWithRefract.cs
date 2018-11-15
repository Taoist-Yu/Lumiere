using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityWithRefract : RefractLight
{
	Material defaultMatOfLight;

	/*普通操作*/
	private void Awake()
	{
		RayLuncherAwake();
		defaultMatOfLight = this.GetComponent<LineRenderer>().material;
		Debug.Log(defaultMatOfLight);
	}

	void Start()
	{
		RayLuncherStart();
	}

	void Update()
	{
		RayLuncherUpdate();
	}


	//重写该方法使其支持折射
	protected override void RayLuncherUpdate()
	{
		//重置线渲染器
		lineRenderer.positionCount = 0;
		//销毁临时色散线
		GameObject[] destoryedObject = GameObject.FindGameObjectsWithTag("TempLight");
		foreach (GameObject obj in destoryedObject)
		{
			Destroy(obj);
		}
		if (isEmitting)
			EmitRayWithRefracting();
	}


	protected void EmitRayWithRefracting()
	{
		bool flag = false;  //是否检测到挡光实体

		ray.direction = Quaternion.AngleAxis(angle, Vector3.forward) * transform.up;
		ray.origin = (Vector2)transform.position + offset * ray.direction;
		lineRenderer.positionCount = 2;

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
				other.OnLighting(hitArray[i].point, -ray.direction, light);
				//如果目标挡光(具有漫反射属性)，截断射线
				if (other.scatteringMode == ScatteringMode.diffuse)
				{
					lineRenderer.SetPosition(0, ray.origin);
					lineRenderer.SetPosition(1, hitArray[i].point + ray.direction * 0.1f);
					//计算折射
					Vector3[] lightDispertion = Refract(ray.direction, hitArray[i].normal, Color.white);
					if (lightDispertion.Length == 4)//色散发生
					{
						Debug.Log("ok");
						for (int j = 0; j < 4; j++)
						{
							GameObject tempLight = new GameObject("Empty");
							tempLight.tag = "TempLight";
							LineRenderer tempDispertion = tempLight.AddComponent<LineRenderer>();
							tempDispertion.material = new Material(Shader.Find("UI/Default"));
							//tempDispertion.material = new Material(defaultMatOfLight);
							//tempDispertion.startColor = colorOflightDispertion[j];
							//tempDispertion.endColor = colorOflightDispertion[j];
							tempDispertion.positionCount = 2;
							tempDispertion.startWidth = 0.1f;
							tempDispertion.endWidth = tempDispertion.startWidth + 0.2f;
							tempDispertion.material.color = colorOflightDispertion[j];
							//Debug.Log(colorOflightDispertion[j]);
							Vector3 startPos = new Vector3(hitArray[i].point.x, hitArray[i].point.y + 0.15f, 0) - new Vector3(0, 0.1f * j, 0);
							tempDispertion.SetPosition(0, startPos);
							tempDispertion.SetPosition(1, startPos + lightDispertion[j] * 10);
						}
					}
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

	//绘制色散光线
	void DrawLightDispertion(RaycastHit2D hitPoint, Vector3[] lightDispertion)
	{
		
	}
}
