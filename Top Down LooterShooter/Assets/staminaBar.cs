using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class staminaBar : MonoBehaviour
{
    public Slider StaminaBar;
    public static staminaBar instance;
    public GameObject player;

    void Start()
    {
        StaminaBar.maxValue = player.GetComponent<Moobment>().maxStamina;
        StaminaBar.value = player.GetComponent<Moobment>().maxStamina;
    }

    private void Update()
    {
        StaminaBar.value = player.GetComponent<Moobment>().currentStamina;
    }
}
