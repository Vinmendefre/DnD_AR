using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimator : MonoBehaviour
{
    public GameObject projectile;
    private float launchVelocity = 150;

    public void Start()
    {
        projectile = GameObject.Find("TacticalRock");
    }

    public void animateAttack(GameObject selectedUnit, GameObject hitUnit, UnitSelection.AttackType selectedAttack)
    {
        selectedUnit.transform.LookAt(hitUnit.transform);
        Animation animation = selectedUnit.GetComponent<Animation>();
        animation["attack1"].wrapMode = WrapMode.Once;
        animation.Play("attack1");
        if (selectedAttack == UnitSelection.AttackType.ranged)
        {
            launchProjectile(selectedUnit);
        }
        StartCoroutine(returnToIdleAnimation(animation));
    }

    public void playDeathAnimation(GameObject hitUnit)
    {
        Animation animation = hitUnit.GetComponent<Animation>();
        // animation["death"].wrapMode = WrapMode.Loop;
        animation.Play("death");
    }
    
    private IEnumerator returnToIdleAnimation(Animation animation) {
        yield return new WaitForSeconds(3f);
        animation.Play("idle");
    }

    private void launchProjectile(GameObject selectedUnit)
    {
        GameObject tacticalRock = Instantiate(projectile, selectedUnit.transform.Find("RockLauncher").transform.position, selectedUnit.transform.Find("RockLauncher").transform.rotation);
        tacticalRock.GetComponent<Rigidbody>().AddRelativeForce(new Vector3( 0, launchVelocity, 0));
        Destroy(tacticalRock, 3f);
    }
}
