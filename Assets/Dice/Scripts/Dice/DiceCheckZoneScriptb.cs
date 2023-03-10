using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceCheckZoneScriptb : MonoBehaviour
{
    Vector3 diceVelocity;
    // Start is called before the first frame update
    private void FixedUpdate()
    {
        diceVelocity = DiceScriptA.diceVelocity;
    }

    // Update is called once per frame
    private void OnTriggerStay(Collider col)
    {
        if (diceVelocity.x == 0f && diceVelocity.y == 0f && diceVelocity.z == 0f)
        {
            switch (col.gameObject.name)
            {
                case "Side0b": 
                    DiceNumberTextScript.diceNumber2 = 9;
                    break;
                case "Side1b": 
                    DiceNumberTextScript.diceNumber2 = 8;
                    break;
                case "Side2b": 
                    DiceNumberTextScript.diceNumber2 = 7;
                    break;
                case "Side3b": 
                    DiceNumberTextScript.diceNumber2 = 6;
                    break;
                case "Side4b": 
                    DiceNumberTextScript.diceNumber2 = 5;
                    break;
                case "Side5b": 
                    DiceNumberTextScript.diceNumber2 = 4;
                    break;
                case "Side6b": 
                    DiceNumberTextScript.diceNumber2 = 3;
                    break;
                case "Side7b": 
                    DiceNumberTextScript.diceNumber2 = 2;
                    break;
                case "Side8b": 
                    DiceNumberTextScript.diceNumber2 = 1;
                    break;
                case "Side9b": 
                    DiceNumberTextScript.diceNumber2 = 10;
                    break;
                
                    
            }
            
        }
    }
}
