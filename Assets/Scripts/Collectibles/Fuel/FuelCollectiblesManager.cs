using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelCollectiblesManager : MonoBehaviour
{
    [SerializeField] GameObject fuelCollectiblePrefab;
    public float heightOffset=1;

    [Range(0,1)]
    public float probabilityToCreateFuelCollectible;

    public void GenerateFuelCollectible(Vector3 position)
    {
        if (!isProbableToCreateFuelCollectible()) return;

        Vector3 positionWithHeightOffset = new Vector3(position.x, position.y + heightOffset, 0);
        Instantiate(fuelCollectiblePrefab, positionWithHeightOffset, Quaternion.identity);
    }

    public bool isProbableToCreateFuelCollectible()
    {       
        if(Random.Range(0f, 1f) <= probabilityToCreateFuelCollectible)
        {
            //Debug.Log("probable");
            return true;
        }
        else
        {
           // Debug.LogError("not probable");
            return false;
        }
    }
}
