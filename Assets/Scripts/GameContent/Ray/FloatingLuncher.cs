using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 视觉效果脚本
 * 控制发射题随机漂浮
 */
public class FloatingLuncher : MonoBehaviour {

	[Header("浮动相关")]
	public float offset = 0.15f;
	public float floatingSpeed = 1f;
	[Header("旋转相关")]
	public float yRotateSpeed = 0.1f;
	public float xRotateSpeed = 0.1f;
	public float zRotateSpeed = 0.1f;
	public float yRotateRange = 500;
	public float xRotateRange = 500;
	public float zRotateRande = 500;

	protected Transform Model;

	private void Awake()
	{
		Model = transform.Find("Model");
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Model.localPosition = new Vector3(
						Model.position.x,
						offset * Mathf.Sin(Time.time * floatingSpeed),
						Model.position.z
					);
		Model.localRotation = Quaternion.Euler(
						new Vector3(
								Mathf.Sin(Time.time * xRotateSpeed) * xRotateRange,
								Mathf.Sin((Time.time + 1.57f) * yRotateSpeed) * yRotateRange,
								Mathf.Cos(Time.time * zRotateSpeed) * zRotateRande
							)
					);

	}
}
