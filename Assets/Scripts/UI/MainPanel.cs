using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainPanel : MonoBehaviour {

	private Button btn_Start;
	private Button btn_Setting;
	private Animator FateInOut;

	public AudioSource bottonClickSource;

	// Use this for initialization
	void Start () {
		InitButton();
		FateInOut = transform.parent.Find("FateMask").GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void InitButton()
	{
		btn_Start = transform.Find("Buttons/btn_Start").GetComponent<Button>();
		btn_Start.onClick.AddListener(OnStartButtonClicked);
		btn_Setting = transform.Find("Buttons/btn_Setting").GetComponent<Button>();
		btn_Setting.onClick.AddListener(OnSettingButtonClicked);
	}

	void OnStartButtonClicked()
	{
		bottonClickSource.Play();

		FateInOut.Play("FateIn");       //纯色遮罩淡入，屏幕渐黑
		GameStart(1, true);
		StartCoroutine(GameStart(1, true));
	}

	void OnSettingButtonClicked()
	{
		Debug.Log("SettingButtonClicked");
		bottonClickSource.Play();
	}

	/// <summary>
	/// 开始游戏
	/// </summary>
	/// <param name="level"></param>
	/// <param name="isNewGame"></param>
	IEnumerator GameStart(int level, bool isNewGame)
	{
		yield return new WaitForSeconds(1.1f);
		Debug.Log("StartButtonClicked");
		GameGlobal.GameData.currentLevel = level;
		SceneManager.LoadScene(level);
		
	}

}
