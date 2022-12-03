using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    public float movementSpeed = 1;
    public float jumpForce = 1;

    //public Animator animator;

    [SerializeField] private LayerMask platformsLayerMask;
    private Rigidbody2D rigidbody2D;
    private BoxCollider2D boxCollider2D;

    private int doubleJump = 0;

    private bool facingRight = true;
    // Start is called before the first frame update
    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = transform.GetComponent<BoxCollider2D>();

        
    }

    // Update is called once per frame
    private void Update()
    {

        
        if(isOnGround() && Input.GetKeyDown(KeyCode.Space)){
            rigidbody2D.velocity = Vector2.up * jumpForce;
        }

        else if(doubleJump < 2 && Input.GetKeyDown(KeyCode.Space)){
            doubleJump ++;
            rigidbody2D.velocity = Vector2.up * jumpForce;

        }else if(doubleJump >=2 && isOnGround()){
            doubleJump = 0;


        }

        if(!isOnGround()){
            //animator.SetBool("isJumping", true);
        }else{
            //animator.SetBool("isJumping", false);
        }
        handleMovement();
    }


    private bool isOnGround(){
        RaycastHit2D raycastHit2D =  Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down , .1f, platformsLayerMask);
        return raycastHit2D.collider != null;
    }


    private void handleMovement(){
   
        if(Input.GetKey(KeyCode.Y)){
            
        }
        if(Input.GetKey(KeyCode.A)){
            rigidbody2D.velocity = new Vector2(-movementSpeed, rigidbody2D.velocity.y);
            //animator.SetFloat("Speed", 3);
            if(facingRight){
                //flip();
            }
        }else{
            if(Input.GetKey(KeyCode.D)){
                rigidbody2D.velocity = new Vector2(+movementSpeed, rigidbody2D.velocity.y);
                //animator.SetFloat("Speed", 3);
                if(!facingRight){
                    //flip();
                }
            }else{
                // NO KEYS PRESSED
                rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
                //animator.SetFloat("Speed", 0);
            }
        }
    }


    // private void flip(){
    //     facingRight = !(facingRight);
    //     transform.Rotate(0f,180f,0f);
    // }


}