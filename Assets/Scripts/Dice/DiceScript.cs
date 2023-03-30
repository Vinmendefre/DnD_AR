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
        liftUpFromCheckZone();
        CheckZoneScript.diceNumber = 0;
        transform.rotation = Quaternion.identity;
        resetVelocity();
        addRandomForce();
        addRandomTorque();
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
    private void liftUpFromCheckZone() {
        var parentBounds = transform.parent.parent.GetComponent<Renderer>().bounds;
        transform.position = new Vector3(parentBounds.center.x, 0.04f, parentBounds.center.z);
    }
}
