using UnityEngine;
using UnityEngine.UI;

public class TomsControlTextScript : MonoBehaviour
{
    public Controls controls;
	public string command;

    void Start()
    {
		Text text = GetComponent<Text>();

		//uses reflection to get the value.
		string value =controls.GetType().GetField(command).GetValue(controls).ToString();
		text.text = value;
    }

}