using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelArsenal;

public class Tower : MonoBehaviour
{

    [Header("Tower")]
    [SerializeField] Transform weapon;
    [SerializeField] float range = 10f;
    [SerializeField] float damage = 2f;
    public float Damage  {get {return damage;}}
    float targetDistance;

    [SerializeField] float rangeIndicatorMod = 1.5f;
    [SerializeField] ParticleSystem projectileParticle;
    [SerializeField] int cost  = 75;
    public AudioSource audioSource;
    public AudioClip fire;
    public AudioClip deploy;

    Transform target;
    [Header("Range Indicator")]
    public Mesh mesh;
    public Material material;
    
    [Header("Projectile")]
    [SerializeField]
    public GameObject projectiles;
    [Header("Missile spawns at attached game object")]
    public Transform spawnPosition;
    [HideInInspector]
    public int currentProjectile = 0;
    public float speed = 500;
    GameObject projectile;
    

    public List<ParticleCollisionEvent> collisionEvents;
    private void Start()
    {

        

        collisionEvents = new List<ParticleCollisionEvent>();
        projectiles.GetComponent<VoxelProjectileScript>().bulletDamage = Damage;
    }

    void Update()
    {
        FindClosestTarget();
        AimWeapon();
        
    }
    private void OnMouseOver()
    {
        DrawRange();
    }

    void FindClosestTarget() 
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach (Enemy enemy in enemies)
        {
            targetDistance = Vector3.Distance(transform.position, enemy.transform.position);
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
      
        targetDistance = Vector3.Distance(transform.position, target.position);
        weapon.LookAt(target);

        if (targetDistance <= range)
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
        if (isActive)
        {
            if (audioSource != null)
            {
                audioSource.clip = fire;
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(fire);
                }
            }
            if (projectile == null)
            {
                projectile = Instantiate(projectiles, spawnPosition.position, Quaternion.identity) as GameObject; //Spawns the selected projectile
                projectile.transform.LookAt(target); //Sets the projectiles rotation to look at the point clicked
                projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * speed); //Set the speed of the projectile by applying force to the rigidbody
            }
        }

        //var emissionComp = projectileParticle.emission;
        //emissionComp.enabled = isActive;
    }
    void DrawRange()
    {
        float zDrawOffset = 0.75f;
        Vector3 rangeIndicatorVector = new Vector3(range * rangeIndicatorMod, range * (rangeIndicatorMod * zDrawOffset), range * rangeIndicatorMod);
        Matrix4x4 trsMatrix = Matrix4x4.TRS(transform.position, Quaternion.identity, rangeIndicatorVector);
        Graphics.DrawMesh(mesh, trsMatrix, material, 1);
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
            if (audioSource != null)
            {
                audioSource.clip = deploy;
                audioSource.PlayOneShot(deploy);
            }

            return true;
        }
        else
        {
            return false;
        }

    }

    
}


