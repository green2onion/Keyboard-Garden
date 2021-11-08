using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Page : MonoBehaviour
{
    TextMeshPro textbox;
    List<string> pages;
    int currentPage;
    List<GameObject> flowers;
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
    }

    public void NextPage()
	{
        AddPage();
        currentPage++;

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
}
