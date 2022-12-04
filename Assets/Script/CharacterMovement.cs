using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
public class CharacterMovement : MonoBehaviour
{
    //God influnce
    public int delay {
        get => pressedKeys.GetDepth();
        set => pressedKeys.SetDepth(value);
    }
    bool canMove = true;
    public Score score;
    public GameOverScreen gameOverScreen;
    CommandQueue pressedKeys = new CommandQueue();

    public float movementSpeed = 1;
    public float jumpForce = 1;

    private Animator animator;
    private SpriteRenderer spr;

    [SerializeField] private LayerMask platformsLayerMask;
    private Rigidbody2D rigidbody2D;
    private BoxCollider2D boxCollider2D;

    private int doubleJump = 0;

    private bool facingRight = true;
    // Start is called before the first frame update
    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = transform.GetComponent<BoxCollider2D>();

        
    }

    // Update is called once per frame
    private void Update()
    {
        pressedKeys.Update();
        
        if(isOnGround() && pressedKeys.GetKeyDown(KeyCode.Space) && canMove){
            rigidbody2D.velocity = Vector2.up * jumpForce;
        }

        else if(doubleJump < 2 && pressedKeys.GetKeyDown(KeyCode.Space) && canMove){
            doubleJump ++;
            rigidbody2D.velocity = Vector2.up * jumpForce;

        }else if(doubleJump >=2 && isOnGround() && canMove){
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

        if(pressedKeys.GetKey(KeyCode.A) && canMove){
            rigidbody2D.velocity = new Vector2(-movementSpeed, rigidbody2D.velocity.y);
            spr.flipX = true;
            animator.SetBool("Walking", true);
            //animator.SetFloat("Speed", 3);
            if (facingRight){
                //flip();
            }
        }else{
            if(pressedKeys.GetKey(KeyCode.D) && canMove){
                rigidbody2D.velocity = new Vector2(+movementSpeed, rigidbody2D.velocity.y);
                spr.flipX = false;
                animator.SetBool("Walking", true);
                //animator.SetFloat("Speed", 3);
                if (!facingRight){
                    //flip();
                }
            }else{
                // NO KEYS PRESSED
                rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
                animator.SetBool("Walking", false);
                //animator.SetFloat("Speed", 0);
            }
        }
        
        
    }


    // private void flip(){
    //     facingRight = !(facingRight);
    //     transform.Rotate(0f,180f,0f);
    // }

    class CommandQueue{
        int depth;
        Queue<KeyCode[][]> commands;

        public CommandQueue(){
            depth = 1;
            commands = new Queue<KeyCode[][]>();
            commands.Enqueue(new KeyCode[][]{new KeyCode[0],new KeyCode[0],new KeyCode[0]});
        }

        static KeyCode[] keys = (KeyCode[])System.Enum.GetValues(typeof(KeyCode));
        // Update is called once per frame
        public void Update()
        {   
            List<KeyCode> pressedKeys = new List<KeyCode>();
            List<KeyCode> tappedKeys = new List<KeyCode>();
            List<KeyCode> releasedKeys = new List<KeyCode>();
            foreach(KeyCode kCode in keys){
                if (Input.GetKey(kCode))
                    pressedKeys.Add(kCode);
                if (Input.GetKeyDown(kCode))
                    tappedKeys.Add(kCode);
                if (Input.GetKeyUp(kCode))
                    releasedKeys.Add(kCode);
            }
            commands.Enqueue(
                new KeyCode[][]{pressedKeys.ToArray(),
                tappedKeys.ToArray(),
                releasedKeys.ToArray()});
            RemoveExcess();
        }

        private void RemoveExcess(){
            while (commands.Count > depth) commands.Dequeue();
        }
        private void FillToCapacity(){
            while (commands.Count < depth) commands.Enqueue(commands.Peek());
        }

        public bool GetKey(KeyCode k){
            return commands.Peek()[0].Contains(k);
        }
        public bool GetKeyDown(KeyCode k){
            return commands.Peek()[1].Contains(k);
        }
        public bool GetKeyUp(KeyCode k){
            return commands.Peek()[2].Contains(k);
        }
        public int GetDepth() {
            return depth;
        }
        public void SetDepth(int depth){
            depth = Mathf.Max(1, depth);
            Debug.Log("Set 2 " + depth);
            this.depth = depth;
            FillToCapacity();
            RemoveExcess();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "FallTrigger"){
            gameOverScreen.SetUp();
        }
        else if(other.tag == "Exam"){
            canMove = false;
            ObjectEntered = other;
            HavocTime = 5;
            animator.SetBool("Working", true);
            StartCoroutine("StopHavocInTime");
            score.increaseScore(20);  
        }
        else if(other.tag == "Lab") {
            canMove = false;
            ObjectEntered = other;
            HavocTime = 3;
            StartCoroutine("StopHavocInTime");
            score.increaseScore(15); 
        } 
    }
    private Collider2D ObjectEntered;
    private float HavocTime = 0;
    IEnumerator StopHavocInTime() {

        yield return new WaitForSeconds(HavocTime);
        animator.SetBool("Working", false);
        canMove = true;
        Destroy(ObjectEntered.gameObject);
    }
}
