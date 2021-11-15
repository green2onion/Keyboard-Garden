using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PageFlipButton : MonoBehaviour
{

	[SerializeField] TextMeshPro textMesh;
	[SerializeField] bool isForward;
	Animator animator;
	SpriteRenderer spriteRenderer;
	[SerializeField] GameObject pageFlip;
	Page page;
	// Start is called before the first frame update
	void Start()
	{
		animator = GetComponent<Animator>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.enabled = false;
		animator.enabled = false;
		page = FindObjectOfType<Page>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnMouseEnter()
	{
		spriteRenderer.enabled = true;
		animator.enabled = true;
		animator.Play(animator.GetCurrentAnimatorClipInfo(0)[0].clip.name, 0);

	}
	private void OnMouseExit()
	{
		//animator.Play("rightCorner", 0, 1);
		spriteRenderer.enabled = false;
		animator.enabled = false;
	}
	private void OnMouseDown()
	{
		if (!page.isFadingIn && !page.isFadingOut)
		{
			if (!isForward)
			{
				if (page.currentPage > 0)
				{
					Debug.Log("clicked");
					textMesh.gameObject.GetComponent<Page>().NextPage(isForward);
					pageFlip.SetActive(true);
				}

			}
			else
			{
				Debug.Log("clicked");
				textMesh.gameObject.GetComponent<Page>().NextPage(isForward);
				pageFlip.SetActive(true);
			}
		}
		

	}
}
