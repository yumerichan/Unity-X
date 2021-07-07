using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyCollision : MonoBehaviour
{
    // Start is called before the first frame update
    public Paladin_Script player_;
    // Update is called once per frame
    void Update()
    {
        transform.position = player_.GetPos();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "EnemyAttack")
        {
            player_.SetHp(player_.GetHp() - 20);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "EnemyAttack")
        {
            player_.SetHp(player_.GetHp() - 20);
        }
    }
}
