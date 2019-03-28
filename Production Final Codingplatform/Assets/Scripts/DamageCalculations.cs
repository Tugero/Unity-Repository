using UnityEngine;

public class DamageCalculations : MonoBehaviour
{
    public float MHP = 100.0f;
    private Vector3 spawnPoint;
    private float CHP;
    public GameObject mob;

    void Start()
    {
        CHP = MHP;
    }

    public void TakeDamage(float DMG)
    {
        CHP -= DMG;
        Debug.Log(""+CHP);
        if(CHP <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        Debug.Log(gameObject);
        Debug.Log("Was Killed!");
        Destroy(gameObject);
    }
}

