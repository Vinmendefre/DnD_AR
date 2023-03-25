using UnityEngine;

public class CheckZoneScript : MonoBehaviour
{
    Vector3 diceVelocity;
    public static int diceNumber;

    private void FixedUpdate()
    {
        diceVelocity = DiceScript.diceVelocity;
    }

    private void OnTriggerStay(Collider col)
    {
        if (diceVelocityIsZero())
        {
            detectNumberOnTop(col);
        }
    }

    private static void detectNumberOnTop(Collider col)
    {
        diceNumber = col.gameObject.name switch
        {
            "Side1" => 20,
            "Side2" => 19,
            "Side3" => 18,
            "Side4" => 17,
            "Side5" => 16,
            "Side6" => 15,
            "Side7" => 14,
            "Side8" => 13,
            "Side9" => 12,
            "Side10" => 11,
            "Side11" => 10,
            "Side12" => 9,
            "Side13" => 8,
            "Side14" => 7,
            "Side15" => 6,
            "Side16" => 5,
            "Side17" => 4,
            "Side18" => 3,
            "Side19" => 2,
            "Side20" => 1,
            _ => diceNumber
        };
    }

    public bool diceVelocityIsZero()
    {
        return diceVelocity is { x: 0f, y: 0f, z: 0f };
    }
}