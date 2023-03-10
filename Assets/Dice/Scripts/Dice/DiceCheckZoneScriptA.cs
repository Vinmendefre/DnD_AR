using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceCheckZoneScriptA : MonoBehaviour
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
                case "Side0": 
                    DiceNumberTextScript.diceNumber1 = 9;
                    break;
                case "Side1": 
                    DiceNumberTextScript.diceNumber1 = 8;
                    break;
                case "Side2": 
                    DiceNumberTextScript.diceNumber1 = 7;
                    break;
                case "Side3": 
                    DiceNumberTextScript.diceNumber1 = 6;
                    break;
                case "Side4": 
                    DiceNumberTextScript.diceNumber1 = 5;
                    break;
                case "Side5": 
                    DiceNumberTextScript.diceNumber1 = 4;
                    break;
                case "Side6": 
                    DiceNumberTextScript.diceNumber1 = 3;
                    break;
                case "Side7": 
                    DiceNumberTextScript.diceNumber1 = 2;
                    break;
                case "Side8": 
                    DiceNumberTextScript.diceNumber1 = 1;
                    break;
                case "Side9": 
                    DiceNumberTextScript.diceNumber1 = 10;
                    break;
                
                    
            }
            
        }
    }
}
