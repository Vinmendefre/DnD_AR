using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceScriptB : MonoBehaviour
{
    static Rigidbody rb;

    public static Vector3 diceVelocity;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        diceVelocity = rb.velocity;
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            DiceNumberTextScript.diceNumber2 = 0;
            float dirX = Random.Range(0, 1500);
            float dirY = Random.Range(0, 1500);
            float dirZ = Random.Range(0, 1500);
            
            transform.position = new Vector3(-1, 2, 1);
            transform.rotation = Quaternion.identity;
            rb.velocity = new Vector3(0,0,0);
            rb.AddForce(transform.up * 1500);
            rb.AddTorque(dirX, dirY, dirZ);
        }
    }
}
