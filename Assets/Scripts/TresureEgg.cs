using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TresureEgg : MonoBehaviour
{
    public Animator anim;
    [SerializeField] GameObject chest;
    [SerializeField] GameObject childChest;
    public void Start()
    {
        
        if (chest != null)
        {
            chest = GameObject.Find("ChestColider");
            childChest = chest.transform.Find("Chest").gameObject;
            childChest.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        rollDice();
    }

    private void OnTriggerExit(Collider other)
    {
        if (childChest.activeSelf)
        {
            anim.Play("close");
            StartCoroutine(waitForChestAnimation(childChest));
        }
    }

    private IEnumerator waitForChestAnimation(GameObject childChest)
    {
        yield return new WaitForSeconds(1); 
        childChest.SetActive(false);
    }

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

        if (number >= 10)
        {
            childChest.SetActive(true);
            anim.Play("open");
        }
        else if (number >= 0 && number <= 4)
        {
            
        }
        else
        {
            
        }
    }

    private bool checkDiceVelocityisZero()
    {
        return GameObject.Find("DiceCheckZone").GetComponent<CheckZoneScript>().diceVelocityIsZero();
    }
    
}
