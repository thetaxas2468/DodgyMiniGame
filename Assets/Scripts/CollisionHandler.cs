using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    public TextMeshProUGUI counterText;
    public GameObject winPanel;

    private int count = 0;
    public static bool gameWon = false;

    private HashSet<GameObject> collidedObjects = new HashSet<GameObject>();

    void Start()
    {
        counterText = GameObject.Find("CounterText").GetComponent<TextMeshProUGUI>();

        winPanel.SetActive(false);
        UpdateUI();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (gameWon) return;

        Renderer renderer = other.gameObject.GetComponent<Renderer>();

        if (renderer != null && !collidedObjects.Contains(other.gameObject) && !(other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("FollowingTrap")))
        {
            collidedObjects.Add(other.gameObject);

            count++;
            UpdateUI();

            renderer.material.color = Color.black;

            if (count >= 2)
            {
                ShowWin();
            }
        }
    }

    void ShowWin()
    {
        gameWon = true;


        winPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Rematch()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameWon = false;
    }

    void UpdateUI()
    {
        if (counterText != null)
        {
            counterText.text = count.ToString();
        }
    }
}