using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FollowingTrap : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed = 1f;
    [SerializeField]
    public Transform playerToFollow;
    [SerializeField]
    public GameObject lossPanel;
    [SerializeField]
    public Transform groundTransform;
    public int toMove = 0;

    void Start()
    {

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerToFollow = player?.transform;

        GameObject ground = GameObject.FindGameObjectWithTag("Ground");
        groundTransform = ground?.transform;

        this.enabled = false;
        
        

    }

    // Update is called once per frame
    void Update()
    {
        if (playerToFollow == null) return;
        bool isAbove = playerToFollow.position.y > transform.position.y + 0.5f;
        bool isClose = Vector3.Distance(transform.position, playerToFollow.position) < 1f;
        bool closeToGround = transform.position.y - groundTransform.position.y < playerToFollow.localScale.y / 2 + 0.1f;


        if ((isAbove && isClose) || closeToGround)
        {
            gameObject.SetActive(false);
            return;
        }

        // ✔ MOVEMENT
        movePlayer();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Game Over!");
            showLoss();
        }
    }

    public void showLoss()
    {
        GameManager.instance.GameOver();
        Time.timeScale = 0f;
    }

    public void Rematch()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        CollisionHandler.gameWon = false;
    }

    public void movePlayer()
    {
        if (toMove == 0) return;
        transform.position = Vector3.MoveTowards(
            transform.position,
            playerToFollow.position,
            moveSpeed * Time.deltaTime
        );
    }
}
