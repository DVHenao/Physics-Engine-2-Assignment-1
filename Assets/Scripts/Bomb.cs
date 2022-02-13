using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Bomb : MonoBehaviour
{

    //Vector2[] trajectoryAngles = {new Vector2()}

    public float startForce;

    //references
    Rigidbody2D rb;
    public GameObject explosion;
    public GameObject GameOverUI;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Blade") //collision detection for slice (slice check is done is slice no need to worry)
        {
            Debug.Log("bomb hit!");
            //instantiate and destroy and increment score
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            ScoreScript.scoreValue = 0;
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        // the force behind the fruits ejection, kinda confused about transform.up but it works
        startForce = Random.Range(10, 15);

        rb = GetComponent<Rigidbody2D>();

        rb.AddForce(transform.up * startForce, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
