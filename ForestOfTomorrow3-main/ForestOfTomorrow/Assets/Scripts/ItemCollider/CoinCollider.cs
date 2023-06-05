using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollider : MonoBehaviour
{
    private Rigidbody2D rb;
    public float dropForce = 5;
    public Equipment money;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector2 randomForce = new Vector2(Random.Range(-2, 2), Random.Range(-2, 2));
        rb.AddForce(randomForce * dropForce, ForceMode2D.Impulse);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            InventoryManagement.instance.AddMoney(money);
        }
    }
}
