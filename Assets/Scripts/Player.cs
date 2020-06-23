using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   
    private Rigidbody2D _rigid; 
    [SerializeField]
    private float _jumpedForce = 5.0f;
    private bool _resetJump = false;
    [SerializeField]
    private float speed = 5.0f;

    private PlayerAnimation _anim;
    void Start()
    {
        //assign handle of rigidbody
        _rigid = GetComponent<Rigidbody2D>();
        _anim = GetComponent<PlayerAnimation>();
    }

    // Update is called once per frame
    void Update()
    {   
        Movement();
    }

    void Movement(){
        float move = Input.GetAxisRaw("Horizontal");
         
         if(Input.GetKeyDown(KeyCode.Space) && isGrounded() == true){
             Debug.Log("Jump!");
             _rigid.velocity = new Vector2(_rigid.velocity.x ,_jumpedForce);
             StartCoroutine(ResetJumpRoutine());
         }


         _rigid.velocity = new Vector2(move * speed,_rigid.velocity.y);

         _anim.Move(move);
    }//Movement

    bool isGrounded(){
       RaycastHit2D hitInfo = Physics2D.Raycast(transform.position,Vector2.down, 0.6f, 1 << 8);
        if(hitInfo.collider != null){
            if(_resetJump == false){
                return true;
            }
        }
        return false;
    }

    IEnumerator ResetJumpRoutine(){
        _resetJump = true;
        yield return new WaitForSeconds(1.0f);
        _resetJump = false;
    }

}
