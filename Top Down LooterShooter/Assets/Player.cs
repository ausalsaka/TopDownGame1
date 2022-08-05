using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private float Xp;
    public int weaponCounter = 0;
    public GameObject[] Weapons = new GameObject[2];
    public int activeWep = 69;
    public float health = 100;
    public GameObject youDied;
    public GameObject deathMenu;
    [HideInInspector]
    public bool dead = false;
    //public Slider healthSlider;
    public Slider Health;
    public Image PrimaryIcon;
    public Image SecondaryIcon;
    public Text MagSize;
    public Text AmmoCount;
    //touch controls
    public GameObject shootArea;

    [HideInInspector]public bool[] buffs = new bool[0]; //0 -> ammo ; 1 -> ?
    public GameObject timerController;


    public void Awake()
    {
        Health.maxValue = health;
        Health.value = health;
        SecondaryIcon.GetComponent<Button>().onClick.AddListener(SecondaryClicked);
    }

    public void PickUpBuff(int buff, float duration)
    {
        buffs[buff] = true;
        StartCoroutine(DisableBuff(buff, duration));
        timerController.GetComponent<BuffTimerControllerScript>().StartBuffTimer(buff, duration);
    }
    IEnumerator DisableBuff(int buff, float duration)
    {
        yield return new WaitForSeconds(duration);
        buffs[buff] = false;
    }

    public void pickUpXp(float value)
    {
        Xp += value;
        if (gameObject.GetComponent<Moobment>().currentStamina < gameObject.GetComponent<Moobment>().maxStamina)
        {
            gameObject.GetComponent<Moobment>().currentStamina += value;     //collected experience regenerates stamina
        }
    }
    public void pickUpHealth(float value)
    {
        health = health + value;
        Health.value = health;
    }
    public void SecondaryClicked()
    {
        //Weapons[activeWep].AddComponent<Weapon>().CancelInvoke("shooting");

        if (weaponCounter == 2 && SecondaryIcon != null && Weapon.reloading == false)
        {
                //SecondaryIcon.sprite = PrimaryIcon.sprite;
            if (activeWep == 0)
            {
                Unequip();
                Weapons[1].SetActive(true);
                Weapons[1].GetComponent<Weapon>().isEquipped = true;
                PrimaryIcon.transform.position.Set(-30, 0, 0);
                PrimaryIcon.sprite = Weapons[1].GetComponent<Weapon>().unequipped;
                SecondaryIcon.sprite = Weapons[0].GetComponent<Weapon>().unequipped;
                MagSize.text = Weapons[1].GetComponent<Weapon>().maxMagSize.ToString();
                AmmoCount.text = Weapons[1].GetComponent<Weapon>().bulletCount.ToString();
                activeWep = 1;
            } else if (activeWep == 1)
            {
                Unequip();
                Weapons[0].SetActive(true);
                Weapons[0].GetComponent<Weapon>().isEquipped = true;
                PrimaryIcon.transform.position.Set(-30, 0, 0);
                PrimaryIcon.sprite = Weapons[0].GetComponent<Weapon>().unequipped;
                if (Weapons[1] != null) { SecondaryIcon.sprite = Weapons[1].GetComponent<Weapon>().unequipped; } else { SecondaryIcon.sprite = null; }
                MagSize.text = Weapons[0].GetComponent<Weapon>().maxMagSize.ToString();
                AmmoCount.text = Weapons[0].GetComponent<Weapon>().bulletCount.ToString();
                activeWep = 0;
            }

        }
    }

    public void pickUpItem(GameObject item)
    {
        if (weaponCounter < 2 && !Weapon.reloading) 
        {
            if (SecondaryIcon != null) { 
                SecondaryIcon.sprite = PrimaryIcon.sprite;
            }
            
            Weapons[weaponCounter] = item;
            activeWep = weaponCounter;
            item.transform.SetParent(gameObject.transform.GetChild(0));
            item.GetComponent<CapsuleCollider2D>().enabled = false;
            item.transform.localPosition = Vector2.zero;
            item.transform.localRotation = Quaternion.identity;
            Unequip();
            Weapons[weaponCounter].GetComponent<Weapon>().isEquipped = true;
            transform.GetComponent<Moobment>().gun = item;
            shootArea.GetComponentInChildren<ShootButton>().gun = item;

            weaponCounter++;
            Weapons[weaponCounter-1].GetComponent<Weapon>().Renderer.sprite = Weapons[weaponCounter-1].GetComponent<Weapon>().hand;
            PrimaryIcon.transform.position.Set(-30, 0, 0);
            PrimaryIcon.sprite = item.GetComponent<Weapon>().unequipped;
            MagSize.text = item.GetComponent<Weapon>().maxMagSize.ToString();
            AmmoCount.text = item.GetComponent<Weapon>().bulletCount.ToString();
        }

    }


    //Unequip Weapons 
    private void Unequip()
    {
        for (int i = 0; i < weaponCounter; i++)
        {
            if (Weapons[i] != null)
            {
                Weapons[i].SetActive(false);
                Weapons[i].GetComponent<Weapon>().isEquipped = false;
                MagSize.text = "#";
                AmmoCount.text = "#";
                PrimaryIcon.transform.position.Set(-250, 0, 0);
            }
        }
    }

    //Drop Current Weapon
    public void DropCurrent()
    {
        for (int i = 0; i < weaponCounter; i++)
        {
            if (Weapons[i] != null && Weapons[i].GetComponent<Weapon>().isEquipped == true && Weapon.reloading == false)
            {
                weaponCounter--;
                Weapons[i].transform.parent = null;
                Weapons[i].GetComponent<Weapon>().isEquipped = false;
                Weapons[i].GetComponent<Weapon>().Renderer.sprite = Weapons[i].GetComponent<Weapon>().unequipped;
                StartCoroutine(EnableCollider(Weapons[i]));
                Weapons[i] = null;
                PrimaryIcon.sprite = null;
                transform.GetComponent<Moobment>().gun = null;
                shootArea.GetComponentInChildren<ShootButton>().gun = null;
                MagSize.text = "#";
                AmmoCount.text = "#";
                if (Weapons[1] != null && Weapons[0] == null)
                {
                    Weapons[0] = Weapons[1];
                    Weapons[1] = null;
                    transform.GetComponent<Moobment>().gun = Weapons[0];
                    shootArea.GetComponentInChildren<ShootButton>().gun = Weapons[0];
                    PrimaryIcon.sprite = Weapons[0].GetComponent<Weapon>().unequipped;
                    MagSize.text = Weapons[0].GetComponent<Weapon>().maxMagSize.ToString();
                    AmmoCount.text = Weapons[0].GetComponent<Weapon>().bulletCount.ToString();
                    SecondaryIcon.sprite = PrimaryIcon.sprite;
                    SecondaryIcon.sprite = null;
                }
            }
        }

    }

    public void Revive()
    {
        Debug.Log("revive");
    }

    IEnumerator EnableCollider(GameObject gun)
    {
        yield return new WaitForSeconds(1.5f);
        gun.GetComponent<CapsuleCollider2D>().enabled = true;
    }



    private void Update()
    {
        if(health <= 0 && dead == false)
        {
            Time.timeScale = 0;
            //Instantiate(youDied, transform.position, Quaternion.identity);
            deathMenu.SetActive(true);
            dead = true;
            shootArea.SetActive(false);
            
        }
        //Weapon Dropping
        if (Input.GetKeyDown("z"))
        {
            DropCurrent();
        }

        // Weapons Array
        if (Input.GetKeyDown("x") && Weapon.reloading == false)
        {
            Unequip();
        }
        else if (Input.GetKeyDown("1") && weaponCounter >= 1 && Weapons[0] != null && Weapon.reloading == false)
        {
            Unequip();
            Weapons[0].SetActive(true);
            Weapons[0].GetComponent<Weapon>().isEquipped = true;
            PrimaryIcon.transform.position.Set(-30, 0, 0);
            PrimaryIcon.sprite = Weapons[0].GetComponent<Weapon>().unequipped;
            if (Weapons[1] != null){SecondaryIcon.sprite = Weapons[1].GetComponent<Weapon>().unequipped;}else { SecondaryIcon.sprite = null; }
            MagSize.text = Weapons[0].GetComponent<Weapon>().maxMagSize.ToString();
            AmmoCount.text = Weapons[0].GetComponent<Weapon>().bulletCount.ToString();
        }
        else if (Input.GetKeyDown("2") && weaponCounter == 2 && Weapons[1] != null && Weapon.reloading == false)
        {
            Unequip();
            Weapons[1].SetActive(true);
            Weapons[1].GetComponent<Weapon>().isEquipped = true;
            PrimaryIcon.transform.position.Set(-30, 0, 0);
            PrimaryIcon.sprite = Weapons[1].GetComponent<Weapon>().unequipped;
            SecondaryIcon.sprite = Weapons[0].GetComponent<Weapon>().unequipped;
            MagSize.text = Weapons[1].GetComponent<Weapon>().maxMagSize.ToString();
            AmmoCount.text = Weapons[1].GetComponent<Weapon>().bulletCount.ToString();
        }
    }

}
