using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Page : MonoBehaviour
{
	TextMeshPro textbox;
	List<string> pages;
	public int currentPage;
	List<GameObject> flowers;
	[SerializeField] float fadeMultiplier;
	public bool isFadingOut;
	public bool isFadingIn;
	// Start is called before the first frame update
	void Start()
	{
		textbox = gameObject.GetComponent<TextMeshPro>();
		pages = new List<string>();
		flowers = new List<GameObject>();
		AddPage();

	}

	// Update is called once per frame
	void Update()
	{
		textbox.text = pages[currentPage];



		if (isFadingOut)
		{
			TextFadeOut();
		}
		if (isFadingIn)
		{
			TextFadeIn();
		}
	}

	public void NextPage()  // +1 or -1 page
	{
		if (!isFadingIn && !isFadingOut)
		{
			AddPage();
			isFadingOut = true;
		}
		
	}

	public void AddFlower(GameObject flower)
	{
		flowers.Add(flower);
	}
	void AddPage()
	{
		pages.Add(string.Empty);
	}

	public void AddCharToPage(char input)
	{
		pages[currentPage] += input;
	}
	public void Backspace()
	{
		string temp = pages[currentPage];
		pages[currentPage] = temp.Remove(pages[currentPage].Length - 1, 1);
		GameObject flowerToRemove = flowers[flowers.Count - 1];
		flowerToRemove.GetComponent<Note>().Decay();
		flowers.Remove(flowerToRemove);
	}

	void TextFadeOut()
	{
		if(!isFadingIn)
		{
			if (textbox.alpha > 0)
			{
				textbox.alpha -= (Time.deltaTime * fadeMultiplier);


			}
			else
			{
				textbox.alpha = 0;
				isFadingOut = false;
				isFadingIn = true;
				currentPage++;

			}
		}



	}
	void TextFadeIn()
	{
		if (!isFadingOut)
		{
			if (textbox.alpha < 1f)
			{
				textbox.alpha += (Time.deltaTime * fadeMultiplier);
				Debug.Log(textbox.alpha);
			}
			else
			{
				textbox.alpha = 1f;
				isFadingIn = false;
				Debug.Log("no longer fading in");
			}
		}

	}
}
