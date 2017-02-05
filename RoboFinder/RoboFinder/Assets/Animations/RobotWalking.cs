using UnityEngine;
using System.Collections;

public class RobotWalking : MonoBehaviour {
    public float speed = 6f; //Speed of player
    public float turnSpeed = 60f; //Turn speed of player
    public float turnSmooth = 15f; //The turn smoothness of player

    private Vector3 movement;
    private Vector3 turning;
    private Animator anim;
    private Rigidbody robotBody;

//Entery point for Unity
    void Awake()
    {
        anim = GetComponent<Animator>(); //This is to get the animator component
        robotBody = GetComponent<Rigidbody>(); //This is to get the rigidbody component

    }
    void FixedUpdate()
    {
        float lh = Input.GetAxisRaw("Horizontal");
        float lv = Input.GetAxisRaw("Vertical");

        Move(lh, lv);
        RotatePlayer(lh, lv);
        Animation(lh, lv);
    }
    void Move(float lh, float lv) //This is where the player will move
    {
        movement.Set(lh, 0, lv);
        movement = movement.normalized * speed * Time.deltaTime; //setting the smoothness of movement and speed
        robotBody.MovePosition(transform.position + movement); //This is passing the movement into the player
    }
    void Animation(float lh, float lv) //Calls lh and lv
    {
        bool imRunning = false;
        if (lh != 0f || lv != 0f) //Stops animations when imRunning is not true
        {
            imRunning = true;
        }
        anim.SetBool("IsRunning", imRunning); //Gets the parameter from the animator in unity
        
    }
    void RotatePlayer(float lh, float lv)
    {
        if (lh!=0f || lv != 0f)
        {
            Vector3 targetDirection = new Vector3(lh, 0f, lv);
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
            Quaternion nearRotation = Quaternion.Lerp(GetComponent<Rigidbody>().rotation, targetRotation, turnSmooth * Time.fixedDeltaTime);
            GetComponent<Rigidbody>().MoveRotation(nearRotation);
        }
    }
}
