using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{


    //Variables
    public float health = 3;


    //Methods

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
            print("Enemy " + this.gameObject.name + " has died");
            Destroy(this.gameObject);
        }
    }
}
