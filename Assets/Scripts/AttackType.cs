using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface AttackType
{
    public bool attack(Entity attackingEntity);
}

public class SingleClosestTarget : AttackType
{
    Entity target;

    public bool attack(Entity attackingEntity)
    {
        if (attackingEntity.canAttack)
        {
            target = findClosestTarget(attackingEntity);

            if (target != null)
            {
                target.takeDamage(attackingEntity.damage);
                return true;
            }
        }
        //Debug.Log("SingleClosestTarget attack " + attackingEntity.name);

        return false;
    }

    private Entity findClosestTarget(Entity attackingEntity)
    {
        // get all entity colliders in range
        Collider2D[] hitColliders= Physics2D.OverlapCircleAll(attackingEntity.transform.position, attackingEntity.range);

        Entity nearest = null;
        Entity temp;
        float nearDist = float.PositiveInfinity;

        // loop over all colliders
        for (int i = 0; i < hitColliders.Length; i++)
        {
            // get the entity for the current collider
            bool hasEntity = hitColliders[i].transform.gameObject.TryGetComponent<Entity>(out temp);

            // check that the current entity is the right target type
            if (hasEntity && temp.family == attackingEntity.targetFamily)
            {
                Vector3 offset = attackingEntity.transform.position - hitColliders[i].transform.position;
                float thisDist = offset.sqrMagnitude;
                
                if (temp.name.StartsWith("Drain"))
                {
                    // override the check for closest by setting nearest target to drain
                    // and quit the loop early so we don't run unnecessary code
                    nearest = temp;
                    break;
                }
                // if distance to current target is < distance from current nearest target
                // set current target to be the new nearest
                if (thisDist < nearDist)
                {
                    nearDist = thisDist;
                    nearest = temp;
                }

                //Debug.Log(attackingEntity.name + " found target: " + nearest.name);
            }
        }

        return nearest;
    }
}

public class SingleFurthestTarget : AttackType
{
    Entity target;

    public bool attack(Entity attackingEntity)
    {
        //Debug.Log("SingleFurthestTarget attack" + attackingEntity.name);

        target = findFurthestTarget(attackingEntity);

        if (target != null)
        {
            target.takeDamage(attackingEntity.damage);
            return true;
        }

        return false;
    }

    private Entity findFurthestTarget(Entity attackingEntity)
    {
        // get all entity colliders in range
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackingEntity.transform.position, attackingEntity.range);

        Entity furthest = null;
        Entity temp;
        float farDist = 0.0F;

        // loop over all colliders
        for (int i = 0; i < hitColliders.Length; i++)
        {
            bool hasEntity = hitColliders[i].transform.gameObject.TryGetComponent<Entity>(out temp);

            // check that the current entity is the right target type
            if (hasEntity && temp.family == attackingEntity.targetFamily)
            {
                Vector3 offset = attackingEntity.transform.position - hitColliders[i].transform.position;
                float thisDist = offset.sqrMagnitude;

                // if distance to current target is > distance from current nearest target
                // set current target to be the new furthest
                if (thisDist > farDist)
                {
                    farDist = thisDist;
                    furthest = temp;
                }

                Debug.Log(attackingEntity.name + " found target: " + furthest.name);
            }
        }

        return furthest;
    }
}

public class AreaOfEffect : AttackType
{
    List<Entity> targets;

    public bool attack(Entity attackingEntity)
    {
        //Debug.Log("AreaOfEffect attack" + attackingEntity.name);

        targets = getTargetsInRangeOfPosition(attackingEntity);
        int i = 0;

        // loop over all targets and deal damage to them
        while (targets[i] != null)
        {
            targets[i].takeDamage(attackingEntity.damage);
            i++;
        }

        // return true if the list had anything in it
        return (targets[0] != null);
    }

    private List<Entity> getTargetsInRangeOfPosition(Entity attackingEntity)
    {
        // get all entity colliders in range
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackingEntity.transform.position, attackingEntity.range);

        List<Entity> targets = new List<Entity>();
        Entity temp;

        // loop over all colliders
        for (int i = 0; i < hitColliders.Length; i++)
        {
            bool hasEntity = hitColliders[i].transform.gameObject.TryGetComponent<Entity>(out temp);

            // check that the current entity is the right target type
            // and add it to the list
            if (hasEntity && temp.family == attackingEntity.targetFamily)
            {
                targets.Add(temp);
            }
        }

        return targets;
    }
}
