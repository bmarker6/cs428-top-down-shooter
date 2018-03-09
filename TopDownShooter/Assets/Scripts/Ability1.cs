using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability1 : MonoBehaviour
{


    AudioSource audioSource;
    public PlayerController player;
    public Texture2D ab1;
    public Texture2D ab1CD;
    float ab1Timer = 0;
    public float ab1Power;
    public float ab1Duration;
    public float ab1CDTime;
    
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ab1Timer -= Time.deltaTime;
    }

    void OnGUI()
    {

        bool ab1Key = Input.GetKeyDown(KeyCode.Q);

        if (ab1Timer <= 0)
        {

            GUI.Label(new Rect(100, 100, 50, 50), ab1);

            if (ab1Key)
            {
                AbilityOne();
            }

        }
        else
        {
            GUI.Label(new Rect(100, 100, 50, 50), ab1CD);
        }

    }

    void AbilityOne()
    {

        audioSource.Play();
        player.buffMoveSpeed(ab1Duration, ab1Power);
        ab1Timer = ab1CDTime;

    }

}
