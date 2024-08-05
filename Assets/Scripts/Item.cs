using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Zenject;

public class Item : MonoBehaviour
{
    public ItemConfig itemConfig;
    private Transform player;
    private bool moveTowardsPlayer;
    [Inject] private GameManager gameManager;

    void Start()
    {
        InitializeItem();
    }

    private void Update()
    {
        if (player == null)
            return;

        if (moveTowardsPlayer)
            MoveTowardsPlayer();
        else
        {
            CheckDistanceWithPlayer();
        }
    }

    private void InitializeItem()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = itemConfig.sprite;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player == null)
        {
            Debug.LogWarning("Player not found! Ensure player exist or it has Player tag");
        }
    }
    //This function is called when item touches the player
    private void PlayerTouch()
    {
        gameManager.PropTouched(itemConfig.type,itemConfig.increaseAmount);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerTouch();
        }
    }
    public void InjectManuallyGameManager(GameManager gm)
    {
        gameManager = gm;
    }

    //Check if player is close enough to start chasing
    private void CheckDistanceWithPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= itemConfig.distanceToMoveTowardsPlayer)
            CanMoveTowardsPlayer();
    }

    // start chasing the player
    private void CanMoveTowardsPlayer()
    {
        moveTowardsPlayer = true;
    }
    private void MoveTowardsPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, itemConfig.moveSpeed * Time.deltaTime);
    }
}
