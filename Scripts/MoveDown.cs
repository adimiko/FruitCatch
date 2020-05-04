using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown : MonoBehaviour
{
    [SerializeField] float speed = 0.1f;

    private Rigidbody fruitRb;

    void Start()
    {
        fruitRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        fruitRb.AddForce(Vector3.down * speed);
    }
}
