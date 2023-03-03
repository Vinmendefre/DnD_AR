using System;
using System.Collections;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class UnitSelection : MonoBehaviour
{
    public LayerMask selectableUnitsLayer;
    public GameObject selectionCircle;
    private GameObject selectedUnit;
    private float circleOffset = 0.3f;
    
    public enum AttackType { None, ranged, melee };
    public GameObject attackCanvas;
    public AttackType selectedAttack;
    
    public GameObject outOfRangeText;

    private void Start()
    {
        selectedAttack = AttackType.None;
    }

    private void Update()
    {
        // Check for mouse click
        
        if (Input.GetMouseButtonDown(0))
        {
            // Raycast from camera to screen position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, selectableUnitsLayer))
            {
                // Check if the raycast hit a selectable unit
                GameObject hitUnit = hit.collider.gameObject;
                if (hitUnit.CompareTag("selectableUnit"))
                {
                    if (selectedUnit != null && hitUnit == selectedUnit)
                    {
                        // If the clicked unit is already selected, deselect it
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
                        }
                        else
                        {
                            displayOutOfRangeMessage();
                            Debug.Log("Target out of range");
                        }
                    } else if (selectedUnit == null)
                    {

                        // Select the new unit and create the selection circle
                        selectedUnit = hitUnit;
                        Debug.Log("Unity Selected");
                        selectionCircle.transform.position = selectedUnit.transform.position + Vector3.up * circleOffset;
                        selectionCircle.SetActive(true);
                        attackCanvas.SetActive(true);
                    }
                    
                }
            }
        }
    }

    public void selectMeleeAttack(String attackType)
    {
        selectedAttack = (AttackType) Enum.Parse(typeof(AttackType), attackType, true);
        Debug.Log("selectedAttack" + selectedAttack);
    }
    
    
    private void displayOutOfRangeMessage() {
        outOfRangeText.SetActive(true);
        StartCoroutine(hideOutOfRangeMessage());
    }

    private IEnumerator hideOutOfRangeMessage() {
        yield return new WaitForSeconds(2f);
        outOfRangeText.SetActive(false);
    }

    private void deselectUnit()
    {
        selectionCircle.SetActive(false);
        attackCanvas.SetActive(false);
        selectedUnit = null;
        selectedAttack = AttackType.None;
        Debug.Log("unit deselected");
    }

    private float getAttackDistance()
    {
        switch (selectedAttack)
        {
            case AttackType.melee:
                return 0.001f;
            case AttackType.ranged:
                return 15f;
            default:
                return 0;
        }
    }

}