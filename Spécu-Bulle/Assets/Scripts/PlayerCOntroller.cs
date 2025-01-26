using UnityEngine;

public class PlayerCOntroller : MonoBehaviour
{
    public Rigidbody2D rbPlayer;
    private float xPlayer;
    private float yPlayer;
    public float moveSpeed;
    public float moveSpeedStock;
    public float moveSpeedReduce;
    [SerializeField]private SpriteRenderer srPlayer;
    public static PlayerCOntroller instance;
  
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
        moveSpeedReduce = moveSpeed / 2;
        moveSpeedStock = moveSpeed;
    }
    
    void Update()
    {
        xPlayer = Input.GetAxis("Horizontal");
        yPlayer = Input.GetAxis("Vertical");
        
        rbPlayer.linearVelocity = new Vector2(xPlayer * moveSpeed, yPlayer * moveSpeed);

        if (rbPlayer.linearVelocity.x > 0)
        {
            srPlayer.flipX = false;
        }
        if (rbPlayer.linearVelocity.x < 0)
        {
            srPlayer.flipX = true;
        }
    }
}
