using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    private float xRange;


    private GameManager gameManager;

    [SerializeField] ParticleSystem particle;

    public delegate void FruitCollidedHandler (int fruitPointValue);
    public event FruitCollidedHandler FruitCollided;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        xRange = gameManager.xGameplayRange;
    }

    void Update()
    {
        if(gameManager.isGameActive)
        {
            transform.Translate(Vector3.right * Time.deltaTime * GetInputHorizontal() * speed);
            CheckPosition();
        }

    }

    float GetInputHorizontal() 
        => Input.GetAxis("Horizontal");

    void CheckPosition()
    {
        if(transform.position.x > xRange) transform.position = new Vector3(xRange, 0, 0);

        else if(transform.position.x < -xRange)  transform.position = new Vector3(-xRange, 0, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        FruitCollided?.Invoke(collision.gameObject.GetComponent<Target>().pointValue);

        var fruitPosition = collision.gameObject.transform.position;
        fruitPosition.z -= 0.5f;

        if(fruitPosition.y > 0.75f)
        {
            Instantiate(particle, fruitPosition, particle.transform.rotation);
            Destroy(collision.gameObject);
        }
    }
}
