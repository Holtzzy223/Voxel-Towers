using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VoxelArsenal;
using TMPro;
using UnityEngine.UI;
public class Tower : MonoBehaviour
{

    [Header("Tower")]
    [SerializeField] string towerName;
    [SerializeField] Transform weapon;
    [SerializeField] bool isCannon = true;
    [SerializeField] bool isSloth = false;
    [SerializeField] bool isBeamWeapon = false;
    [SerializeField] float range = 10f;
    public float Range { get{ return range; } }
    private float maxRange = 30f;
    [SerializeField] float damage = 2f;
    [SerializeField] float damageMod = 1f;
    [SerializeField] float speedDamage = 2f;
    [SerializeField] float speedDamageMod = 0f;
    private float maxDamage = 15f;
    public float Damage { get { return damage; } }
    public float SpeedDamage { get { return speedDamage; } }
    float targetDistance;

    private float rangeIndicatorMod = 1.8f;
    public float RangeIndicatorMod { get { return rangeIndicatorMod; } }
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
    public float speed;
    private float maxSpeed = 1700;
    public GameObject projectile;
    private int tier = 0;
    private int tierMax = 3;
    public GameObject onHoverUI;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI sellText;
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI rangeText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI tierText;
    public Slider attackSlider;
    public Slider rangeSlider;
    public Slider speedSlider;
    [Header("Beams")]
    [Header("Beam Prefabs")]
    public GameObject[] beamLineRendererPrefab;
    public GameObject[] beamStartPrefab;
    public GameObject[] beamEndPrefab;

    private int currentBeam = 0;

    private GameObject beamStart;
    private GameObject beamEnd;
    private GameObject beam;
    private LineRenderer line;

    [Header("Adjustable Variables")]
    public float beamEndOffset = 1f; //How far from the raycast hit point the end effect is positioned
    public float textureScrollSpeed = 8f; //How fast the texture scrolls along the beam
    public float textureLengthScale = 3; //Length of the beam texture
    public List<ParticleCollisionEvent> collisionEvents;
    GridManager gridManager;
   
    void OnEnable()
    {
        nameText.text = towerName;
        tierText.text = "Tier: " + (tier+1);
        attackSlider.maxValue = maxDamage;
        attackSlider.value = damage;
        rangeSlider.maxValue = maxRange;
        rangeSlider.value = range;
        speedSlider.maxValue = maxSpeed;
        speedSlider.value = speed;
        gridManager = FindObjectOfType<GridManager>();
    }
   
    private void Start()
    {

        if (isBeamWeapon)
        {
            CreateBeam();
            beamEnd.GetComponent<VoxelProjectileScript>().bulletDamage = Damage;
            DeactivateBeam();
        }

        collisionEvents = new List<ParticleCollisionEvent>();
        projectiles.GetComponent<VoxelProjectileScript>().bulletDamage = Damage;
        projectiles.GetComponent<VoxelProjectileScript>().speedDamage = SpeedDamage;
        
        if (isSloth == true)
        {
            InvokeRepeating("SlothTower", Random.Range(0.5f,1.5f), 1f);
        }

    }

    void Update()
    {
        
        

        
        
        if (onHoverUI.activeInHierarchy)
        {
            switch (tier)
            {
                case 0:
                    upgradeCost = Mathf.RoundToInt(cost * 1.15f);

                    break;
                case 1:
                    upgradeCost = Mathf.RoundToInt(cost * 1.35f);

                    break;
                case 2:
                    upgradeCost = Mathf.RoundToInt(cost * 1.55f);

                    break;
            }
            if (tier == tierMax)
                costText.text = "Max Tier";
            else
            {
                costText.text = "- $" + upgradeCost.ToString();
            }
        }
       

    }



    private void FixedUpdate()
    {

        FindClosestTarget();
        AimWeapon();
            if (isBeamWeapon)
            {
                if (target == null  || targetDistance > range)
                {
                    DeactivateBeam();

                }
                if (target != null && targetDistance <= range)
                {
                    ActivateBeam();
                }

            }
        switch (tier)
        {
            case 0:
                upgradeCost = Mathf.RoundToInt(cost * 1.15f);

                break;
            case 1:
                upgradeCost = Mathf.RoundToInt(cost * 1.35f);

                break;
            case 2:
                upgradeCost = Mathf.RoundToInt(cost * 1.55f);

                break;
        }
        if (tier == tierMax)
            costText.text = "Max Tier";
        else
        {
            costText.text = "- $" + upgradeCost.ToString();
        }
    }

    private void OnMouseOver()
    {
        sellText.text = "+ $" + Mathf.FloorToInt(cost * (tier + 1) * 0.50f).ToString();
      
        DrawRange();
        var UI = FindObjectOfType<UpgradeUI>();
        if (Input.GetMouseButtonDown(0))
        {
            if (UI == null||UI.gameObject.activeInHierarchy == false)
            {
                onHoverUI.SetActive(true);
                
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
        if (target != null)
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
                if (isCannon)
                {
                    projectile = Instantiate(projectiles, spawnPosition.position, Quaternion.identity) as GameObject; //Spawns the selected projectile
                    projectile.transform.LookAt(target); //Sets the projectiles rotation to look at the target
                    projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * speed); //Set the speed of the projectile by applying force to the rigidbody
                }
                if (isBeamWeapon)
                {
                    ShootBeamInDir(spawnPosition.position,target.position);

                }
            }

        }

        //var emissionComp = projectileParticle.emission;
        //emissionComp.enabled = isActive;
    }
    void DrawRange()
    {
        if (isSloth == false)
        {
            float zDrawOffset = 0.75f;
            Vector3 rangeIndicatorVector = new Vector3(range * rangeIndicatorMod, range * (rangeIndicatorMod * zDrawOffset), range * rangeIndicatorMod);
            Matrix4x4 trsMatrix = Matrix4x4.TRS(transform.position, Quaternion.identity, rangeIndicatorVector);

            Graphics.DrawMesh(mesh, trsMatrix, material, 1);
        }
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
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();
        GridManager gridManager = FindObjectOfType<GridManager>();
        if (bank == null)
        {
            return false;
        }
        if (bank.CurrentBalance >= cost )
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
            damage +=damageBuff*damageMod;
            speedDamage += 0.05f*speedDamageMod;
            range += rangeBuff;
            
            switch (tier)
            {
                case 0:
                    projectiles = upgradeProjectiles[0];
                    speed = upgradeProjectileSpeeds[0];
                    projectiles.GetComponent<VoxelProjectileScript>().bulletDamage = Damage;
                    if (isBeamWeapon)
                    {
                        beamEnd.GetComponent<VoxelProjectileScript>().bulletDamage = Damage;
                    }
                    break;
                case 1:
                    projectiles = upgradeProjectiles[1];
                    speed = upgradeProjectileSpeeds[1];
                    projectiles.GetComponent<VoxelProjectileScript>().bulletDamage = Damage;
                    if (isBeamWeapon)
                    {
                        beamEnd.GetComponent<VoxelProjectileScript>().bulletDamage = Damage;
                    }
                    break;
                case 2:
                    projectiles = upgradeProjectiles[2];
                    speed = upgradeProjectileSpeeds[2];
                    projectiles.GetComponent<VoxelProjectileScript>().bulletDamage = Damage;
                    if (isBeamWeapon)
                    {
                        beamEnd.GetComponent<VoxelProjectileScript>().bulletDamage = Damage;
                    }
                    break;
            }
            projectiles.GetComponent<VoxelProjectileScript>().bulletDamage = Damage;
            projectiles.GetComponent<VoxelProjectileScript>().speedDamage = SpeedDamage;
            bank.Withdrawl(upgradeCost);
            tier++;
            //Time.timeScale = 1;
            UpdateStats();
            //onHoverUI.SetActive(false);
        }

    }
    public void SellTower()
    {
        PlayerBank bank = FindObjectOfType<PlayerBank>();
        var towerValue = Mathf.FloorToInt(cost * (tier+1) * 0.5f);
        bank.Deposit(towerValue);
        
        Tile[] waypoints = FindObjectsOfType<Tile>();
        Transform closestPoint = null;
        float maxDistance = Mathf.Infinity;

        foreach (Tile waypoint in waypoints)
        {
            targetDistance = Vector3.Distance(transform.position, waypoint.transform.position);
            if (targetDistance < maxDistance)
            {
                closestPoint = waypoint.transform;
                maxDistance = targetDistance;
            }
        }
        var targetPoint = closestPoint;
        closestPoint.gameObject.GetComponent<Tile>().isPlaceable = true;
        gridManager.ClearNode(gridManager.GetCoordsFromPos(closestPoint.position));
        FindObjectOfType<Pathfinder>().NotifyRecievers();
        Time.timeScale = 1;
        onHoverUI.SetActive(false);
        gameObject.SetActive(false);

        CancelInvoke();
    }
    public void UpgradeButton() 
    {
        switch (tier)
        {
            case 0:
                
                UpgradeTower(2f, 0.75f, upgradeCost);
                break;
            case 1:
               
                UpgradeTower(3f, 1.25f, upgradeCost);
                break;
            case 2:
                
                UpgradeTower(4f, 1.25f, upgradeCost);
                break;

        }

    }

    private void UpdateStats()
    {

        Debug.Log("Should change stats bar");
        tierText.text = "Tier: " + (tier+1);
        attackSlider.value = damage;
        rangeSlider.value = range*1.15f;
        speedSlider.value = speed*1.05f;

    }
    //------------------------------------------------------------------------------Sloth----------------------------------------------------------------------------------------------------------------------------
    void SlothTower()
    {

        if (target != null)
        {
            projectile = Instantiate(projectiles, spawnPosition.position, Quaternion.identity) as GameObject; //Spawns the selected projectile
            //projectile.transform.localScale = new Vector3(range, range, range);
            projectile.transform.LookAt(target); //Sets the projectiles rotation to look at the target
            projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * speed); //Set the speed of the projectile by applying force to the rigidbody

        }

    }
    //-------------------------------------------------------------------------------Beams-------------------------------------------------------------------------------------------------------------------
    void CreateBeam()
    {
        
        
        beamStart = Instantiate(beamStartPrefab[currentBeam],   transform.position  /*new Vector3(0, 0, 0)*/, Quaternion.identity) as GameObject;
        beamEnd = Instantiate(beamEndPrefab[currentBeam],       transform.position/*new Vector3(0, 0, 0)*/, Quaternion.identity) as GameObject;
        beam = Instantiate(beamLineRendererPrefab[currentBeam], transform.position/*new Vector3(0, 0, 0)*/, Quaternion.identity) as GameObject;
        line = beam.GetComponent<LineRenderer>();
    }

    void ShootBeamInDir(Vector3 start, Vector3 dir)
    {
        
        line.positionCount = 2;
        line.SetPosition(0, start);
        beamStart.transform.position = start;

        Vector3 end = Vector3.zero;
        RaycastHit hit;
        if (Physics.Raycast(start, dir, out hit))
            end = hit.point - (dir.normalized * beamEndOffset);
        else
            end = dir;
;
        beamEnd.transform.position = end;
        line.SetPosition(1, end);

        beamStart.transform.LookAt(beamEnd.transform.position);
        beamEnd.transform.LookAt(beamStart.transform.position);

        float distance = Vector3.Distance(start, end);
        line.sharedMaterial.mainTextureScale = new Vector2(distance / textureLengthScale, 1);
        line.sharedMaterial.mainTextureOffset -= new Vector2(Time.deltaTime * textureScrollSpeed, 0);
    }

    private void DeactivateBeam()
    {
        beamStart.SetActive(false);// Destroy(beamStart);
        beamEnd.SetActive(false);// Destroy(beamEnd);
        beam.SetActive(false);// Destroy(beam);
        line.gameObject.SetActive(false);
    }
    private void ActivateBeam()
    {
        beamStart.SetActive(true);
        beamEnd.SetActive(true);
        beam.SetActive(true);
        line.gameObject.SetActive(true);
    }

}



