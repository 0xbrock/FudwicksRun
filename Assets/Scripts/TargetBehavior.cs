using UnityEngine;
using System.Collections;

public class TargetBehavior : MonoBehaviour
{

    // target impact on game
    public int scoreAmount = 0;
    public int underpantsAmount = 0;
    public float timeAmount = 0.0f;

    // explosion when hit?
    public GameObject explosionPrefab;

    // when collided with another gameObject
    void OnCollisionEnter(Collision newCollision)
    {
        // exit if there is a game manager and the game is over
        if (GameManager.gm)
        {
            if (GameManager.gm.gameIsOver)
                return;
        }

        if (this.tag == "Finish" && newCollision.gameObject.tag == "Player")
        {
            GameManager.gm.BeatLevel();
            return;
        }


        if (newCollision.gameObject.tag == "Player")
        {
            if (explosionPrefab)
            {
                // Instantiate an explosion effect at the gameObjects position and rotation
                Instantiate(explosionPrefab, transform.position, transform.rotation);
            }

            // if game manager exists, make adjustments based on target properties
            if (GameManager.gm)
            {
                if ((this.tag != "Goblin" && newCollision.gameObject.tag == "Player") || (this.tag == "Goblin" && newCollision.gameObject.tag == "Player" &&
                    newCollision.impulse.y < 0.0f && newCollision.contacts[0].point.y > 0.8f))
                {
                    GameManager.gm.targetHit(scoreAmount, underpantsAmount, timeAmount);
                }
            }

            // destroy the projectile
            //Destroy(newCollision.gameObject);

            // destroy self
            Destroy(gameObject);
        }
    }
}

