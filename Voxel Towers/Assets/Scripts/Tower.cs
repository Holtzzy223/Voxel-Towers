using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] float range = 15f;
    [SerializeField] float rangeIndicatorMod = 1.5f;
    [SerializeField] ParticleSystem projectileParticle;
    [SerializeField] int cost  = 75;
    Transform target;

    public Mesh mesh;
    public Material material;

    private void Start()
    {
 
    }

    void Update()
    {
        FindClosestTarget();
        AimWeapon();
        
    }

    void FindClosestTarget() 
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach (Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if (targetDistance < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }
        target = closestTarget;
    }
    void AimWeapon()
    {
      
        float targetDistance = Vector3.Distance(transform.position, target.position);
        weapon.LookAt(target);

        if (targetDistance < range)
        {
           Attack(true);
        }
        else
        {
            Attack(false);
        }
    }
    void Attack(bool isActive)
    {
        var emissionComp = projectileParticle.emission;
        emissionComp.enabled = isActive;
    }
    void DrawRange()
    {
        Graphics.DrawMesh(mesh, Matrix4x4.TRS(transform.position, Quaternion.identity, new Vector3(range * rangeIndicatorMod, range * rangeIndicatorMod, range * rangeIndicatorMod)), material, 0);
    }
    private void OnMouseOver()
    {
        DrawRange();
    }
    public bool CreateTower(Tower tower, Vector3 position) 
    {
        PlayerBank bank = FindObjectOfType<PlayerBank>();
        if (bank == null)
        {
            return false;
        }
        if (bank.CurrentBalance >= cost)
        {
            Instantiate(tower, position, Quaternion.identity);
            bank.Withdrawl(cost);
            return true;
        }
        else
        {
            return false;
        }
    }
}
