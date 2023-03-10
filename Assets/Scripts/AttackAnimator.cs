using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimator : MonoBehaviour
{

    public void animateAttack(GameObject selectedUnit, GameObject hitUnit)
    {
        selectedUnit.transform.LookAt(hitUnit.transform);
        Animation animation = selectedUnit.GetComponent<Animation>();
        animation["attack1"].wrapMode = WrapMode.Once;
        animation.Play("attack1");
        StartCoroutine(returnToIdleAnimation(animation));
    }
    
    private IEnumerator returnToIdleAnimation(Animation animation) {
        yield return new WaitForSeconds(3f);
        animation.Play("idle");
    }
}
