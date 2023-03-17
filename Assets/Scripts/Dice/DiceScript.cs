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

        if (spaceKeyIsPressed())
        {
            NumberTextScript.diceNumber = 0;
            moveToRandomPosition();
            transform.rotation = Quaternion.identity;
            resetVelocity();
            addRandomForce();
            addRandomTorque();
        }
    }

    private bool spaceKeyIsPressed()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    private void resetVelocity()
    {
        rb.velocity = new Vector3(0, 0, 0);
    }

    private void addRandomTorque()
    {
        float dirX = Random.Range(250, 3250);
        float dirY = Random.Range(300, 3300);
        float dirZ = Random.Range(350, 3350);
        rb.AddTorque(dirX, dirY, dirZ);
    }

    private void addRandomForce()
    {
        float forceUp = Random.Range(1700, 2500);
        rb.AddForce(transform.up * forceUp);
    }

    private void moveToRandomPosition()
    {
        float posX = Random.Range(-3, 3);
        float posY = Random.Range(2, 4);
        float posZ = Random.Range(-3, 3);
        transform.position = new Vector3(posX, posY, posZ);
    }
}
