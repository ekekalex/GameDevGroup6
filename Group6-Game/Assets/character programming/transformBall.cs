using System;
using System.ComponentModel;
using UnityEngine;

//this is for the transformation of the character into the ball form
//when a key assign is pressed, the character would transform into a ball
//this script must be attached to a parent (empty parent) that have the "normal" and "ball" forms as childs
public class TransformBall : MonoBehaviour
{
    private GameObject normalForm;
    private GameObject ballForm;

    //key assigned
    private KeyCode toggleKey = KeyCode.T;
    private bool isBall = false;

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            isBall = !isBall;
            normalForm.SetActive(!isBall);
            ballForm.SetActive(isBall);
        }
    }
}