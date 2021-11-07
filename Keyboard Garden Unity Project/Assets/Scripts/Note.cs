using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
	public float timer;
	public enum instrument // not in use yet, could be useful later
	{
		piano,
		violin,
		guitar
	}
	public AudioClip audioClip; // the audioclip this flower plays, assigned by KeyboardInput
	AudioSource audioSource; // the flower's AudioSource component
	Animator animator;
	// Start is called before the first frame update
	void Start()
	{
		//AssignColor();
		animator = GetComponent<Animator>();
		animator.speed = 1 / (audioClip.length / animator.GetCurrentAnimatorStateInfo(0).length);
		audioSource = GetComponent<AudioSource>();
		audioSource.clip = audioClip;
		audioSource.Play();
	}

	// Update is called once per frame
	void Update()
	{
		/*
		timer -= Time.deltaTime;
		if (timer <= 0)
		{
			Destroy(gameObject);
		}
		*/
	}

	void AssignColor()
	{
		float r = Random.Range(0f, 1f);
		float g = Random.Range(0f, 1f);
		float b = Random.Range(0f, 1f);
		GetComponent<SpriteRenderer>().color = new Color(r, g, b);
	}
}
