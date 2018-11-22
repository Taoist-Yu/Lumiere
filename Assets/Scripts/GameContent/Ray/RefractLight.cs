using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefractLight : RayLuncher
{
	/*该类只存放折射和色散相关函数,继承发射器功能*/

	public static bool totalReflecting = false;  //是否发生全反射
	bool inGlass = false; //是否在玻璃内
	public RayLight lightOfDis = new RayLight(); //色散画线颜色

	//根据现实物理来计算反射光
	public Vector3 Refract(Vector3 inDirection, Vector3 inNormal, float c = 1.5f)
	{
		//初始化参数，进入单位球
		inDirection = Vector3.Normalize(inDirection);
		inNormal = Vector3.Normalize(inNormal);
		float e = 1 / c;
		//计算入射角和折射角
		float cosIn = Vector3.Dot(-inDirection, inNormal);
		float cosOut = Mathf.Sqrt(1 - e * e * (1 - cosIn * cosIn));
		//根据是否发生全反射返回值（和输出错误信息）
		if (cosOut > 0)
		{
			Vector3 resultOfRefract = e * inDirection + inNormal * (e * cosIn - cosOut);
			totalReflecting = false;
			return resultOfRefract;
		}
		else
		{
			Debug.Log("Can't refract");
			totalReflecting = true;
			//发生全反射则返回全反射的出射光(在折射率小于1的时候才会出现这种情况)
			return Vector3.Reflect(inDirection, inNormal);
		}
	}

	//根据颜色返回反射光（色散）
	public Vector3[] Refract(Vector3 inDirection, Vector3 inNormal, Color color)
	{
		if (color != Color.white)       //非白光直接返回，不发生色散
		{
			Vector3[] lightDispersion = new Vector3[1];
			lightDispersion[0] = Vector3.Reflect(inDirection, inNormal);
			return lightDispersion;
		}
		else                            //白光返回多色光数组
		{
			Vector3[] lightDispersion = new Vector3[4];
			for (int i = 0; i < 4; i++)
			{
				lightDispersion[i] = Refract(inDirection, inNormal, Mathf.Sqrt(2f) + i * 0.5f);
			}
			return lightDispersion;
		}
	}

	//计算并绘制色散
	public void lightDispertionCal(int i, Vector3[] lightDispertion)
	{
		if (inGlass)
		{
			for (int j = 0; j < 4; j++)
			{
				DrawLightDispertion(hitArray[i], lightDispertion[j], lightOfDis.Color);
				lightOfDis.lightColor++;
			}
		}
		else
		{
			for (int j = 0; j < 4; j++)
			{
				RaycastHit2D dispertionRayHit = hitArray[i];
				int numOftotalReflecting = 0;
				//DrawLightDispertion(hitArray[i], lightDispertion[j], lightOfDis.Color);
				do
				{
					dispertionRayHit = Physics2D.Raycast(dispertionRayHit.point + new Vector2(lightDispertion[j].x, lightDispertion[j].y) * 0.01f, lightDispertion[j]);
					lightDispertion[j] = Refract(lightDispertion[j], dispertionRayHit.normal, 1 / Mathf.Sqrt(2));
					numOftotalReflecting++;
				} while (totalReflecting && numOftotalReflecting < 5);
				if (numOftotalReflecting < 5)
				{
					DrawLightDispertion(dispertionRayHit, lightDispertion[j], lightOfDis.Color);
				}
				lightOfDis.lightColor++;
			}
		}
	}

	//绘制色散光线
	public void DrawLightDispertion(RaycastHit2D hitPoint, Vector3 lightDispertion,Color color)
	{
		GameObject tempLight = new GameObject("Empty");
		tempLight.tag = "TempLight";
		LineRenderer tempDispertion = tempLight.AddComponent<LineRenderer>();
		tempDispertion.material = new Material(Shader.Find("UI/Default"));
		tempDispertion.positionCount = 2;
		tempDispertion.startWidth = 0.1f;
		tempDispertion.endWidth = tempDispertion.startWidth + 0.2f;
		tempDispertion.material.color = color;
		Vector3 startPos = new Vector3(hitPoint.point.x, hitPoint.point.y, 0);
		tempDispertion.SetPosition(0, startPos);
		tempDispertion.SetPosition(1, startPos + lightDispertion * 30);
	}


	/*色散
	* 使用重载有color参数的refrect方法可能会发生色散
	* 直接提供折射率只会折射或者全反射
	* 检测返回数组的长度（4或1），为4则表示发生色散
	* 此时第二次调用refrect方法，求出光束的下边界
	* 根据物理原理每个颜色光路宽度不变
	* 此时为实体添加四个LineRenderer组件，画出光路
	* 色散结束后销毁LineRenderer*/

	/*空气密度层问题
	* 还是分割面用碰撞体+射线检测的方式
	* 空气密度层有float折射率属性，直接调用Refrect方法*/

	/*三棱镜
	* 还是分割面用碰撞体+射线检测的方式
	* 一个bool记录此刻的材质是玻璃还是空气
	* 检测到此前材质是空气的不需要显示光路，只需继续计算
	* 此前材质是玻璃的，如果是反射（折射和反射的返回值相同），不需要显示光路，继续计算
	* 如果是折射，显示光路
	* 检测到需要折射时调用Refrect方法*/

	/* 需要提供颜色（带折射率）或者直接提供折射率 */

	//over

}