using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempScript : MonoBehaviour
{
    
    public void rollDice()
    {
        GameObject.Find("d20").GetComponent<DiceScript>().rollDice();

        StartCoroutine(waitFordice());
    }

    private IEnumerator waitFordice()
    {
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(checkDiceVelocityisZero);
        
        int number = CheckZoneScript.diceNumber;

        if (number > 15)
        {
            Debug.Log("ATTACK HIT with :" + number);
        }
        else
        {
            Debug.Log("ATTACK MISS with :" + number);
        }
    }

    private bool checkDiceVelocityisZero()
    {
        return GameObject.Find("DiceCheckZone").GetComponent<CheckZoneScript>().diceVelocityIsZero();
    }
}