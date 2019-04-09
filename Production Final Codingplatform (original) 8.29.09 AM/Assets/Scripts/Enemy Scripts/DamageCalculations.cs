using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageCalculations : MonoBehaviour
{
    public float MHP;
    private Vector3 spawnPoint;
    public float CHP;
    public GameObject mob;
    private bool isFlipped;
    public GameObject player;

    void Start()
    {
        CHP = MHP;
        isFlipped = false;
        player = GameObject.Find("Player");
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
        if (gameObject.tag == "Vines")
        {
            if (player.GetComponent<PlayerMover>().hasScicle)
            {
                Debug.Log("Damge Taken");
                CHP -= DMG;
                Debug.Log("" + CHP);
                if (CHP <= 0)
                {
                    Death();
                }
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
        if (gameObject.tag == "Enemy" || gameObject.tag == "Vines")
        {
            Debug.Log(gameObject);
            Debug.Log("Was Killed!");
            Destroy(mob);
        }
        if (gameObject.tag == "Player")
        {
            SceneManager.LoadScene(3);
            Screen.lockCursor = false;
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

