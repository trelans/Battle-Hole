using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class destroyCubes : MonoBehaviour
{

    private float value;
  [SerializeField] private GameObject ammos;
 [SerializeField] private int ammoMax;
  private int currentAmmo;
  [SerializeField] private TextMeshProUGUI tmp;
  private bool isFull;
  private holeManager hole;
  private Navigator navigator;
    private void Awake()
    {
        hole = FindObjectOfType<holeManager>();
        navigator = FindObjectOfType<Navigator>()
            ;
    }

    private void Start()
    {
        value = 100f / GameObject.FindGameObjectsWithTag("block").Length;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("block"))
        {
            other.gameObject.SetActive(false);
            
        }
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            other.gameObject.GetComponent<Enemy>().SetIsMoving(false);
            other.gameObject.tag = "EnemyAmmo";
            

        }
        if (other.CompareTag("Ammo"))
        {
            other.gameObject.SetActive(false);
            other.gameObject.transform.position = ammos.transform.position;
            other.gameObject.transform.SetParent(ammos.transform);
          
            UpdateAmmoCount();

        }


    }

    private void UpdateAmmoCount()
    {
       
        currentAmmo++;
        tmp.SetText(currentAmmo + "/" + ammoMax);
        if (currentAmmo == ammoMax)
        {
            hole.SetHoleSize(0);
            isFull = true;
            navigator.SetIsFull(isFull);
        }
       
    }
    
    public void ResetAmmoCount()
    {
       
        currentAmmo = 0;
        tmp.SetText(currentAmmo + "/" + ammoMax);
        hole.SetHoleSize(1.5f);
        isFull = false;
        navigator.SetIsFull(false);
      
       
    }
}
