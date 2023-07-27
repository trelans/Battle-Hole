using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour
{
    private int shooterCount;
    private GameObject shooterParent;
    [SerializeField] private ObjectPool bulletPool;
    [SerializeField] private float sightRange;
    [SerializeField] private LayerMask shootableObject;
    private int movingCount;
    private List<GameObject> movingBullets;
    [SerializeField] private GameObject particleParent;

    private bool update = false;
    private int tankLevel;
    private float shootingRange;
    private float secsBetweenShots;
    private float bulletSpeed;
    private bool inRange;
    private bool inSlowRange;
    
    private float latestLoad;

    // private ShooterRecoil _shooterRecoil;
    private bool isRecoiling;


    private float shootingSpeedRate; 
    // private SoundManager _soundManager;

    private bool isGameOver = false;

    private RaycastHit m_Hit;
    private bool enemyInfront;
    private bool isSlowed;
   private Collider _collider;
//    private Player _player;
   [SerializeField] private float slowRange;
    private void Awake() {
        latestLoad = 0;
        // _shooterRecoil = FindObjectOfType<ShooterRecoil>();
        // _soundManager = FindObjectOfType<SoundManager>();
        _collider = GetComponent<Collider>();
        // _player = FindObjectOfType<Player>();
    }
    // Start is called before the first frame update
    void Start()
    {
        isRecoiling = false;
        inRange = true;
        movingBullets = new List<GameObject>();
        UpdateStats();
        shootingSpeedRate = 1;
    
    }

    // Update is called once per frame
    void Update()
    {
        if (latestLoad > secsBetweenShots)
        {
            LoadBullets();
            latestLoad = 0;
        }
        ShootBullets();
        latestLoad = latestLoad + Time.deltaTime;
        // if (inRange)
        // {
            // PlayParticles();
        // }
        // else
        // {
            // StopParticles();
        // }


    }

    void FixedUpdate() 
    {
      /*   if (Physics.BoxCast( _collider.bounds.center, transform.localScale + (Vector3.left * 10) +  (Vector3.right * 10) + (Vector3.forward *10 ) , 
                 transform.forward, out m_Hit, transform.rotation , slowRange))
         {
             if (m_Hit.collider.gameObject.layer == 8 )
             {
                 enemyInfront = true;
          
             }
             else
             {
                 if (enemyInfront )
           
                 {
                     enemyInfront = false;
                     Debug.Log("LLAA : " + m_Hit.collider.gameObject.layer);
                 }
             }
           
         }
         
        }
        */
        if (enemyInfront && !isSlowed)
        {
            // _player.SetSpeed(1);
            Debug.Log("Slowed");
            isSlowed = true;
        }

        if (isSlowed && !enemyInfront)
        {
            // _player.SetSpeed(10);
            Debug.Log("Unslowed");
            isSlowed = false;
        }
        if (inRange && !isRecoiling)
        {
            // _shooterRecoil.SetRecoil(true);
            isRecoiling = true;
            // _soundManager.PlayFireSoundEffect(shootingSpeedRate);
        }
        if (!inRange && isRecoiling)
        {
            // _shooterRecoil.SetRecoil(false);
            isRecoiling = false;
            // _soundManager.StopFire();
          
        }

        
    }

    IEnumerator WaitALittle()
    {

        yield return new WaitForSeconds(0.3f);
        if (!enemyInfront)
        {
            
        }
    }

    public void SetGameOver()
    {
        isGameOver = true;
    }

    private void SetShooterParent()
    {
     
            shooterParent = transform.GetChild(tankLevel - 1).GetChild(shooterCount - 1)
                .GetChild(0).GetChild(0).gameObject;
        
       
    }

    private void LoadBullets()
    {
        for (int i = 0; i < shooterCount; i++)
        {
            GameObject bullet = bulletPool.GetPooledObject();
            GameObject shooter = null;
            if (i < shooterParent.transform.childCount)
            {
                shooter = shooterParent.transform.GetChild(i).gameObject;
            }

            if (bullet != null && shooter != null)
            {
                bullet.transform.position = shooter.transform.position;
                bullet.transform.rotation = shooter.transform.rotation; 
                movingBullets.Add(bullet);
                movingCount = movingCount + 1;
                bullet.SetActive(true);
            }
        }
    }

    private void ShootBullets()
    {
        for (int i = 0; i < movingCount; i++)
        {
            GameObject curBullet = movingBullets[i];
            Vector3 curBulletPos = curBullet.transform.position;
            Vector3 forwardVec = curBullet.transform.forward * Time.deltaTime * bulletSpeed;
            curBullet.transform.position = curBulletPos + forwardVec;
        
            if ((curBullet.transform.position.z  - shooterParent.transform.position.z) > shootingRange)
            {
                movingBullets.Remove(curBullet);
                movingCount = movingCount - 1;
                curBullet.SetActive(false);
            }
        }
    }

    public void ChangeAmmo(GameObject newAmmo)
    {
        bulletPool.ChangeObject(newAmmo);
    }

    public void UpdateStats()
    {
        // TankController controller = gameObject.GetComponent<TankController>();
        // tankLevel = controller.GetLevel();
        // secsBetweenShots = 1 - controller.GetShootingSpeed() / controller.GetMaxShootingSpeed();
        // shootingRange = controller.GetShootingRange();
        // bulletSpeed = controller.GetBulletSpeed();
        // shooterCount = controller.GetShooterAmount();
        // SetShooterParent();
        // SetParticles();

        secsBetweenShots = 0.25f;
        bulletSpeed = 10f;
        shootingRange = 100f;
        shooterCount = 1;
        shooterParent = this.gameObject;
  
    
    }

    public void SetShootingSpeedForSound(float rate)
    {
        shootingSpeedRate = rate;
        // _soundManager.UpdateFireSoundRate(shootingSpeedRate);
    }
    
    private void SetParticles()
    {
        for (int i = 0; i < shooterCount; i++)
        {
            GameObject particle = particleParent.transform.GetChild(i).gameObject;
            ParticleSystem ps = particle.GetComponent<ParticleSystem>();
            var rend = ps.GetComponent<ParticleSystemRenderer>();
            if (movingBullets.Count > 0)
                rend.material = movingBullets[0].GetComponent<MeshRenderer>().material;
            particle.SetActive(true);
            ps.Stop();
            if (!ps.isPlaying)
            {
                var main = ps.main;
                main.duration = secsBetweenShots;
            }
            ps.Play();
        }

    }
    
    
    private void PlayParticles()
    {
        for (int i = 0; i < shooterCount; i++)
        {
            GameObject particle = particleParent.transform.GetChild(i).gameObject;
            particle.SetActive(true);
        }
    }

    private void StopParticles()
    {
        for (int i = 0; i < shooterCount; i++)
        {
            GameObject particle = particleParent.transform.GetChild(i).gameObject;
            particle.SetActive(false);
        }
    }
}
