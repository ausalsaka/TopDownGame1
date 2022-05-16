using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MilkShake;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [HideInInspector]
    public GameObject Player;
    [HideInInspector]
    public bool isEquipped = false;
    [SerializeField]
    private Joystick aimstick;
    public Joystick joystick;
    public AudioSource Sounds;
    public AudioClip reloadClip;
    public AudioClip shootClip;
    private float chancer;
    private float chancer2;
    private float posneg;
    private float posneg2;
    [HideInInspector]
    public GameObject bulletExit;
    public GameObject bulletExitL;
    public GameObject bulletExitR;
    public GameObject bulletPrefab;                 //Retrieves Bullet Sprite
    public Sprite hand;
    public Sprite unequipped;
    public float fireRate = 2f;                     //Sets firerate
    public int maxMagSize = 10;
    public float reloadTime = 2;
    [HideInInspector]
    public int bulletCount;
    private float lastFired;
    public static bool reloading = false;
    public GameObject Magazine;
    public SpriteRenderer Renderer;
    public float shakeScale;
    public GameObject flashR;
    public GameObject flashL;
    //public Image icon;
    //public Text MagSize;
    public Text AmmoCount;



    private void Awake()
    {
        flashR.SetActive(false);
        flashL.SetActive(false);
    }

    private void Start()
    {
        bulletCount = maxMagSize;
        Player = GameObject.Find("Player");
    }

    void Update()
    {
        //weaponOrientationCheck();
        fireInputDetection();
        checkForReload();
    }
    
    void weaponOrientationCheck()
    {
        //Vector3 mousePosition = aimstick.Direction;
        //mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //if (aimstick.Horizontal > 0 && isEquipped)
        //{
        //    Renderer.flipX = false;
        //    bulletExit = bulletExitR;
        //    
        //}else if (aimstick.Horizontal < 0 && isEquipped)
        //{
        //    Renderer.flipX = true;
        //    bulletExit = bulletExitL;
        //}
        if (aimstick.Horizontal > 0 && isEquipped)
        {
            Renderer.flipX = false;
            bulletExit = bulletExitR;

        }
        else if (joystick.Direction != Vector2.zero && joystick.Horizontal > 0 && isEquipped)
        {
            Renderer.flipX = false;
            bulletExit = bulletExitR;
        }
        else if (isEquipped)
        {
            Renderer.flipX = true;
            bulletExit = bulletExitL;

        }

    }
    void fireInputDetection()
    {


        if (Input.touchCount>0 && isEquipped && reloading != true )
        {
            int i = 0;
            while ( i < Input.touchCount)
            {
                var touch = Input.GetTouch(i);
                

                if (touch.phase == TouchPhase.Began && touch.position.x < 400 && touch.position.y < 300)
                {
                    touch.fingerId = 100;
                }

                if ((touch.fingerId != 100) && (Time.time - lastFired > 1 / fireRate) && (bulletCount != 0))
                {
                    lastFired = Time.time;
                    Instantiate(bulletPrefab, bulletExit.transform.position, bulletExit.transform.rotation);
                    StartCoroutine(muzzleFlash());
                    Sounds.PlayOneShot(shootClip);
                    CinemachineShake.Instance.ShakeCamera(shakeScale, .2f);
                    bulletCount--;
                    AmmoCount.text = bulletCount.ToString();
                }
                else if (bulletCount == 0)
                {
                    StartCoroutine(reload());
                }
                   
               

                i++;
            }


        }
    }

    IEnumerator muzzleFlash()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        yield return new WaitForSeconds(0.05f);
        if (Player.transform.position.x < mousePosition.x && isEquipped)
        {
            flashR.SetActive(true);
        } else{
            flashL.SetActive(true);
        }
        yield return new WaitForSeconds(0.05f);
        flashR.SetActive(false);
        flashL.SetActive(false);
    }

    void checkForReload()
    {
        
        if (Input.GetKeyDown("r") && isEquipped && reloading == false && bulletCount != maxMagSize)
        {
            StartCoroutine(reload());
        }
    }




    IEnumerator dropMag()
    {
        yield return new WaitForSeconds(.5f);
        chancer = Random.Range(1f, 2f);
        chancer2 = Random.Range(1f, 2f);
        if (chancer > 1.5)
        {
            posneg = -1;
        } else 
        {
            posneg = 1;
        }
        if (chancer2 > 1.5)
        {
            posneg2 = -1;
        }else
        {
            posneg2 = 1;
        }

        Vector2 rand = new Vector2(Random.Range(Player.transform.position.x + 0.7f *posneg, Player.transform.position.x + 1.4f * posneg), Random.Range(Player.transform.position.y + .7f *posneg2, Player.transform.position.y + 1.4f * posneg2));
        Instantiate(Magazine, rand, Quaternion.Euler(new Vector3(0, 0, Random.Range(0f, 360f))));
    }

    IEnumerator reload()
    {
        reloading = true;
        Debug.Log("reloading");
        Sounds.PlayOneShot(reloadClip);
        StartCoroutine(dropMag());
        yield return new WaitForSeconds(reloadTime);
        bulletCount = maxMagSize;
        Debug.Log(bulletCount + "/" + maxMagSize);
        reloading = false;
        AmmoCount.text = bulletCount.ToString();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "Player")
        {
            Player player = col.GetComponent<Player>();
            if (player != null)
            {
                player.pickUpItem(gameObject);
                //icon.sprite = unequipped;
                //AmmoCount.text = bulletCount.ToString();
                //MagSize.text = maxMagSize.ToString();
            }
        }
    }
}
