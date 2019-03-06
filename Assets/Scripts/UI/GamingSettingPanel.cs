using System;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

class GamingSettingPanel : MonoBehaviour
{
	private Button btn_Continue;
	private Button btn_Returned;

	//按钮音效
	private AudioSource clickAudio;

	//屏幕遮罩
	private Animator FateInOut;

	// Use this for initialization
	void Awake()
	{
		InitButton();
		clickAudio = GameObject.Find("Audios/ButtonClick").GetComponent<AudioSource>();
		FateInOut = GameObject.Find("FateMask").GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		if (GetInput.ESC)
		{
			ContinueGame();
		}
	}

	void InitButton()
	{
		btn_Continue = transform.Find("Buttons/btn_Continue").GetComponent<Button>();
		btn_Continue.onClick.AddListener(OnContinueButtonClicked);
		btn_Returned = transform.Find("Buttons/btn_Returned").GetComponent<Button>();
		btn_Returned.onClick.AddListener(OnReturnedButtonClicked);
	}

	void OnContinueButtonClicked()
	{
		ContinueGame();
		clickAudio.Play();
	}

	void OnReturnedButtonClicked()
	{
		clickAudio.Play();
		StartCoroutine(ExitLevel());
	}

	void ContinueGame()
	{
		if (transform.parent.gameObject.activeInHierarchy == true)
		{
			transform.parent.gameObject.SetActive(false);
			GameGlobal.GameData.isPausing = false;
		}
	}

	IEnumerator ExitLevel()
	{
		FateInOut.Play("FateIn");       //纯色遮罩淡入，屏幕渐黑
		yield return new WaitForSeconds(1.1f);
		GameGlobal.GameData.currentLevel = 0;
		SceneManager.LoadScene(0);
	}

}

