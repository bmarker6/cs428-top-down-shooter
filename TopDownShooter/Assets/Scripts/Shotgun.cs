using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun
{

    private float maxSpread = 0.2f;
    private float minSpread = -0.2f;

    public override void Shoot()
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


            for (int i = 0; i < 7; i++)
            {

                var randomSpreadX = Random.Range(minSpread, maxSpread);
                var randomSpreadZ = Random.Range(minSpread, maxSpread);

                Vector3 spread = spawn.transform.position;

                spread = spread + new Vector3(randomSpreadX, 0, randomSpreadZ);

                //Quaternion BulletAngle = Quaternion.LookRotation(test - spawn.forward);
                //Quaternion BulletRotation = Quaternion.Lerp(Quaternion.LookRotation(spawn.forward), BulletAngle, Time.deltaTime);
                Quaternion BulletTravel = Quaternion.LookRotation(spawn.forward + (spread - spawn.transform.position));

                Instantiate(bullet, spread, BulletTravel);
            }
            
            Rigidbody newShell = Instantiate(shell, shellEjectionPoint.position, Quaternion.identity) as Rigidbody;
            newShell.AddForce(shellEjectionPoint.right * Random.Range(150f, 200f) + spawn.right * Random.Range(-10f, 10f));
        }
    }
}
