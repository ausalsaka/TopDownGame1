using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private float Xp;
    public static int weaponCounter = 0;
    public static GameObject[] Weapons = new GameObject[2];
    public static float health = 100;
    public GameObject youDied;
    private bool dead = false;
    public Slider healthSlider;
    public static Slider Health;
    public Image PrimaryIcon;
    public Image SecondaryIcon;
    public Text MagSize;
    public Text AmmoCount;



    public void Awake()
    {
        Health = healthSlider;
        Health.maxValue = health;
        Health.value = health;
    }
    public void pickUpXp(float value)
    {
        Xp = Xp + value;
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

    public void pickUpItem(GameObject item)
    {
        if (weaponCounter < 2 && !Weapon.reloading) 
        {
            if (SecondaryIcon != null) { 
                SecondaryIcon.sprite = PrimaryIcon.sprite;
            }
            
            Weapons[weaponCounter] = item;
            item.transform.SetParent(gameObject.transform.GetChild(0));
            item.GetComponent<CapsuleCollider2D>().enabled = false;
            item.transform.localPosition = Vector2.zero;
            item.transform.localRotation = Quaternion.identity;
            unequip();
            Weapons[weaponCounter].GetComponent<Weapon>().isEquipped = true;
            transform.GetComponent<Moobment>().gun = item;

            weaponCounter++;
            Weapons[weaponCounter-1].GetComponent<Weapon>().Renderer.sprite = Weapons[weaponCounter-1].GetComponent<Weapon>().hand;
            PrimaryIcon.transform.position.Set(-30, 0, 0);
            PrimaryIcon.sprite = item.GetComponent<Weapon>().unequipped;
            MagSize.text = item.GetComponent<Weapon>().maxMagSize.ToString();
            AmmoCount.text = item.GetComponent<Weapon>().bulletCount.ToString();
        }
    }


    //Unequip Weapons 
    private void unequip()
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
    private void dropCurrent()
    {
        for (int i = 0; i < weaponCounter; i++)
        {
            if (Weapons[i] != null && Weapons[i].GetComponent<Weapon>().isEquipped == true && Weapon.reloading == false)
            {
                weaponCounter--;
                Weapons[i].transform.parent = null;
                Weapons[i].GetComponent<Weapon>().isEquipped = false;
                Weapons[i].GetComponent<Weapon>().Renderer.sprite = Weapons[i].GetComponent<Weapon>().unequipped;
                StartCoroutine(enableCollider(Weapons[i]));
                Weapons[i] = null;
                PrimaryIcon.sprite = null;
                transform.GetComponent<Moobment>().gun = null;
                MagSize.text = "#";
                AmmoCount.text = "#";
                if (Weapons[1] != null && Weapons[0] == null)
                {
                    Weapons[0] = Weapons[1];
                    Weapons[1] = null;
                    transform.GetComponent<Moobment>().gun = Weapons[0];
                    PrimaryIcon.sprite = Weapons[0].GetComponent<Weapon>().unequipped;
                    MagSize.text = Weapons[0].GetComponent<Weapon>().maxMagSize.ToString();
                    AmmoCount.text = Weapons[0].GetComponent<Weapon>().bulletCount.ToString();
                    SecondaryIcon.sprite = PrimaryIcon.sprite;
                    SecondaryIcon.sprite = null;
                }
            }
        }

    }



    IEnumerator enableCollider(GameObject gun)
    {
        yield return new WaitForSeconds(1.5f);
        gun.GetComponent<CapsuleCollider2D>().enabled = true;
    }



    private void Update()
    {
        if(health <= 0 && dead == false)
        {
            Time.timeScale = 0;
            Instantiate(youDied, transform.position, Quaternion.identity);
            dead = true;
        }
        //Weapon Dropping
        if (Input.GetKeyDown("z"))
        {
            dropCurrent();
        }

        // Weapons Array
        if (Input.GetKeyDown("x") && Weapon.reloading == false)
        {
            unequip();
        }
        else if (Input.GetKeyDown("1") && weaponCounter >= 1 && Weapons[0] != null && Weapon.reloading == false)
        {
            unequip();
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
            unequip();
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
