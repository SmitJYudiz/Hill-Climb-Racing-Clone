using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SetEndTriggerBehaviour : MonoBehaviour
{
    //there should be action, which will be invoked when player, enters in ontrigger of this box collider, and then the subscripbed method should be in Terrain creator which will extend the path.
    public UnityEvent PlayerTriggeredSetEnd;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PlayerTriggeredSetEnd?.Invoke();
        }
    }
}
