using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedoMeterBehaviour : MonoBehaviour
{
    [SerializeField] Rigidbody2D carRB;

    [SerializeField] Image speedoMeterFG;

    float maxFillAmount = 0.75f;

    float minFillAmount = 0f;

    private void Update()
    {
        
    }


}
