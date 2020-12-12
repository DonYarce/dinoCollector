using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
	public GameObject explosion;
    public GameObject playerExplosion;

	public int scoreValue;
	public GameController gameController;

	void Start()
    {
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		gameController = gameControllerObject.GetComponent<GameController>();
    }

	void OnTriggerEnter(Collider other)
	{

        if (other.CompareTag("Boundary")) return;
	
        Instantiate(explosion, other.transform.position, other.transform.rotation);
        if (other.CompareTag("Player"))
        {
            
            Instantiate(playerExplosion, transform.position, transform.rotation);
            if (gameController.gameOver == false) { gameController.GameOver() ; }


		}
		gameController.AddScore(scoreValue);
		Destroy(other.gameObject); // Rayo o Jugador
		Destroy(gameObject); // Asteroide
	}
}
