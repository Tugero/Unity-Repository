using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHurt : MonoBehaviour
{
    public GameObject player;
    public float DMG = 25.0f;
    private float health;

    private void Start()
    {
        health = player.GetComponent<DamageCalculations>().CHP;
    }
    private void Update()
    {
        health = player.GetComponent<DamageCalculations>().CHP;
        if (health <= 0)
            ded();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
                player.GetComponent<DamageCalculations>().TakeDamage(DMG);
        }
        if (gameObject.tag == "Player")
        {
            if (other.tag == "Enemy")
            {
                other.GetComponent<DamageCalculations>().TakeDamage(DMG);
            }
        }
    }

    public void ded()
    {
        player.GetComponent<Animator>().SetInteger("Move", 6);
    }
}
