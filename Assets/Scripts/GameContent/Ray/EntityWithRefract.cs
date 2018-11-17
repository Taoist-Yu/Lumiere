using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityWithRefract : RefractLight
{
	Material defaultMatOfLight;
	Transform particlePrismTrans;

	/*普通操作*/
	private void Awake()
	{
		RayLuncherAwake();
		defaultMatOfLight = this.GetComponent<LineRenderer>().material;
		particlePrismTrans = GameObject.Find("ParticleAroundPrism").transform;
		isEmitting = true;
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

	/*折射相关*/

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
		{
			EmitRayWithRefracting();
		}
	}

	//支持折射的emit
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
					//测试色散
					Vector3[] lightDispertion = Refract(ray.direction, hitArray[i].normal, Color.white);
					lightOfDis.lightColor = Light.LightColor.blue;
					if (lightDispertion.Length == 4)	//色散发生
					{
						lightDispertionCal(i, lightDispertion);
					}
					flag = true;
					ParticlePrism.playingParticle = true;
					particlePrismTrans.gameObject.SendMessage("reEmitParticle", Color.white);
					//ParticlePrismTest.reEmitParticle(Color.white);
					break;
				}
			}
		}

		if (flag == false)
		{
			lineRenderer.SetPosition(0, ray.origin);
			lineRenderer.SetPosition(1, ray.origin + 20 * ray.direction);
			ParticlePrism.playingParticle = false;
			//ParticlePrismTest.reEmitParticle(Color.grey);
			particlePrismTrans.gameObject.SendMessage("reEmitParticle",Color.grey);
			//Debug.Log(ParticlePrismTest.colorOfParticle);
		}
	}
}