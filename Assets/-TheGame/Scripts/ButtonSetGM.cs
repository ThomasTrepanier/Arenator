using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSetGM : MonoBehaviour {

	public void OnCustomClick(string MethodName){
		GameManager.instance.PlaySound (GameManager.instance.ButtonClick_Sound, 1f);
		GameManager.instance.Invoke (MethodName, 0f);
	}
}
