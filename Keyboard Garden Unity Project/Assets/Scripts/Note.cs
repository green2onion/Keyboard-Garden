using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
	[SerializeField] float decayTimer;
	public enum instrument // not in use yet, could be useful later
	{
		piano,
		violin,
		guitar
	}
	public AudioClip audioClip; // the audioclip this flower plays, assigned by KeyboardInput
	public AnimationClip animationClip; //the animation clip this flower plays, assigned by KeyboardInput
	public SpriteRenderer spriteRenderer;
	public Sprite sprite;
	Animation anim;
	AudioSource audioSource; // the flower's AudioSource component
	Animator animator;
	bool isDecaying;
	// Start is called before the first frame update
	void Start()
	{
		//AssignColor();
		isDecaying = false;
		animator = GetComponent<Animator>();
		animator.speed = 1 / (audioClip.length / animator.GetCurrentAnimatorStateInfo(0).length);
		audioSource = GetComponent<AudioSource>();
		audioSource.clip = audioClip;
		audioSource.Play();
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = sprite;
		anim.clip = animationClip;
		anim.Play();

	}

	// Update is called once per frame
	void Update()
	{
		
		if (isDecaying)
		{
			decayTimer -= Time.deltaTime;
			if (decayTimer <= 0)
			{
				Destroy(gameObject);
			}
		}

		
	}

	public void StopPlaying()
	{
		animator.StopPlayback();
	}

	public void Decay()
	{
		
		animator.speed = 5;
		animator.SetFloat("Direction", -1);
		animator.Play("flowerSketch", -1, float.NegativeInfinity);
		isDecaying = true;

	}

	void AssignColor()
	{
		float r = Random.Range(0f, 1f);
		float g = Random.Range(0f, 1f);
		float b = Random.Range(0f, 1f);
		GetComponent<SpriteRenderer>().color = new Color(r, g, b);
	}
}
