using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelArsenal;
using TMPro;
public class Tower : MonoBehaviour
{

    [Header("Tower")]
    [SerializeField] Transform weapon;
    [SerializeField] float range = 10f;
    [SerializeField] float damage = 2f;
    public float Damage { get { return damage; } }
    float targetDistance;

    [SerializeField] float rangeIndicatorMod = 1.5f;
    [SerializeField] ParticleSystem projectileParticle;
    [SerializeField] int cost = 75;
    [SerializeField] int upgradeCost;

    public AudioSource audioSource;
    public AudioClip fire;
    public AudioClip deploy;

    Transform target;
    [Header("Range Indicator")]
    public Mesh mesh;
    public Mesh hilightMesh;
    public Material material;

    [Header("Projectile")]
    [SerializeField]
    public GameObject projectiles;
    public GameObject[] upgradeProjectiles;
    public float[] upgradeProjectileSpeeds;
    [Header("Missile spawns at attached game object")]
    public Transform spawnPosition;
    [HideInInspector]
    public int currentProjectile = 0;
    public float speed = 500;
    GameObject projectile;
    private int tier = 0;
    private int tierMax = 3;
    public GameObject onHoverUI;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI sellText;

    public List<ParticleCollisionEvent> collisionEvents;
    private void Start()
    {

        

        collisionEvents = new List<ParticleCollisionEvent>();
        projectiles.GetComponent<VoxelProjectileScript>().bulletDamage = Damage;
        
    }

    void Update()
    {
        switch (tier)
        {
            case 0:
                upgradeCost = Mathf.RoundToInt(cost * 0.5f);
                break;
            case 1:
                upgradeCost = Mathf.RoundToInt(cost * 0.65f);
                break;
            case 2:
                upgradeCost = Mathf.RoundToInt(cost * 0.85f);
                break;

        }
        
        costText.text = "- $"+ upgradeCost.ToString();
        sellText.text = "+ $" + Mathf.FloorToInt(cost * (tier + 1) * 0.50f).ToString();
        FindClosestTarget();
        var matColor = material.color;
        matColor.a = Random.Range(0.0f,0.5f);
    }
    private void FixedUpdate()
    {
        AimWeapon();

    }

    private void OnMouseOver()
    {
          
        DrawRange();
        var UI = FindObjectOfType<UpgradeUI>();
        if (Input.GetMouseButtonDown(0))
        {
            if (UI == null||UI.gameObject.activeInHierarchy == false)
            {
                onHoverUI.SetActive(true);
                Time.timeScale = 0;
            }
        }
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
                projectile.transform.LookAt(target); //Sets the projectiles rotation to look at the target
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
    void DrawHighLight() 
    {
      //  Vector3 scale = new Vector3(1.1f, 1.1f, 1.1f);
      //  //Vector3 rangeIndicatorVector = new Vector3(range * rangeIndicatorMod, range * (rangeIndicatorMod * zDrawOffset), range * rangeIndicatorMod);
      //  Matrix4x4 trsMatrix = Matrix4x4.TRS(transform.position, Quaternion.identity, scale);
      //
      //  Graphics.DrawMesh(hilightMesh, trsMatrix, material, 1);

        
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

    public void UpgradeTower(float damageBuff, float rangeBuff,int upgradeCost)
    {
        PlayerBank bank = FindObjectOfType<PlayerBank>();
        if (bank.CurrentBalance >= upgradeCost && tier != tierMax)
        {
            damage +=damageBuff;
            range += rangeBuff;
            switch (tier)
            {
                case 0:
                    projectiles = upgradeProjectiles[0];
                    speed = upgradeProjectileSpeeds[0];
                    projectiles.GetComponent<VoxelProjectileScript>().bulletDamage = Damage;
                    break;
                case 1:
                    projectiles = upgradeProjectiles[1];
                    speed = upgradeProjectileSpeeds[1];
                    projectiles.GetComponent<VoxelProjectileScript>().bulletDamage = Damage;
                    break;
                case 2:
                    projectiles = upgradeProjectiles[2];
                    speed = upgradeProjectileSpeeds[2];
                    projectiles.GetComponent<VoxelProjectileScript>().bulletDamage = Damage;
                    break;
            }
            bank.Withdrawl(upgradeCost);
            upgradeCost += 15;
            tier++;
            Time.timeScale = 1;
            onHoverUI.SetActive(false);
        }

    }
    public void SellTower()
    {
        PlayerBank bank = FindObjectOfType<PlayerBank>();
        var towerValue = Mathf.FloorToInt(cost * (tier+1) * 0.5f);
        bank.Deposit(towerValue);
        
        Waypoint[] waypoints = FindObjectsOfType<Waypoint>();
        Transform closestPoint = null;
        float maxDistance = Mathf.Infinity;

        foreach (Waypoint waypoint in waypoints)
        {
            targetDistance = Vector3.Distance(transform.position, waypoint.transform.position);
            if (targetDistance < maxDistance)
            {
                closestPoint = waypoint.transform;
                maxDistance = targetDistance;
            }
        }
        var targetPoint = closestPoint;
        closestPoint.gameObject.GetComponent<Waypoint>().isPlaceable = true;
        Time.timeScale = 1;
        onHoverUI.SetActive(false);
        gameObject.SetActive(false);
        
    }
    public void UpgradeButton() 
    {
        switch (tier)
        {
            case 0:
                UpgradeTower(1f, 0.75f, upgradeCost);
                break;
            case 1:
                UpgradeTower(2f, 1.25f, upgradeCost);
                break;
            case 2:
                UpgradeTower(4f, 1.25f, upgradeCost);
                break;

        }

    }
    
}


