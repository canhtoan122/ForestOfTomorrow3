using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBloodCollider : MonoBehaviour
{
    private Rigidbody2D rb;
    public float dropForce = 5;
    public InventoryData inventoryData;
    public Equipment monsterBlood;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector2 randomForce = new Vector2(Random.Range(-2, 2), Random.Range(-2, 2));
        rb.AddForce(randomForce * dropForce, ForceMode2D.Impulse);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            inventoryData.AddEquipment(monsterBlood);

        }
    }
}
