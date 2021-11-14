using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Page : MonoBehaviour
{
	TextMeshPro textbox;
	List<string> pages;
	public int currentPage;
	List<List<GameObject>> pagesOfFlowers;
	[SerializeField] float fadeMultiplier;
	public bool isFadingOut;
	public bool isFadingIn;
	public bool isForward;
	// Start is called before the first frame update
	void Start()
	{
		textbox = gameObject.GetComponent<TextMeshPro>();
		pages = new List<string>();
		pagesOfFlowers = new List<List<GameObject>>();
		AddPage();

	}

	// Update is called once per frame
	void Update()
	{
		textbox.text = pages[currentPage];



		if (isFadingOut)
		{
			TextFadeOut(isForward);
		}
		if (isFadingIn)
		{
			TextFadeIn();
		}
	}
	public void NextPage(bool isForward)  // +1 or -1 page
	{
		if (!isFadingIn && !isFadingOut)
		{
			this.isForward = isForward;
			if (isForward)
			{
				
				AddPage();
				isFadingOut = true;
			}
			else
			{
				if (currentPage>0)
				{
					isFadingOut = true;
				}
				
			}
			
		}
		
	}

	public void AddFlower(GameObject flower)
	{
		pagesOfFlowers[currentPage].Add(flower);
	}
	void AddPage()
	{
		pages.Add(string.Empty);
		List<GameObject> flowersOfThePage = new List<GameObject>();
		pagesOfFlowers.Add(flowersOfThePage);
	}

	public void AddCharToPage(char input)
	{
		pages[currentPage] += input;
	}
	public void Backspace()
	{
		string temp = pages[currentPage];
		pages[currentPage] = temp.Remove(pages[currentPage].Length - 1, 1);
		GameObject flowerToRemove = pagesOfFlowers[currentPage][pagesOfFlowers[currentPage].Count - 1];
		flowerToRemove.GetComponent<Note>().Decay();
		pagesOfFlowers[currentPage].Remove(flowerToRemove);
	}

	void TextFadeOut(bool isForward)
	{
		if(!isFadingIn)
		{
			if (textbox.alpha > 0)
			{
				textbox.alpha -= (Time.deltaTime * fadeMultiplier);
				foreach(GameObject flower in pagesOfFlowers[currentPage])
				{
					Color tempColor = flower.GetComponent<SpriteRenderer>().color;
					tempColor.a -= (Time.deltaTime * fadeMultiplier);
					flower.GetComponent<SpriteRenderer>().color = tempColor;
				}

			}
			else
			{
				textbox.alpha = 0;
				isFadingOut = false;
				isFadingIn = true;
				if (isForward)
				{
					currentPage++;
				}
				else
				{
					currentPage--;
				}
				

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
				foreach (GameObject flower in pagesOfFlowers[currentPage])
				{
					Color tempColor = flower.GetComponent<SpriteRenderer>().color;
					tempColor.a += (Time.deltaTime * fadeMultiplier);
					flower.GetComponent<SpriteRenderer>().color = tempColor;
				}
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
