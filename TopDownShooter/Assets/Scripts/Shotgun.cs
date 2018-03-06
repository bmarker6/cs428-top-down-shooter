using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun {

    private float maxSpread = 0.8f;
    private float minSpread = 0.1f;

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


            for (int i = 0; i < 5; i++)
            {

                var randomSpreadX = Random.Range(minSpread, maxSpread);
                var randomSpreadZ = Random.Range(minSpread, maxSpread);

                Vector3 test = spawn.transform.position;

                test = test + new Vector3(randomSpreadX, 0, randomSpreadZ);

                Instantiate(bullet.transform, test, Quaternion.LookRotation(spawn.forward));

                Debug.Log("Shot 1 bullet");
            }

            //Instantiate(bullet, spawn.transform.position, Quaternion.LookRotation(spawn.forward));


            Rigidbody newShell = Instantiate(shell, shellEjectionPoint.position, Quaternion.identity) as Rigidbody;
            newShell.AddForce(shellEjectionPoint.right * Random.Range(150f, 200f) + spawn.right * Random.Range(-10f, 10f));
        }
    }
}
