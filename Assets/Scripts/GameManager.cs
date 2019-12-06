using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [SerializeField] private GameObject avocado;
    [SerializeField] private List<Transform> holes;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text timerText;

    private List<Transform> occupiedHoles = new List<Transform>();
    private int score = 0;
    private int secs = 10;
    private float tempInt = 0;

    public List<Transform> Holes
    {
        get { return holes; }
    }

    public List<Transform> OccupiedHoles
    {
        get { return occupiedHoles; }
        set { occupiedHoles = value; }
    }

    public int Score
    {
        get { return score; }
        set { score = value; }
    }

    public Text ScoreText
    {
        get { return scoreText; }
        set { scoreText = value; }
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
        int temp = PlayerPrefs.GetInt("Num");

        for (int i = 0; i < PlayerPrefs.GetInt("Num"); i++)
        {
            GameObject gO = Instantiate(avocado) as GameObject;
        }

        scoreText.text = "Score: " + score + "pts";

        occupiedHoles.Clear();

        tempInt = (int)Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if ((int)Time.time - (int)tempInt == 1)
        {
            if (secs <= 11)
            {
                timerText.color = Color.red;
                timerText.GetComponent<Text>().enabled = true;
                StartCoroutine(Pulse());
            }

            --secs;
            tempInt = Time.time;
        }

        if (secs == 120)
            timerText.text = "Time Left: 2:00" + " mins";
        else if (secs > 60)
            timerText.text = "Time Left: 1:" + (secs - 60) + " mins";
        else
            timerText.text = "Time Left: 0:" + secs + " mins";

        if (secs <= 0)
            GameOver();

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.GetComponent<Avocado>() != null)
                {
                    Transform temp = hit.collider.GetComponent<Avocado>().Temp;

                    if (!hit.collider.GetComponent<Avocado>().Destroyed)
                        StartCoroutine(hit.collider.GetComponent<Avocado>().Hit());
                }
            }
        }

        if (occupiedHoles.Count < PlayerPrefs.GetInt("Num"))
        {
            GameObject gO = Instantiate(avocado) as GameObject;
            Debug.Log("TEST");
        }
    }

    private IEnumerator Pulse()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        timerText.GetComponent<Text>().enabled = false;

        yield break;
    }

    public IEnumerator RemoveHole(Transform t)
    {
        Debug.Log(occupiedHoles.Contains(t));
        yield return new WaitForSecondsRealtime(0.5f);
        occupiedHoles.Remove(t);
        Debug.Log(occupiedHoles.Contains(t));
    }

    private void GameOver()
    {
        PlayerPrefs.SetInt("Score", score);
        SceneManager.LoadScene(2);
    }

}
