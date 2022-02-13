using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    //references
    public GameObject Fruit;
    public GameObject Bomb;
    public Transform[] spawnPoints;

    //Delay between fruits (TINKER WITH THIS FOR MORE FUN)
    public float minDelayFruits = .1f;
    public float maxDelayFruits = 1f;

    public float minDelayBombs = 0.5f;
    public float maxDelayBombs = 2f;


    // Start is called before the first frame update
    void Start()
    {
        // Game loop for spawner
        StartCoroutine(SpawnFruits());
        StartCoroutine(SpawnBombs());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnFruits()
    {
        while(true) // always on
        {
            //delay
            float delay = Random.Range(minDelayFruits, maxDelayFruits);
            yield return new WaitForSeconds(delay);

            // random choice between set spawners
            int spawnIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[spawnIndex];


            // everything below is changing the angle of how the fruit is ejected from spawners depending on the spawner they begin from

            switch (spawnIndex)
            {
                case 0:
                    {
                        int angle = Random.Range(-10, 10);
                        spawnPoint.transform.eulerAngles = new Vector3(0, 0, angle);
                        //spawnPoint.rotation = new Vector3(0, 0, angle);
                        break;
                    }
                case 1:
                    {
                        int angle = Random.Range(0, 15);
                        spawnPoint.transform.eulerAngles = new Vector3(0, 0, angle);
                        //spawnPoint= new Vector3(0, 0, angle);
                        break;
                    }
                case 2:
                    {
                        int angle = Random.Range(0, -15);
                        spawnPoint.transform.eulerAngles = new Vector3(0, 0, angle);
                        break;
                    }
            }

                    // instantiate fruit at point AND provide reference so we can delete later
                    // THIS IS WHAT I WAS STUCK ON, USE FOR QBERT!!!!!!!!!!
            GameObject spawnedFruit = Instantiate(Fruit, spawnPoint.position, spawnPoint.rotation);

            Destroy(spawnedFruit, 5f);

            SpawnFruits();
        }
    }

    IEnumerator SpawnBombs()
    {
        while (true) // always on
        {
            //delay
            float delay = Random.Range(minDelayBombs, maxDelayBombs);
            yield return new WaitForSeconds(delay);

            // random choice between set spawners
            int spawnIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[spawnIndex];

            int angle = 0;


            // everything below is changing the angle of how the fruit is ejected from spawners depending on the spawner they begin from

            switch (spawnIndex)
            {
                case 0:
                    {
                        angle = Random.Range(-10, 10);
                        spawnPoint.transform.eulerAngles = new Vector3(0, 0, angle);
                        //spawnPoint.rotation = new Vector3(0, 0, angle);
                        break;
                    }
                case 1:
                    {
                        angle = Random.Range(-5, 15);
                        spawnPoint.transform.eulerAngles = new Vector3(0, 0, angle);
                        //spawnPoint= new Vector3(0, 0, angle);
                        break;
                    }
                case 2:
                    {
                        angle = Random.Range(5, -15);
                        spawnPoint.transform.eulerAngles = new Vector3(0, 0, angle);
                        break;
                    }
            }

            // instantiate fruit at point AND provide reference so we can delete later
            // THIS IS WHAT I WAS STUCK ON, USE FOR QBERT!!!!!!!!!!
            GameObject spawnedBomb = Instantiate(Bomb, spawnPoint.position, spawnPoint.rotation);

            yield return new WaitForSeconds(0.01f); // little delay to properly pull the velocity, idk how to pull startforce from bomb.cs but this works
            PhysicsCalc(spawnedBomb.GetComponent<Rigidbody2D>().velocity, angle);

            Destroy(spawnedBomb, 5f);

            SpawnBombs();
        }
    }

    void PhysicsCalc(Vector2 Fvelocity, int Fangle)
    {
        float maxHeight;
        // take angle give the real angle to the x axis
        if(Fangle > 0)
        {
            Fangle = 90 - Fangle;
        }
        if (Fangle < 0)
        {
            Fangle *= -1;
            Fangle = 90 - Fangle;
        }
        if (Fangle > 0)
        {
            Fangle = 90;
        }

        //begin calculations (H=V2*sin2(theta)/2*g) using this formula
        maxHeight = Mathf.Pow(Fvelocity.y,2) * Mathf.Pow( Mathf.Sin(Fangle), 2) / (2 * 9.8f);


        //this prints the maximum height/vertical displacement the bomb can reach measuring FROM ITS SPAWN POINT not world coords
        Debug.Log(maxHeight);


    }
}
