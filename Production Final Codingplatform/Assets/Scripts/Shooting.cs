using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float DMG = 25.0f;
    public float Range = 100.0f;
    public Camera fpsCam;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit Hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out Hit, Range))
        {
            Debug.Log(Hit.transform.name);
            DamageCalculations target = Hit.transform.GetComponent<DamageCalculations>();
            if (target != null)
            {
                Debug.Log("Damge Taken");
                target.TakeDamage(DMG);
           }
        }


    }
}