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
	RuntimeAnimatorController animatorController; //the animation clip this flower plays, assigned by KeyboardInput
	public SpriteRenderer spriteRenderer;
	public Sprite sprite;
	AudioSource audioSource; // the flower's AudioSource component
	Animator animator;
	bool isDecaying;

	public void SetRuntimeAnimatorController(RuntimeAnimatorController runtimeAnimatorController)
	{
		this.animatorController = runtimeAnimatorController;
		
	}
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
		animator.runtimeAnimatorController = animatorController as RuntimeAnimatorController;
		//anim.runtimeAnimatorController = animatorController;

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
		//Debug.Log("animation event triggered");
	}

	public void Decay()
	{

		animator.speed = 5;
		animator.SetFloat("Direction", -1);
		animator.Play(animator.GetCurrentAnimatorClipInfo(0)[0].clip.name, 0, 1f);
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
