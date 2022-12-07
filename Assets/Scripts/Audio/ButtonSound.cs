using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{

	[SerializeField] AudioClip _buttonClickSFX;
	[SerializeField] AudioClip _buttonHoverSFX;

	void Start()
	{
		Button btn = GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	public void TaskOnClick()
	{
		AudioManager.Instance.PlaySound2D(_buttonClickSFX, .5f, UnityEngine.Random.Range(.8f, 1.2f));
	}

	public void TaskOnHover()
	{
		AudioManager.Instance.PlaySound2D(_buttonHoverSFX, .35f);
	}

}
