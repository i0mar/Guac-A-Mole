using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Avocado : MonoBehaviour
{
    [SerializeField] private Sprite guacmole;

    private Transform temp;
    private bool destroyed = false;

    public bool Destroyed
    {
        get { return destroyed; }
    }

    public Transform Temp
    {
        get { return temp; }
    }

    void Start()
    {
        temp = GameManager.Instance.Holes[Random.Range(0, GameManager.Instance.Holes.Count - 1)];

        while (GameManager.Instance.OccupiedHoles.Contains(temp))
        {
            temp = GameManager.Instance.Holes[Random.Range(0, GameManager.Instance.Holes.Count - 1)];
        }

        transform.position = temp.position;
        GameManager.Instance.OccupiedHoles.Add(temp);

        Invoke("Disappear", 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (destroyed)
            Destroy(gameObject, 1.0f);
    }

    private void Disappear()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        StartCoroutine(GameManager.Instance.RemoveHole(temp));
        destroyed = true;
    }

    public IEnumerator Hit()
    {
        CancelInvoke();

        GameManager.Instance.Score += 5;
        GameManager.Instance.ScoreText.text = "Score: " + GameManager.Instance.Score + "pts";

        destroyed = true;
        GetComponent<SpriteRenderer>().sprite = guacmole;
        yield return new WaitForSecondsRealtime(0.5f);

        GameManager.Instance.OccupiedHoles.Remove(temp);
        Destroy(gameObject);

    }
}
