using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardInput : MonoBehaviour
{
	public static KeyboardInput keyboardInput;
	[SerializeField] GameObject flower;
	List<GameObject> flowers;
	[SerializeField] AudioClip[] notes;
	[SerializeField] Text textbox;

	private void Awake()
	{
		keyboardInput = this;
	}
	// Start is called before the first frame update
	void Start()
	{
		flowers = new List<GameObject>();
	}

	// Update is called once per frame
	void Update()
	{
		CheckInput();
	}

	void SpawnFlower(int flowerIndex)
	{
		float spawnY = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
		float spawnX = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);

		Vector2 spawnPosition = new Vector2(spawnX, spawnY);
		GameObject newFlower = Instantiate(flower, spawnPosition, Quaternion.identity);
		newFlower.GetComponent<Note>().audioClip = notes[flowerIndex];
		flowers.Add(newFlower);

	}

	void CheckInput()
	{
		foreach (char inputChar in Input.inputString) // get input chars
		{
			if (inputChar == 32) // space 
			{
				textbox.text += inputChar;
			}
			else if (inputChar == 8) // backspace
			{
				if (textbox.text.Length > 0)
				{
					string temp = textbox.text;
					textbox.text = temp.Remove(textbox.text.Length - 1, 1);
				}

			}
			else if (inputChar >= 33 && inputChar <= 64) // special characters
			{
				textbox.text += inputChar;
			}
			else
			{
				char temp = char.ToUpper(inputChar);
				if (temp >= 65 && temp <= 90) // 26 letters
				{
					int flowerIndex = temp - 65;
					SpawnFlower(flowerIndex);
					textbox.text += inputChar;
				}
			}
		}
	}
}
