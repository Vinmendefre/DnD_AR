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
    }

    public void rollDice() {
        CheckZoneScript.diceNumber = 0;
        moveToRandomPosition();
        transform.rotation = Quaternion.identity;
        resetVelocity();
        addRandomForce();
        addRandomTorque();
    }

    private static bool spaceKeyIsPressed() {
        return Input.GetKeyDown(KeyCode.Space);
    }

    private static void resetVelocity() {
        rb.velocity = new Vector3(0, 0, 0);
    }

    private static void addRandomTorque() {
        rb.AddTorque(
            Random.Range(250, 3250),
            Random.Range(300, 3300),
            Random.Range(350, 3350)
        ); 
    }

    private void addRandomForce() {
        var forceUp = Random.Range(65, 110);
        rb.AddForce(transform.up * forceUp);
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void moveToRandomPosition() {
        var parentBounds = transform.parent.parent.GetComponent<Renderer>().bounds;
        var posY = Random.Range(0.02f, 0.04f);
        transform.position = new Vector3(parentBounds.center.x, posY, parentBounds.center.z);
    }
}
