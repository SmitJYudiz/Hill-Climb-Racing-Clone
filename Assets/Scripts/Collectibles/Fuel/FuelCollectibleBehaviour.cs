using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelCollectibleBehaviour : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FuelCollected();
    }

    public void FuelCollected()
    {
        gameObject.SetActive(false);
    }
}
