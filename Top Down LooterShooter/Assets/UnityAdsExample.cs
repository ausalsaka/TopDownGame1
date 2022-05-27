using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class UnityAdsExample : MonoBehaviour
{
    public string androidId;
    public string iOSId;
    private string gameId;
    private string rewardedId;
    //references for reward
    public GameObject player;
    public GameObject deathMenu;
    [HideInInspector]public int adsShown = 0;
    public Text text;



    private void Awake()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            gameId = iOSId;
            rewardedId = "Rewarded_iOS";

        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            gameId = androidId;
            rewardedId = "Rewarded_Android";
                
        }
        else
        {
            Debug.Log("could not initialize ads" + "Platform = " + Application.platform);
            gameId = androidId;
            rewardedId = "Rewarded_Android";
        }
    }


    void Start()
    {
        Advertisement.Initialize(gameId);
    }

    [System.Obsolete]
    public void ShowRewardedAd()
    {
        if (Advertisement.IsReady(rewardedId) && adsShown<1)
        {
            adsShown++;
            text.text = adsShown.ToString();
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show(rewardedId, options);
        }
    }

    public void ShowAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
        }


    }

    void keepPlaying()
    {
        Time.timeScale = 1;
        deathMenu.SetActive(false);
        player.GetComponent<Player>().dead = false;
        player.GetComponent<Player>().health = 100;
        player.GetComponent<Player>().Health.value = player.GetComponent<Player>().health;
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("enemy"))
        {
            Destroy(enemy);
        }
        foreach (GameObject spnr in GameObject.FindGameObjectsWithTag("spawner"))
        {
            spnr.GetComponent<spawner>().stopSpawning = true;
            spnr.GetComponent<spawner>().newWaveStarted = false;
            spawner.currentEnemies = 0;
            spnr.GetComponent<spawner>().currentlySpawning = false;
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                //reward

                keepPlaying();

                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
    }
}