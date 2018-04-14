using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float bulletSpeed = 7f;
    public float lifeDuration = 5;

    private float currentLife = 0;

    private GameObject triggeringEnemy;
    public float damage = 1;

    // Update is called once per frame
    void Update()
    {


        transform.Translate(Vector3.forward * Time.deltaTime * bulletSpeed);
        //transform.position += transform.forward * Time.deltaTime * bulletSpeed;
        currentLife += 1 * Time.deltaTime;

        if (currentLife >= lifeDuration)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        print("BULLET COLLISION! with " + other.name);

        if (other.tag == "Enemy")
        {
            triggeringEnemy = other.gameObject;
            triggeringEnemy.GetComponent<BasicEnemy>().health -= damage;
            Destroy(this.gameObject);
        }
    }
}
