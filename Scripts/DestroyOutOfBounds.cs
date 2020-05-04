using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    public event Action GoodFruitTouchedLowerBand;

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);

        if(collision.gameObject.CompareTag("Good Fruit")) GoodFruitTouchedLowerBand?.Invoke();
    }
}
