using UnityEngine;

public class DamageCalculations : MonoBehaviour
{
    public float MHP = 100.0f;
    private Vector3 spawnPoint;
    public float CHP;
    public GameObject mob;
    private bool isFlipped;

    void Start()
    {
        CHP = MHP;
        isFlipped = false;
    }

    public void TakeDamage(float DMG)
    {
        if (gameObject.tag == "Enemy")
        {
            Debug.Log("Damge Taken");
            CHP -= DMG;
            Debug.Log("" + CHP);
            if (CHP <= 0)
            {
                Death();
            }
        }
        if (gameObject.tag == "Player")
        {
            Debug.Log("Player Hurt");
            CHP -= DMG;
            Debug.Log("" + CHP);
            if (CHP <= 0)
            {
                Death();
            }
        }
    }

    public void Death()
    {
        if (gameObject.tag == "Enemy")
        {
            Debug.Log(gameObject);
            Debug.Log("Was Killed!");
            Destroy(mob);
        }
        if (gameObject.tag == "Player")
        {
            Debug.Log(gameObject);
            Debug.Log("Was Killed!");
            //Destroy(gameObject);

        }
    }

    public void ChangeGravity()
    {
      if (gameObject.tag == "Crystal")
        {
            if (isFlipped == false)
            {
                Debug.Log("Crystal Hit");
                Physics.gravity = new Vector3(0, 9.81f, 0);
            }
            if (isFlipped == true)
            {
                Debug.Log("Crystal Hit");
                Physics.gravity = new Vector3(0, -9.81f, 0);
                isFlipped = false;
            }
            if (Physics.gravity== new Vector3(0, 9.81f, 0))
                isFlipped = true;
            if (Physics.gravity == new Vector3(0, -9.81f, 0))
                isFlipped =false;
        }
    }
}

