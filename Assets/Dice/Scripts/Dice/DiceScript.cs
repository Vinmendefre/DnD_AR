using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceScript : MonoBehaviour
{
    static Rigidbody rb;
    public static Vector3 diceVelocity;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        
        diceVelocity = rb.velocity;

        if (SpaceKeyIsPressed())
        {
            resetVelocity();
            transform.rotation = Quaternion.identity;
            moveToRandomPosition();
            NumberTextScript.diceNumber = 0;
            addRandomForce();
            addRandomTorque();
        }
    }

    private static bool SpaceKeyIsPressed()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    private static void resetVelocity()
    {
        rb.velocity = new Vector3(0, 0, 0);
    }

    private static void addRandomTorque()
    {
        float dirX = Random.Range(0, 2000);
        float dirY = Random.Range(0, 2000);
        float dirZ = Random.Range(0, 2000);
        rb.AddTorque(dirX, dirY, dirZ);
    }

    private void addRandomForce()
    {
        float forceUp = Random.Range(1700, 2500);
        rb.AddForce(transform.up * forceUp);
    }

    private static void moveToRandomPosition()
    {
        float posX = Random.Range(-3, 3);
        float posZ = Random.Range(-3, 3);
        rb.transform.position = new Vector3(posX, 0, posZ);
    }
}
