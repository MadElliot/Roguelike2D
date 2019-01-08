using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {


    private Vector2 targetPos = new Vector2(1, 1);

    public float restTime = 1;
    private float Timer = 0;

   
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        GetComponent<Rigidbody2D>().MovePosition(Vector2.Lerp(transform.position,targetPos,Time.deltaTime*30));

        Timer += Time.deltaTime;
        if (Timer<restTime)return;

        //不平滑Raw处理，键盘输入始终是1，0，-1
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if (h>0)
        {
            v = 0;
        }
        if (h!=0||v!=0)
        {
            //检测
            GetComponent<BoxCollider2D>().enabled = false;
            RaycastHit2D hit = Physics2D.Linecast(targetPos, targetPos + new Vector2(h, v));
            GetComponent<BoxCollider2D>().enabled = true;
            if (hit.transform==null)
            {
                targetPos += new Vector2(h, v);
                
            }
            else
            {
                switch (hit.collider.tag)
                {
                    case "Outwall":
                        break;
                    case"Wall":
                        GetComponent<Animator>().SetTrigger("Attack");
                        hit.collider.SendMessage("TakeDamage");
                        break;
                    case"Food":
                        GameManager.Instance.AddFood(10);
                        targetPos += new Vector2(h, v);
                        Destroy(hit.collider.gameObject);
                        break;
                    case"Soda":
                        GameManager.Instance.AddFood(20);
                        targetPos += new Vector2(h, v);
                        Destroy(hit.collider.gameObject);
                        break;
                    case "Enemy":
                        break;
                }
            }
            GameManager.Instance.OnPlayerMove();
            Timer = 0;//不管是攻击还是移动都需要休息
        }

	}
    public void TakeDamage(int Hurt) {
        GameManager.Instance.ReduceFood(Hurt);
        GetComponent<Animator>().SetTrigger("Hurt");
    }
}
