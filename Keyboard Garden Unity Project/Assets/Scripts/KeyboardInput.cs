using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardInput : MonoBehaviour
{
	public static KeyboardInput keyboardInput;
	[SerializeField] GameObject flower;

	[SerializeField] AudioClip[] notes;
	[SerializeField] AnimationClip[] notesAnim;
	[SerializeField] TextMeshPro textbox;
	Keyboard keyboard;
	[SerializeField] Vector2 flowerBoxTopLeft;
	[SerializeField] float flowerBoxHeight;
	[SerializeField] float flowerBoxWidth;
	Page page;


	private void Awake()
	{
		keyboardInput = this;
		keyboard = new Keyboard();
	}
	// Start is called before the first frame update
	void Start()
	{
		page = textbox.gameObject.GetComponent<Page>();
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
		newFlower.GetComponent<Note>().animationClip = notesAnim[flowerIndex];
		page.AddFlower(newFlower);

	}

	void CheckInput()
	{
		foreach (char inputChar in Input.inputString) // get input chars
		{
			if (inputChar == 32) // space == flower 33
			{
				//textbox.text += inputChar;
				page.AddCharToPage(inputChar);
				SpawnFlower(33);
			}
			else if (inputChar == 8) // backspace == flower 32
			{
				if (textbox.text.Length > 0)
				{
					string temp = textbox.text;
					//textbox.text = temp.Remove(textbox.text.Length - 1, 1);
					page.Backspace();
					//SpawnFlower(32); 
				}

			}
			else if (inputChar == '!')
			{
				SpawnFlower(27); // flower 27 is !
				//textbox.text += inputChar;
				page.AddCharToPage(inputChar);
			}
			else if (inputChar == ',')
			{
				SpawnFlower(28); // flower 28 is ,
				//textbox.text += inputChar;
				page.AddCharToPage(inputChar);
			}
			else if (inputChar == '.')
			{
				SpawnFlower(29); // flower 29 is .
				//textbox.text += inputChar;
				page.AddCharToPage(inputChar);
			}
			else if (inputChar == '?')
			{
				SpawnFlower(30); // flower 30 is !
				//textbox.text += inputChar;
				page.AddCharToPage(inputChar);
			}
			else if (inputChar == '\n')
			{
				SpawnFlower(31); // flower 31 is enter
				//textbox.text += inputChar;
				page.AddCharToPage(inputChar);
			}
			else if (inputChar >= 33 && inputChar <= 64) // other special characters, default sound
			{
				//textbox.text += inputChar;
				page.AddCharToPage(inputChar);
				SpawnFlower(34); // default sound == 34
			}
			else
			{
				char temp = char.ToUpper(inputChar);
				if (temp >= 65 && temp <= 90) // 26 letters
				{
					int flowerIndex = temp - 65;
					SpawnFlower(flowerIndex);
					//textbox.text += inputChar;
					page.AddCharToPage(inputChar);
				}
			}
		}
	}
}
