using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class New_Enemy : MonoBehaviour {

    private Transform Player;
    private Vector2 targetPosition;

    public int Hurt = 10;
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        targetPosition = transform.position;

        GameManager.Instance.enemylist.Add(this);
	}

    void LateUpdate() {

        GetComponent<Rigidbody2D>().MovePosition(Vector2.Lerp(transform.position, targetPosition, 30*Time.deltaTime));
    }
    public void Move() {
        Vector2 offset = Player.position - transform.position;

        if (offset.magnitude < 1.1)
        {
            //攻击
            GetComponent<Animator>().SetTrigger("EnemyAttack");
            Player.SendMessage("TakeDamage", Hurt);
        }
        else
        {
            float x = 0, y = 0;
            if (Mathf.Abs(offset.y) > Mathf.Abs(offset.x))
            {
                  //向y轴移动
                if (offset.y < 0)
                {
                    y = -1;
                }
                else y = 1;
            }
            else
            {
                    //向x轴移动
                if (offset.x > 0)
                {
                    x = 1;
                }
                else x = -1;
            }
            GetComponent<BoxCollider2D>().enabled = false;
            RaycastHit2D hit = Physics2D.Linecast(targetPosition, targetPosition + new Vector2(x, y));
            GetComponent<BoxCollider2D>().enabled = true;
            if (hit.transform == null)
            {
                targetPosition += new Vector2(x, y);
            }
            else
            {
                if (hit.collider.tag == "Soda"||hit.collider.tag=="Food")
                {
                    targetPosition += new Vector2(x, y);
                }
            }
        }
    }
}
