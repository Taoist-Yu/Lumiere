using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//UI界面中的场景模型
public class LevelModel : MonoBehaviour {

	private float rotateSpeed = 45;

	// Update is called once per frame
	void Update () {
		transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0));
	}
}
