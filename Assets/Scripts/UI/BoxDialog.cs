using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 悬浮在场景中的矩形对话框
/// 对话框存在时游戏暂停
/// </summary>
public class BoxDialog : MonoBehaviour
{
	//由于人物永远在屏幕中心，只需将对话框置于屏幕中心即可

	public string[] content;
	Text showedText;
	Animator ani;		//动画组件

	float interval = 0.2f;		//两段文本之间的时间间隔系数(实际时间间隔 = 系数 * 文段长度)

	private void Start()
	{
		//获取文本控件
		showedText = transform.Find("Text").GetComponent<Text>();
		//获取动画组件
		ani = GetComponent<Animator>();
		//暂时隐藏
		gameObject.SetActive(false);
	}

	/// <summary>
	/// 当对话框事件被触发时自动调用
	/// 激活对话框，开始播放对话框动画
	/// </summary>
	public void ActivateEvent()
	{
		gameObject.SetActive(true);
		ani.Play("Start");
	}

	private void Update()
	{

	}

	/// <summary>
	/// 在出现动画结束时调用，开启对话的播放
	/// </summary>
	public void PlayDialog()
	{
		StartCoroutine(AutoPlay());
	}

	/// <summary>
	/// 自毁方法
	/// </summary>
	public void Died()
	{
		Destroy(gameObject);
	}

	/// <summary>
	/// 逐句播放文本段
	/// </summary>
	/// <returns></returns>
	IEnumerator AutoPlay()
	{
		foreach(string text in content)
		{
			StartCoroutine(UpdateText(text));
			yield return new WaitForSeconds(text.Length * interval);
		}
		showedText.text = "";
		ani.Play("End");
	}

	/// <summary>
	/// 播放text中的文本，逐字显示
	/// </summary>
	/// <param name="text"></param>
	/// <returns></returns>
	IEnumerator UpdateText(string text)
	{
		string currentTxt = "";
		foreach(char c in text)
		{
			currentTxt += c;
			showedText.text = currentTxt;
			yield return new WaitForSeconds(0.05f);
		}
	}

}
