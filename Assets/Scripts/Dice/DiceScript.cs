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
            rollDice();
        }
    }

    public void rollDice()
    {
        
        CheckZoneScript.diceNumber = 0;
        moveToRandomPosition();
        transform.rotation = Quaternion.identity;
        resetVelocity();
        addRandomForce();
        addRandomTorque();
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
        float forceUp = Random.Range(65, 110);
        rb.AddForce(transform.up * forceUp);
    }

    private void moveToRandomPosition()
    {
        Bounds parentBounds = transform.parent.parent.GetComponent<Renderer>().bounds;
        float posY = Random.Range(0.02f, 0.04f);
        transform.position = new Vector3(parentBounds.center.x, posY, parentBounds.center.z);
    }
}
