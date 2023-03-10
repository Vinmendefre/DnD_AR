using System;
using System.Collections;
using DefaultNamespace;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class UnitSelection : MonoBehaviour
{
    [SerializeField] private LayerMask selectableUnitsLayer;
    [SerializeField] private GameObject selectionSphere;
    [SerializeField] private AttackAnimator attackAnimator;
    private GameObject selectedUnit;
    private float circleOffset = 0.3f;
    
    private enum AttackType { None, ranged, melee };
    [SerializeField] private GameObject attackPanel;
    private AttackType selectedAttack;
    
    [SerializeField] private GameObject outOfRangePanel;

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
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            // Raycast from camera to screen position
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
                        // Check if the selected unit can attack the clicked unit
                        float distance = Vector3.Distance(selectedUnit.transform.position, hitUnit.transform.position);
                        if (distance <= getAttackDistance())
                        {
                            Debug.Log(selectedUnit.name + " attacked " + hitUnit.name + "with" + selectedAttack);
                            attackAnimator.animateAttack(selectedUnit, hitUnit);
                            
                            UnitHealth hitUnitUnitHealth = hitUnit.GetComponent<UnitHealth>();
                            hitUnitUnitHealth.TakeDamage(15f);
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
        StartCoroutine(hideOutOfRangeMessage());
    }

    private IEnumerator hideOutOfRangeMessage() {
        yield return new WaitForSeconds(2f);
        outOfRangePanel.SetActive(false);
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
                return 0.05f;
            case AttackType.ranged:
                return 15f;
            default:
                return 0;
        }
    }

}