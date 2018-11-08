using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayLuncher : Entity {

	protected Ray2D ray;
	protected RaycastHit2D[] hitArray;
	protected GameObject barrel;
	protected LineRenderer lineRenderer;

	[Header("发射管最大旋转角度")]
	public float angleRange = 90;
	[Header("发射管初始角度")]
	public float initialAngle = 0;
	[Header("发射点偏转位移")]
	public float offset = 1;
	[Header("")]
	public float angle;

	private void Awake()
	{
		GameBehavierInit();
		ray = new Ray2D();
		barrel = transform.Find("Barrel").gameObject;

		lineRenderer = GetComponent<LineRenderer>();
	}

	// Use this for initialization
	void Start () {
		scatteringMode = ScatteringMode.diffuse;
		angle = initialAngle;
		barrel.transform.rotation = Quaternion.Euler(0, 0, initialAngle);

		//LineRenderer设置
		lineRenderer.positionCount = 2;
		lineRenderer.startWidth = 0.5f;
		lineRenderer.endWidth = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		barrel.transform.rotation = Quaternion.Euler(0, 0, angle);

		EmitRay();

	}

	//生成一个Ray并发射
	void EmitRay()
	{
		bool flag = false;
		ray.direction = barrel.transform.up;
		ray.origin = offset*barrel.transform.up;

		hitArray = Physics2D.RaycastAll(ray.origin, ray.direction);

		for (int i = 0; i < hitArray.Length; i++)
		{
			Entity other = new Entity();
			
			if(other != null)
			{
				if(other.scatteringMode == ScatteringMode.diffuse)
				{
					lineRenderer.SetPosition(1, ray.origin);
					lineRenderer.SetPosition(2, hitArray[i].point);
					flag = true;
					Debug.Log(1);
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
