using System.Collections;
using System.Collections.Generic;
using Portadown.UIKit;
using UnityEngine;

public class CarBodyBehaviour : MonoBehaviour
{

    [SerializeField] Transform parentTransform;

    [SerializeField] float ultaOffset;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("opponent's layer "+collision.collider.gameObject.layer);
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer (LayerMask.LayerToName(8)))
        {

            Debug.Log("GameOver");
            Debug.Log("apna offset: "+ Vector3.Dot(parentTransform.up, Vector3.down));
            UIController.instance.ShowNextScreen(ScreenType.Gameover);
            if (Vector3.Dot(parentTransform.up, Vector3.down) > ultaOffset)
            {
                Debug.Log("GameOver");
                UIController.instance.ShowNextScreen(ScreenType.Gameover);
            }
        }
    }
}
