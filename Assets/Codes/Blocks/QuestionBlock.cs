using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBlock : MonoBehaviour
{
    [SerializeField] private float bounceHeight = 0.5f;
    [SerializeField] private float bounceSpeed = 2f;

    private Vector2 orginalPosition;

    [SerializeField] Sprite emptyBlockSprite;
    [SerializeField] GameObject coinPrefab;

    [SerializeField] PlayerScore playerScoreComponent;

    private bool canBounce = true;

    private void Start()
    {
        orginalPosition = transform.position;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.bounds.max.y < transform.position.y
            && col.collider.bounds.min.x < transform.position.x + 0.5f
            && col.collider.bounds.max.x > transform.position.x - 0.5f
            && col.collider.tag == "Player")
        {
            QuestionBlockBounce();
        }
    }

    public void QuestionBlockBounce()
    {
        if (!canBounce)
        {
            return;
        }

        canBounce = false;

        StartCoroutine(Bounce());
    }

    IEnumerator Bounce()
    {
        ChangeSprite();
        PresentCoin();
        AddPoints();

        while (transform.position.y < orginalPosition.y + bounceHeight)
        {
            transform.position = new Vector2(
                transform.position.x, 
                transform.position.y + bounceSpeed * Time.deltaTime
            );

            yield return null;
        }

        while (transform.position.y > orginalPosition.y)
        {
            transform.position = new Vector2(
                transform.position.x, 
                transform.position.y - bounceSpeed * Time.deltaTime
            );

            yield return null;
        }

        transform.position = orginalPosition;
    }

    void ChangeSprite ()
    {
        GetComponent<Animator>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = emptyBlockSprite;
    }

    void PresentCoin()
    {
        GameObject spinningCoin = Instantiate(coinPrefab);

        spinningCoin.transform.SetParent(this.transform);
    }

    void AddPoints()
    {
        playerScoreComponent.AddCoinScore();
    }
}

