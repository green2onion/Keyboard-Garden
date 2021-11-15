using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Page : MonoBehaviour
{
	TextMeshPro textbox;
	List<string> pages;
	public List<bool> isLocked;
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
		isLocked = new List<bool>();
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
		isLocked.Add(false);
	}

	public void AddCharToPage(char input)
	{
		if (!isLocked[currentPage])
		{
			pages[currentPage] += input;
		}
		
	}
	public void Backspace()
	{
		if (!isLocked[currentPage])
		{
			string temp = pages[currentPage];
			pages[currentPage] = temp.Remove(pages[currentPage].Length - 1, 1);
			GameObject flowerToRemove = pagesOfFlowers[currentPage][pagesOfFlowers[currentPage].Count - 1];
			flowerToRemove.GetComponent<Note>().Decay();
			pagesOfFlowers[currentPage].Remove(flowerToRemove);
		}

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
			else // finished fading out, loads next/previous page
			{
				textbox.alpha = 0;
				foreach (GameObject flower in pagesOfFlowers[currentPage])
				{
					Color tempColor = new Color(255, 255, 255, 0);
					flower.GetComponent<SpriteRenderer>().color = tempColor;
				}
				isFadingOut = false;
				isFadingIn = true;
				isLocked[currentPage] = true;
				if (isForward)
				{
					currentPage++;
				}
				else
				{
					currentPage--;
				}
				StopAllCoroutines();
				StartCoroutine(playAudioSequentially());

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
				foreach (GameObject flower in pagesOfFlowers[currentPage])
				{
					Color tempColor = new Color(255, 255, 255, 1);
					flower.GetComponent<SpriteRenderer>().color = tempColor;
				}
				isFadingIn = false;
				Debug.Log("no longer fading in");
			}
		}

	}




	IEnumerator playAudioSequentially()
	{
		AudioSource adSource = GetComponent<AudioSource>();
		List<AudioClip> adClips = new List<AudioClip>();
		foreach(GameObject flower in pagesOfFlowers[currentPage])
		{
			adClips.Add(flower.GetComponent<Note>().audioClip);
		}


		yield return null;

		//1.Loop through each AudioClip
		for (int i = 0; i < adClips.Count; i++)
		{
			//2.Assign current AudioClip to audiosource
			adSource.clip = adClips[i];

			//3.Play Audio
			adSource.Play();

			//4.Wait for it to finish playing
			while (adSource.isPlaying)
			{
				yield return null;
			}

			//5. Go back to #2 and play the next audio in the adClips array
		}
	}
}
