using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class TresureEgg : MonoBehaviour
{
    public Animator anim;
    [SerializeField] GameObject chest;
    [SerializeField] GameObject childChest;
    [SerializeField] private AttackAnimator attackAnimator;
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
        rollDice(other);
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

    public void rollDice(Collider other)
    {
        GameObject.Find("d20").GetComponent<DiceScript>().rollDice();

        StartCoroutine(waitFordice(other));
    }

    private IEnumerator waitFordice(Collider other)
    {
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(checkDiceVelocityisZero);
        
        int number = CheckZoneScript.diceNumber;
        Debug.Log("WÜRFEL=>"  + number);
        if (number >= 10)
        {
            childChest.SetActive(true);
            anim.Play("open");
            GameObject gobo = other.gameObject;               
            UnitHealth hitUnitUnitHealth = gobo.GetComponent<UnitHealth>();
            hitUnitUnitHealth.Heal(15f);
        }
        else if (number >= 0 && number <= 4)
        {
            GameObject gobo = other.gameObject;               
            UnitHealth hitUnitUnitHealth = gobo.GetComponent<UnitHealth>();
            hitUnitUnitHealth.TakeDamage(15f);
            if (hitUnitUnitHealth.isDead)
            {
                attackAnimator.playDeathAnimation(gobo);
            }
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
