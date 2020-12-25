using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController playerController;

    float xValue;
    Vector3 scaleLeft, scaleRight;

    Animator anim;

    bool isDead = false;

    [SerializeField]
    AudioSource chop;


    [SerializeField]
    GameObject tapLeftButton, tapRightButton;

    // Start is called before the first frame update
    void Start()
    {
        playerController = this;
        xValue = transform.localScale.x;
        scaleLeft = new Vector3(xValue, transform.localScale.y, transform.localScale.z);
        scaleRight = new Vector3(-xValue, transform.localScale.y, transform.localScale.z);

        anim = GetComponent<Animator>();
    }


    public void OnLeftTap()
    {
        if (!isDead && !GameManager.gameManager.IsGameOver() && !GameManager.gameManager.IsGamePaused())
        {
            if (!GameManager.gameManager.IsGameStarted())
            {
                GameManager.gameManager.GameHasStarted();
                tapLeftButton.SetActive(false);
                tapRightButton.SetActive(false);
            }
            transform.localScale = scaleLeft;
            anim.SetTrigger("Cut");
            chop.Play();

            if (!TrunkSpawner.trunkSpawner.CheckHitLeft())
            {
                TrunkSpawner.trunkSpawner.RemoveNextTrunk();
                GameManager.gameManager.OnScoreIncrease();
            }
            else
            {
                PlayerIsDead();

            }
        }
    }

    public void OnRightTap()
    {
        if (!isDead && !GameManager.gameManager.IsGameOver() && !GameManager.gameManager.IsGamePaused())
        {
            if (!GameManager.gameManager.IsGameStarted())
            {
                tapLeftButton.SetActive(false);
                tapRightButton.SetActive(false);
                GameManager.gameManager.GameHasStarted();
            }
            transform.localScale = scaleRight;
            anim.SetTrigger("Cut");
            chop.Play();
            if (!TrunkSpawner.trunkSpawner.CheckHitRight())
            {
                TrunkSpawner.trunkSpawner.RemoveNextTrunk();
                GameManager.gameManager.OnScoreIncrease();

            }
            else
            {
                PlayerIsDead();
            }
        }

    }

    public void ResetPlayer()
    {
        isDead = false;
        anim.ResetTrigger("Cut");
        anim.SetBool("IsDead", isDead);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Obstacle")
        {
            PlayerIsDead();
        }
    }

    void PlayerIsDead()
    {
        isDead = true;
        anim.SetBool("IsDead", isDead);
        GameManager.gameManager.OnGameOver();

    }
}
