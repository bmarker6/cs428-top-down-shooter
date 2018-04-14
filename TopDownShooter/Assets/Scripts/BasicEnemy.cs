using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    //Variables
    public float health = 3;

    public Animator anim;

    //Methods


    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
//            print("Enemy " + this.gameObject.name + " has died");
            anim.Play("Die");
            StartCoroutine(WaitToDie());
            Destroy(this.gameObject);
        }
    }

    IEnumerator WaitToDie()
    {
        yield return new WaitForSeconds(2);
    }
}
