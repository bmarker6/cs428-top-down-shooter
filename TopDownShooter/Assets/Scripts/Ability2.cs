using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability2 : MonoBehaviour {

    AudioSource audioSource;
    public Gun weapon;
    public Texture2D ab2;
    public Texture2D ab2CD;
    float ab2Timer = 0;
    public float ab2CDTime;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ab2Timer -= Time.deltaTime;
    }

    void OnGUI()
    {

        bool ab2Key = Input.GetKeyDown(KeyCode.R);

        if (ab2Timer <= 0)
        {

            GUI.Label(new Rect(160, 100, 50, 50), ab2);

            if (ab2Key)
            {
                AbilityTwo();
            }

        }
        else
        {
            GUI.Label(new Rect(160, 100, 50, 50), ab2CD);
        }

    }

    void AbilityTwo()
    {

        audioSource.Play();
        weapon.IncreaseROF(10, 300);

        ab2Timer = ab2CDTime;

    }
}
