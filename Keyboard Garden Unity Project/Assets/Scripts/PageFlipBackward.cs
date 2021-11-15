using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PageFlipBackward : MonoBehaviour
{

    [SerializeField] TextMeshPro textMesh;
    Animator animator;
    SpriteRenderer spriteRenderer;
    [SerializeField] GameObject pageFlip;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnMouseEnter()
    {
        spriteRenderer.enabled = true;

    }
    private void OnMouseDown()
    {
        Debug.Log("clicked");
        textMesh.gameObject.GetComponent<Page>().NextPage(false);
        pageFlip.SetActive(true);
    }
}
