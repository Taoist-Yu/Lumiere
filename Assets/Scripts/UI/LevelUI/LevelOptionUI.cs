using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOptionUI : MonoBehaviour {

	public GameObject levelUIManagerObject;
	private LevelUIManager UIManager;

	private void Awake()
	{
		UIManager = levelUIManagerObject.GetComponent<LevelUIManager>();
	}
}
