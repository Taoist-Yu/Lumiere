using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : GameBehaviour {

	/*物体是如何响应射线光的，折射，反射等*/
	public enum ScatteringMode
	{
		diffuse,            //漫反射，吸收光
		transmission,       //透射，光的方向不变直接穿过
	}
	public ScatteringMode scatteringMode = ScatteringMode.diffuse;      //默认为漫反射

	//四面碰撞体数组
	public GameObject[] colliders;
	//管理碰撞体的根物体(一个空物体)
	public GameObject colliderRoot;

	protected override void GameBehavierInit()
	{
		base.GameBehavierInit();
		colliderRoot = transform.Find("Colliders").gameObject;
		colliders = new GameObject[4];
		colliders[0] = transform.Find("Colliders/SouthCollider").gameObject;
		colliders[1] = transform.Find("Colliders/EastCollider").gameObject;
		colliders[2] = transform.Find("Colliders/NorthCollider").gameObject;
		colliders[3] = transform.Find("Colliders/WestCollider").gameObject;
	}

	protected void EntityUpdate()
	{

	}

	private void Awake()
	{
		GameBehavierInit();
	}

	private void Start()
	{
		for (int i = 1; i < 4; i++)
			colliders[i].SetActive(false);
	}

	protected override void OnLevelRotateBegin()
	{
		base.OnLevelRotateBegin();
	}

	protected override void OnLevelRotateEnd()
	{
		base.OnLevelRotateEnd();
		foreach (GameObject collider in colliders)
		{
			collider.SetActive(false);
		}
		colliders[levelController.perspective].SetActive(true);
		colliderRoot.transform.SetPositionAndRotation(
			colliderRoot.transform.position,
			Quaternion.Euler(0, 0, 0)
		);
	}

	/*由光线类调用，不是事件函数
	 * point: 光的入射点，世界坐标下
	 * dirction: 光源方向，指向光源
	 */
	public virtual void OnLighting(RaycastHit2D hit, Vector3 direction, RayLight light)
	{ }

}
