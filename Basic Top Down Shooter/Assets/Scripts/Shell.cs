using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{

	private float lifeTime = 5;
	private Material mat;
	private Color startColor;
	private float fadePercent;
	private float deathTime;
	private bool fading;

	void Start ()
	{
		mat = GetComponent<Renderer>().material;
		startColor = mat.color;
		deathTime = Time.time + lifeTime;
		
		StartCoroutine("Fade");
	}

	IEnumerator Fade()
	{
		while (true)
		{
			yield return new WaitForSeconds(.2f);

			if (fading)
			{
				fadePercent += Time.deltaTime;
				mat.color = Color.Lerp(startColor, Color.clear, fadePercent);

				if (fadePercent >= 1)
				{
					Destroy(gameObject);
				}
			}
			else
			{
				if (Time.time > deathTime)
				{
					fading = true;
				}
			}
		}
	}

	private void OnTriggerEnter(Collider c)
	{
		if (c.tag == "Ground")
		{
			GetComponent<Rigidbody>().Sleep();
		}
	}
}
