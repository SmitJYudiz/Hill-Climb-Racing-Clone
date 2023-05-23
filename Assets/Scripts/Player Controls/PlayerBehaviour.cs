using System.Collections;
using System.Collections.Generic;
using Portadown.UIKit;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] Rigidbody2D playerRB;
    [SerializeField] float speed;

    public WheelJoint2D backWheel;
    public WheelJoint2D frontWheel;
   
    public Vector2 inputVector;

    

    void OnMovement(InputValue inputValue)
    {
        inputVector = inputValue.Get<Vector2>();
        MoveCar();

        IsCarFlipped();
    }
     void MoveCar()
    {
        if(inputVector == Vector2.zero)
        {
            frontWheel.useMotor = false;
            backWheel.useMotor = false;            
        }
        else
        {
            frontWheel.useMotor = true;
            backWheel.useMotor = true;
            JointMotor2D motor = new JointMotor2D { motorSpeed = speed * -inputVector.x, maxMotorTorque = 10000 };
            frontWheel.motor = motor;
            backWheel.motor = motor;
        }
    }

    //if car is flipped upside down, then gameover.
    void IsCarFlipped()
    {
        if(transform.rotation.z>160 && playerRB.velocity.magnitude <= 1)
        {
            UIController.instance.ShowNextScreen(ScreenType.Gameover);
        }
    }
}
