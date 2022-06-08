using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MilkShake;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Weapon : MonoBehaviour
{
    [HideInInspector]public GameObject Player;
    [HideInInspector]public bool isEquipped = false;
    [SerializeField]private Joystick aimstick;
    public Joystick joystick;
    public AudioSource Sounds;
    public AudioClip reloadClip;
    public AudioClip shootClip;
    private float chancer;
    private float chancer2;
    private float posneg;
    private float posneg2;
    [HideInInspector]public GameObject bulletExit;
    public GameObject bulletExitL;
    public GameObject bulletExitR;
    public GameObject bulletPrefab;                 //Retrieves Bullet Sprite
    public Sprite hand;
    public Sprite unequipped;
    public float fireRate = 2f;                     //Sets firerate
    public int maxMagSize = 10;
    public float reloadTime = 2;
    [HideInInspector] public bool isShooting = false;
    
    [HideInInspector]public int bulletCount;
    public static bool reloading = false;
    public GameObject Magazine;
    public SpriteRenderer Renderer;
    public float shakeScale;
    public GameObject flashR;
    public GameObject flashL;
    //public Image icon;
    //public Text MagSize;
    public Text AmmoCount;
    //touch screen tools
    public Image reloadButton;
        


    private void Awake()
    {
        flashR.SetActive(false);
        flashL.SetActive(false);
    }

    private void Start()
    {
        //GameObject.Find("Inventory").GetComponent<Button>().onClick.AddListener(blockTouch);
        reloadButton.GetComponent<Button>().onClick.AddListener(StartReloading);
        bulletCount = maxMagSize;
        Player = GameObject.Find("Player");
    }
    
    void Update()
    {
        fireInputDetection();
        //checkForReload();
    }



    //void cancelinvoke(string invoke)
    //{
    //    CancelInvoke(invoke);
    //}

    //void blockTouch()
    //{
    //    touchingInventory = true;
    //}
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0017:Simplify object initialization", Justification = "<Pending>")]
    void fireInputDetection()
    {

        if (Input.touchCount>0 && isEquipped && Player.GetComponent<Player>().dead == false && ShootButton.pushingShoot)
        {
            int i = 0;
            while ( i < Input.touchCount )
            {
            Touch t = Input.GetTouch(i);
                if (t.phase == TouchPhase.Began && bulletCount != 0 
                    && !isShooting ) 
                {
                    InvokeRepeating("shooting", 0f, 60/fireRate);
                    isShooting = true;
                }
                else if (bulletCount == 0)
                {
                    CancelInvoke("shooting");
                    isShooting = false;
                    StartCoroutine(reload());
                }

                i++;
            }
        }
        else
        {
            CancelInvoke("shooting");
            isShooting = false;
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


    void StartReloading()
    {
        if (isEquipped) StartCoroutine(reload());
    }


    void shooting()
    {

        if (bulletCount != 0 && reloading != true && isEquipped)
            {
                Instantiate(bulletPrefab, bulletExit.transform.position, bulletExit.transform.rotation);
                StartCoroutine(muzzleFlash());
                
                Sounds.PlayOneShot(shootClip);
                CinemachineShake.Instance.ShakeCamera(shakeScale, .2f);
                bulletCount--;
                AmmoCount.text = bulletCount.ToString();
            }
            else if (bulletCount == 0 && isEquipped)
            {
                CancelInvoke("shooting");
                isShooting = false;
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
        if (reloading == false)
        {
            reloading = true;
            Sounds.PlayOneShot(reloadClip);
            StartCoroutine(dropMag());
            yield return new WaitForSeconds(reloadTime);
            //if(Player.GetComponent<Player>())
            bulletCount = maxMagSize;
            reloading = false;
            AmmoCount.text = bulletCount.ToString();
        }

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

