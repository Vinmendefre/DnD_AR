using System.Collections;
using System.Security.Cryptography;
using DefaultNamespace;
using UnityEngine;
using TMPro;

public class TresureEgg : MonoBehaviour
{
    public Animator anim;
    [SerializeField] GameObject chest;
    [SerializeField] GameObject childChest;
    [SerializeField] private AttackAnimator attackAnimator;
    
    [SerializeField] private GameObject combatLogPanel;
    public TMP_Text combatLogText;

    private int rolledNumber;

    public void Start()
    {
        
        if (chest != null)
        {
            chest = GameObject.Find("ChestColider");
            childChest = chest.transform.Find("Chest").gameObject;
            childChest.SetActive(false);
        }
        combatLogPanel = GameObject.Find("combatLogPanel");
        // combatLogPanel.SetActive(false);
        combatLogText.text = "hey you";
        combatLogPanel.SetActive(true);

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
        chest.SetActive(false);
    }
    
    private IEnumerator hideGameObject(GameObject toHide) {
        yield return new WaitForSeconds(2f);
        toHide.SetActive(false);
    }

    public void rollDice(Collider other)
    {
        GameObject.Find("d20").GetComponent<DiceScript>().rollDice();

        StartCoroutine(waitFordice(other));
    }

    private IEnumerator waitFordice(Collider other)
    {
        // yield return new WaitForSeconds(1);
        // yield return new WaitUntil(checkDiceVelocityisZero);
        yield return new WaitForSeconds(3);
        rolledNumber = CheckZoneScript.diceNumber;

        if (rolledNumber == 0)
        {
            rolledNumber = RandomNumberGenerator.GetInt32(1, 21);
        }

        Debug.Log("WÜRFEL=>"  + rolledNumber);
        if (rolledNumber >= 10)
        {
            combatLogText.text = "Rolled: " + rolledNumber + ": found a chest. Have some HP!";
            combatLogPanel.SetActive(true);
            StartCoroutine(hideGameObject(combatLogPanel));
            
            childChest.SetActive(true);
            anim.Play("open");
            GameObject gobo = other.gameObject;               
            UnitHealth hitUnitUnitHealth = gobo.GetComponent<UnitHealth>();
            hitUnitUnitHealth.Heal(15f);
        }
        else if (rolledNumber >= 0 && rolledNumber <= 4)
        {
            
            combatLogText.text = "Rolled " + rolledNumber + ". It's a mimic. Perish!";
            combatLogPanel.SetActive(true);
            StartCoroutine(hideGameObject(combatLogPanel));

            childChest.SetActive(true);
            anim.Play("open");
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
            combatLogText.text = "You rolled a " + rolledNumber + " and found nothing";
            combatLogPanel.SetActive(true);
            StartCoroutine(hideGameObject(combatLogPanel));

        }
    }

    private bool checkDiceVelocityisZero()
    {
        return GameObject.Find("DiceCheckZone").GetComponent<CheckZoneScript>().diceVelocityIsZero();
    }
    
}
