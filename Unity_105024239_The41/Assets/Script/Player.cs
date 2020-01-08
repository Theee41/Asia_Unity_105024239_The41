using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform Target;
    public float rotSpeed = 15.0f;
    public float moveSpeed = 10.0f;
    public Rigidbody rig;
    public Animator ani;
    public Rigidbody rigCatch;
    // Start is called before the first frame update
    void Start()
    {
        rig = this.GetComponent<Rigidbody>();
        ani = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Catch();
        move();
        
    }



    private void move()
    {
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            ani.SetBool("move", false);
            rig.velocity = Vector3.zero;
            return;
        }
            

        Vector3 movement = Vector3.zero;
        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");

        if (horInput != 0 || vertInput != 0)
        {
            ani.SetBool("move", true);
            movement.x = horInput;
            movement.z = vertInput;

            rig.velocity = movement * moveSpeed; //移動

            //旋轉
            Quaternion tmp = Target.rotation;
            Target.eulerAngles = new Vector3(0, Target.eulerAngles.y, 0);
            movement = Target.TransformDirection(movement);
            Target.rotation = tmp;

            Quaternion direction = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed * Time.deltaTime);


        }
        else
            ani.SetBool("move", false);
    }

    private void Catch()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // 按下左鍵撿東西
            ani.SetTrigger("atk");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "tresure_box" && ani.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            // 物理.忽略碰撞(A碰撞，B碰撞)
            Physics.IgnoreCollision(other, GetComponent<Collider>());
            // 碰撞物件.取得元件<泛型>().連接身體 = 檢物品位置
            other.GetComponent<HingeJoint>().connectedBody = rigCatch;
        }
    }
}
