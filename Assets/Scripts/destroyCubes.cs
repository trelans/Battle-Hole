using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class destroyCubes : MonoBehaviour
{

    private float value;
  [SerializeField] private GameObject ammos;
  [SerializeField] private GameObject bombs;

  [SerializeField] private int bombIncrement = 1;
  [SerializeField] private int ammoIncrement = 1;

  private int ammosCollected = 0;
  private int bombsCollected = 0;
 [SerializeField] private int ammoMax;
  private int currentAmmo;
  [SerializeField] private TextMeshProUGUI tmp;
  [SerializeField] private GameObject ammoIcon;
  [SerializeField] private GameObject bombIcon;


  private bool isFull;
  private holeManager hole;
  private Navigator navigator;
  [SerializeField] private int multipleAmmo;
    private void Awake()
    {
        hole = FindObjectOfType<holeManager>();
        navigator = FindObjectOfType<Navigator>()
            ;
    }

    private void Start()
    {
        value = 100f / GameObject.FindGameObjectsWithTag("block").Length;
        SetUI();
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
            for (int i = 0; i < multipleAmmo; i++)
            {
                Instantiate(other.gameObject, other.gameObject.transform).transform.SetParent(ammos.transform);
            }
            
            ammosCollected++;
            UpdateAmmoCount(ammoIncrement);

        }
        if (other.CompareTag("Bomb"))
        {
            other.gameObject.SetActive(false);
            other.gameObject.transform.position = bombs.transform.position;
            other.gameObject.transform.SetParent(bombs.transform);
            for (int i = 0; i < multipleAmmo; i++)
            {
                Instantiate(other.gameObject, other.gameObject.transform).transform.SetParent(bombs.transform);
            }
            
            bombsCollected++;
            UpdateAmmoCount(bombIncrement);

        }

    }

    private void UpdateAmmoCount(int increment)
    {
       
        currentAmmo = currentAmmo + increment + multipleAmmo;
        if (currentAmmo >= ammoMax)
        {
            hole.SetHoleSize(0);
            isFull = true;
            navigator.SetIsFull(isFull);
        }
        
        SetUI();
       
    }
    
    public void ResetAmmoCount(bool isBomb)
    {
        if (isBomb)
        {
            currentAmmo = currentAmmo - bombsCollected * (multipleAmmo + bombIncrement);
            bombsCollected = 0;
        }
        else
        {
            currentAmmo = currentAmmo - ammosCollected * (multipleAmmo + ammoIncrement);
            ammosCollected = 0;
        }
       
        tmp.SetText(currentAmmo + "/" + ammoMax);
        hole.SetHoleSize(1.5f);
        isFull = false;
        navigator.SetIsFull(false);

        SetUI();
      
       
    }

    public void SetUI()
    {
        tmp.SetText(currentAmmo + "/" + ammoMax);
        bombIcon.SetActive(bombsCollected > 0);
        bombIcon.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(bombsCollected.ToString());

        ammoIcon.SetActive(ammosCollected > 0);
        ammoIcon.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(ammosCollected.ToString());
    }

    public int GetCurrentAmmo(bool isBomb)
    {
        int count = ammosCollected;
        if (isBomb)
            count = bombsCollected;
        return count;
    }
}
