using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Produce : MonoBehaviour
{

    //Vector2[] trajectoryAngles = {new Vector2()}

    public float startForce = 15f;

    //references
    Rigidbody2D rb;
    public GameObject explosion;
    public GameObject GameOverUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Blade") //collision detection for slice (slice check is done is slice no need to worry)
        {
            //instantiate and destroy and increment score
            GameObject spawnedParticle = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(spawnedParticle, 3f);

            ScoreScript.scoreValue += 1;
        }
        if (collision.tag == "GameOver") // collision detection for GameOver
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            ScoreScript.scoreValue = 0;
            //Instantiate(GameOverUI);
            //GameOverUI.SetActive(true);
        }

    }



    // Start is called before the first frame update
    void Start()
    {
        // the force behind the fruits ejection, kinda confused about transform.up but it works
        startForce = Random.Range(10, 15);

        rb = GetComponent<Rigidbody2D>();

        rb.AddForce(transform.up* startForce, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
