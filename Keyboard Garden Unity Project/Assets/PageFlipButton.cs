using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PageFlipButton : MonoBehaviour
{

    [SerializeField] TextMeshPro textMesh;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


	private void OnMouseEnter()
	{
        Debug.Log("hovering");

	}
	private void OnMouseDown()
	{
        Debug.Log("clicked");
        textMesh.gameObject.GetComponent<Page>().NextPage();
	}
}
