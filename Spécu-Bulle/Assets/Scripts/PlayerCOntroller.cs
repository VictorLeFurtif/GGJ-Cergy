using UnityEngine;

public class PlayerCOntroller : MonoBehaviour
{
    private Rigidbody2D rbPlayer;
    private float xPlayer;
    private float yPlayer;
    [SerializeField] private float moveSpeed;
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
