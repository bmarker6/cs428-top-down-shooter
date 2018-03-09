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

    //Same value as RPM, for restoring after ROF Buff
    public float originalRPM;


    // Components
    public Transform spawn;
	public Transform shellEjectionPoint;
	public Rigidbody shell;
	protected AudioSource audio;
    public GameObject bullet;

    // System variables
    protected float secondsBetweenShots;
	protected float nextPossibleShootTime;

    private void Update()
    {

        ROFBuffTimer -= Time.deltaTime;
        if (ROFBuffTimer <= 0)
        {
            rpm = originalRPM;
            secondsBetweenShots = 60 / rpm;
        }
    }

    void Start()
	{
		secondsBetweenShots = 60 / rpm;

		audio = GetComponent<AudioSource>();
	}

	public virtual void Shoot()
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
            Instantiate(bullet, spawn.transform.position, Quaternion.LookRotation(spawn.forward));


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

	protected bool CanShoot()
	{
		bool canShoot = true;

		if (Time.time < nextPossibleShootTime)
		{
			canShoot = false;
		}
		
		return canShoot;
	}


    private float ROFBuffTimer = 0;



    // Rate Of Fire Buff Ability
    public void IncreaseROF(float duration, float boost)
    {
        rpm = originalRPM + boost;
        secondsBetweenShots = 60 / rpm;
        ROFBuffTimer = duration;
    }
}