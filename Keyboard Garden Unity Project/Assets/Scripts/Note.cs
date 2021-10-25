using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public enum instrument // not in use yet, could be useful later
	{
        piano,
        violin,
        guitar
	}
    public AudioClip audioClip; // the audioclip this flower plays, assigned by KeyboardInput
    AudioSource audioSource; // the flower's AudioSource component
    // Start is called before the first frame update
    void Start()
    {
        AssignColor();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AssignColor() 
	{
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);
        GetComponent<SpriteRenderer>().color = new Color(r, g, b);
    }
}
