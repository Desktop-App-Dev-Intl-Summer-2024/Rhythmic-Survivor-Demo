using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float rangeAttack;
    private bool isWalking;

    AudioManager audioManager;
    MyGameManager gameManager;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        rangeAttack = 0.5f;
        isWalking = false;

        audioManager = GameObject.FindGameObjectWithTag("Managers").GetComponent<AudioManager>();
        gameManager = GameObject.FindGameObjectWithTag("Managers").GetComponent<MyGameManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isGamePause())
        {
            if(Vector3.Distance(transform.position, player.transform.position) > rangeAttack && !isWalking)
            {
                Debug.Log("entro");
                StartCoroutine(walk());
            }
        }
    }

    IEnumerator walk()
    {
        isWalking = true;
        while(Vector3.Distance(transform.position, player.transform.position) > 0.1)
        {
            Vector3 dir = Vector3.Normalize(transform.position - player.transform.position);
            transform.position -= new Vector3(dir.x * audioManager.getBPM() * Time.deltaTime, 0f, dir.z * audioManager.getBPM() * Time.deltaTime);
            yield return null;
        }
        isWalking = false;
    }
}
