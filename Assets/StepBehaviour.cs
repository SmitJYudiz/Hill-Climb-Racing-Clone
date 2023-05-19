using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepBehaviour : MonoBehaviour
{
    [SerializeField] HingeJoint2D myHingeJoint;
    [SerializeField] Rigidbody2D myRB;

    public Rigidbody2D MyRB
    {
        get { return myRB; }
        //set { myRB = value; }
    }
    
    public void SetConnectedRB(Rigidbody2D incomingRB)
    {
        myHingeJoint.connectedBody = incomingRB;
    }   
}
