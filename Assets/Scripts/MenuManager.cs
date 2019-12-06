using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance = null;

    [SerializeField] private Dropdown numOfAvocados;

    public Dropdown NumOfAvocados
    {
        get { return numOfAvocados; }
    }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Num", MenuManager.Instance.NumOfAvocados.value + 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
