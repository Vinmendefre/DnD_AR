using System;
using System.Collections;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class UnitSelection : MonoBehaviour
{
    [SerializeField] private LayerMask selectableUnitsLayer;
    [SerializeField] private GameObject selectionSphere;
    [SerializeField] private AttackAnimator attackAnimator;
    private GameObject selectedUnit;
    private float circleOffset = 0.3f;
    
    public enum AttackType { None, ranged, melee };
    [SerializeField] private GameObject attackPanel;
    private AttackType selectedAttack;
    
    [SerializeField] private GameObject outOfRangePanel;
    [SerializeField] private GameObject combatLogPanel;
    public TMP_Text combatLogText;

    [SerializeField] private GameObject attackSelectionIndicator;
    
    private void Start()
    {
        selectedAttack = AttackType.None;
        attackSelectionIndicator = GameObject.Find("attackSelectionIndicator");
        attackSelectionIndicator.SetActive(false);
        attackPanel = GameObject.Find("attackPanel");
        attackPanel.SetActive(false);
        outOfRangePanel = GameObject.Find("outOfRangePanel");
        outOfRangePanel.SetActive(false);
        selectionSphere = GameObject.Find("SelectionSphere");
        selectionSphere.SetActive(false);
        selectableUnitsLayer = LayerMask.GetMask("selectableUnitsLayer");
        attackAnimator = FindObjectOfType<AttackAnimator>();
        combatLogPanel = GameObject.Find("combatLogPanel");
        combatLogPanel.SetActive(false);
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, selectableUnitsLayer))
            {
                GameObject hitUnit = hit.collider.gameObject;
                if (hitUnit.CompareTag("selectableUnit"))
                {
                    if (selectedUnit != null && hitUnit == selectedUnit)
                    {
                        deselectUnit();
                        return;
                    }
                    else if (selectedUnit != null && selectedAttack != AttackType.None)
                    {
                        
                        float distance = Vector3.Distance(selectedUnit.transform.position, hitUnit.transform.position);
                        if (distance <= getAttackDistance())
                        {
                            rollDice(hitUnit);
                        }
                        else
                        {
                            displayOutOfRangeMessage();
                        }
                    } else if (selectedUnit == null)
                    {
                        selectUnit(hitUnit);
                    }
                }
            }
        }
    }

    private void selectUnit(GameObject hitUnit)
    {
        selectedUnit = hitUnit;
        selectionSphere.transform.position = selectedUnit.transform.position + Vector3.up * circleOffset;
        selectionSphere.SetActive(true);
        attackPanel.SetActive(true);
    }

    public void selectAttack(String attackType)
    {
        selectedAttack = (AttackType) Enum.Parse(typeof(AttackType), attackType, true);
        attackSelectionIndicator.transform.position = GameObject.Find(attackType + "Button").transform.position +  new Vector3(-50f, 0f,0f);
        attackSelectionIndicator.SetActive(true);
        Debug.Log("selectedAttack :" + selectedAttack);
    }
    
    
    private void displayOutOfRangeMessage() {
        outOfRangePanel.SetActive(true);
        StartCoroutine(hideGameObject(outOfRangePanel));
    }

    private IEnumerator hideGameObject(GameObject toHide) {
        yield return new WaitForSeconds(2f);
        toHide.SetActive(false);
    }

    private void deselectUnit()
    {
        selectionSphere.SetActive(false);
        attackPanel.SetActive(false);
        selectedUnit = null;
        selectedAttack = AttackType.None;
        attackSelectionIndicator.SetActive(false);
    }

    private float getAttackDistance()
    {
        switch (selectedAttack)
        {
            case AttackType.melee:
                return 0.35f;
            case AttackType.ranged:
                return 10f;
            default:
                return 0;
        }
    }
    
    public void rollDice(GameObject hitUnit)
    {
        GameObject.Find("d20").GetComponent<DiceScript>().rollDice();
        StartCoroutine(waitFordice(hitUnit));
    }

    private IEnumerator waitFordice(GameObject hitUnit)
    {
        yield return new WaitForSeconds(2);
        yield return new WaitUntil(checkDiceVelocityisZero);
        
        int dieRoll = CheckZoneScript.diceNumber;
        
        if (dieRoll >= 12)
        {
            combatLogText.text = "Rolled a " + dieRoll + " and hit ";
            combatLogPanel.SetActive(true);
            StartCoroutine(hideGameObject(combatLogPanel));
            attackAnimator.animateAttack(selectedUnit, hitUnit, selectedAttack);
                            
            UnitHealth hitUnitUnitHealth = hitUnit.GetComponent<UnitHealth>();
            hitUnitUnitHealth.TakeDamage(15f);
            if (hitUnitUnitHealth.isDead)
            {
                attackAnimator.playDeathAnimation(hitUnit);
            }
        } else
        {
            combatLogText.text = "Rolled a " + dieRoll + " and missed ";
            combatLogPanel.SetActive(true);
            StartCoroutine(hideGameObject(combatLogPanel));
            Debug.Log("Missed attack with: " + dieRoll);  
        }
    }

    private bool checkDiceVelocityisZero()
    {
        return GameObject.Find("DiceCheckZone").GetComponent<CheckZoneScript>().diceVelocityIsZero();
    }
    
    
}