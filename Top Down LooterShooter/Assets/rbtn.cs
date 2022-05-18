using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class rbtn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(RestartLevel);
    }

    void RestartLevel()
    {
        SceneManager.LoadScene("Scenes/"+SceneManager.GetActiveScene().name);
    }
}
