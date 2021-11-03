using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardInput : MonoBehaviour
{
	public static KeyboardInput keyboardInput;
	[SerializeField] GameObject flower;
	List<GameObject> flowers;
	[SerializeField] AudioClip[] notes;
	[SerializeField] TextMeshPro textbox;
	Keyboard keyboard;
	[SerializeField] Vector2 flowerBoxTopLeft;
	[SerializeField] float flowerBoxHeight;
	[SerializeField] float flowerBoxWidth;



	private void Awake()
	{
		keyboardInput = this;
		keyboard = new Keyboard();
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
		// for debug use
		//Debug.DrawLine(new Vector3((flowerBoxTopLeft.x), flowerBoxTopLeft.y), new Vector3((flowerBoxBottomRight.x), flowerBoxBottomRight.y),Color.white, 1,false);
		var rect = new Rect(flowerBoxTopLeft.x, flowerBoxTopLeft.y, flowerBoxHeight, flowerBoxWidth);
		Debug.DrawLine(new Vector3(rect.x, rect.y), new Vector3(rect.x + rect.width, rect.y), Color.green, 0, false);
		Debug.DrawLine(new Vector3(rect.x, rect.y), new Vector3(rect.x, rect.y - rect.height), Color.red, 0, false);
		Debug.DrawLine(new Vector3(rect.x + rect.width, rect.y - rect.height), new Vector3(rect.x + rect.width, rect.y), Color.green, 0, false);
		Debug.DrawLine(new Vector3(rect.x + rect.width, rect.y - rect.height), new Vector3(rect.x, rect.y - rect.height), Color.red, 0, false);
	}


	Vector2 GetSpawnPosition(char key) // not in use, but probably will come handy later
	{
		Vector2 spawnPosition = new Vector2(0, 0);
		int[] myKeyPosition;
		for (int i = 0; i < keyboard.keyboardChars.Length; i++)
		{
			for (int j = 0; j < keyboard.keyboardChars[i].Length; j++)
			{
				if (key == keyboard.keyboardChars[i][j])
				{
					myKeyPosition = new int[2] { i, j };
				}
			}
		}

		return spawnPosition;
	}

	void SpawnFlower(int flowerIndex)
	{
		float spawnY = Random.Range(flowerBoxTopLeft.y, flowerBoxTopLeft.y - flowerBoxWidth);
		float spawnX = Random.Range(flowerBoxTopLeft.x, flowerBoxTopLeft.x + flowerBoxHeight);

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
