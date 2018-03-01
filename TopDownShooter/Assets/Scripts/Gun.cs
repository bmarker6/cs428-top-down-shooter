using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Gun : MonoBehaviour
{
	public enum GunType
	{
		Semi,
		Burst,
		Auto
	}

	public GunType gunType;
	public float rpm;

	// Components
	public Transform spawn;
	public Transform shellEjectionPoint;
	public Rigidbody shell;
	private LineRenderer tracer;
	private AudioSource audio;

	// System variables
	private float secondsBetweenShots;
	private float nextPossibleShootTime;

	void Start()
	{
		secondsBetweenShots = 60 / rpm;
		if (GetComponent<LineRenderer>())
		{
			tracer = GetComponent<LineRenderer>();
		}

		audio = GetComponent<AudioSource>();
	}

	public void Shoot()
	{
		if (CanShoot())
		{
			Ray ray = new Ray(spawn.position, spawn.forward);
			RaycastHit hit;

			float shotDistance = 20;

			if (Physics.Raycast(ray, out hit, shotDistance))
			{
				shotDistance = hit.distance;
			}

			nextPossibleShootTime = Time.time + secondsBetweenShots;

			audio.Play();

			if (tracer)
			{
				StartCoroutine("RenderTracer", ray.direction * shotDistance);
			}
			
			Rigidbody newShell = Instantiate(shell, shellEjectionPoint.position, Quaternion.identity) as Rigidbody;
			newShell.AddForce(shellEjectionPoint.right * Random.Range(150f, 200f) + spawn.right * Random.Range(-10f, 10f));
		}
	}

	public void ShootContinuous()
	{
		if (gunType == GunType.Auto)
		{
			Shoot();
		}
	}

	private bool CanShoot()
	{
		bool canShoot = true;

		if (Time.time < nextPossibleShootTime)
		{
			canShoot = false;
		}
		
		return canShoot;
	}

	IEnumerator RenderTracer(Vector3 hitPoint)
	{
		tracer.enabled = true;
		tracer.SetPosition(0, spawn.position);
		tracer.SetPosition(1, spawn.position + hitPoint);
		yield return null;
		tracer.enabled = false;
	}

}