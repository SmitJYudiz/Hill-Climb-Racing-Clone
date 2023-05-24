using System.Collections;
using System.Collections.Generic;
using Portadown.UIKit;
using UnityEngine;

public class CarBodyBehaviour : MonoBehaviour
{

    [SerializeField] Transform parentTransform;

    float ultaOffset;
    bool canCallGameOver;
    private void Start()
    {
        ultaOffset = 0f;
        canCallGameOver = true;
        Events.OnGameRestart += changeBooleanCanCallGameOver;
    }

    public void changeBooleanCanCallGameOver()
    {
        canCallGameOver = true;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {       
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer (LayerMask.LayerToName(8)))
        {                      
            if (Vector3.Dot(parentTransform.up, Vector3.down) > ultaOffset && canCallGameOver)
            {                
                Events.GameOver();
                canCallGameOver = false;
            }
        }
    }
}
