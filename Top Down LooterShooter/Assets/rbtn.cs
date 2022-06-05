using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class rbtn : MonoBehaviour
{

    public GameObject player;
    public GameObject adButton;
    public Text adsShown;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(RestartLevel);
    }

    void RestartLevel()
    {
        Time.timeScale = 1;
        int i = 0;
        while (i < player.GetComponent<Player>().weaponCounter)
        {
            player.GetComponent<Player>().Weapons[i] = null;
            player.GetComponent<Player>().weaponCounter--;
            player.GetComponent<Player>().activeWep--;
            i++;
        }
        adsShown.text = "0";
        spawner.mode = 0;
        spawner.currentEnemies = 0;
        adButton.AddComponent<UnityAdsExample>().adsShown = 0;
        ShootButton.pushingShoot = false;
        Weapon.reloading = false;
        SceneManager.LoadScene("Scenes/"+SceneManager.GetActiveScene().name);
        gameObject.SetActive(false);
    }
}
