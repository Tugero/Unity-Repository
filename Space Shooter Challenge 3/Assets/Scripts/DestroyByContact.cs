using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject Explosion;
    public GameObject PlayerExplosion;
    public int ScoreValue;
    private GameController gameController;

    private void Start()
    {
        GameObject gameControllerobject = GameObject.FindWithTag("GameController");
        if (gameControllerobject != null)
        {
            gameController = gameControllerobject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("I can't find the 'GameController' script :(");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundry") || other.CompareTag("Enemy"))
            return;

        if (Explosion != null)
        Instantiate(Explosion, transform.position, transform.rotation);

        if (other.tag == "Player")
        {
            Instantiate(PlayerExplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();
        }
       gameController.AddScore(ScoreValue);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
