using Assets.Sources.Manager;
using Assets.Sources.Utils;
using Assets.Sources.Object;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        pos = new Pos();
    }
    
    private void Start()
    {
        
    }

    private void Update()
    {
        PlayerInput();
        PlayerAnim();
        UpdatePos();
    }

    private void PlayerInput()
    {
        if(!isTalking)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                StartCoroutine(Move(Vector2.left));
                dir = DIR.LEFT;

            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                StartCoroutine(Move(Vector2.right));
                dir = DIR.RIGHT;

            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                StartCoroutine(Move(Vector2.up));

            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                StartCoroutine(Move(Vector2.down));

            }
        }        

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {    
            Item.SetActive(!Item.activeSelf);
        }

        if (Input.GetKeyDown(KeyCode.E) && npcArea)
        {
            UIManager.Instance().TriggerCommentInterface();
            isTalking = !isTalking;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            UseItem();
        }
    }
    private void PlayerAnim()
    {
        if (isMoving) { animator.SetBool("isMoving", true); }
        else animator.SetBool("isMoving", false);

        if (dir == DIR.LEFT)
            spriteRenderer.flipX = false;

        if (dir == DIR.RIGHT)
            spriteRenderer.flipX = true;

        ItemFlip();
    }  
    private void ItemFlip()
    {
        SpriteRenderer render = Item.GetComponentInChildren<SpriteRenderer>();
        if (dir == DIR.LEFT)
        {
            render.flipX = false;
            Item.transform.position = new Vector2(this.gameObject.transform.position.x + -0.55f, 
                Item.transform.position.y);

        }
        else if (dir == DIR.RIGHT)
        {
            render.flipX = true;
            Item.transform.position = new Vector2(this.gameObject.transform.position.x + 0.55f, 
                Item.transform.position.y);
        }

    }
    private IEnumerator Move(Vector2 dir)
    {
        Vector3Int currentCell = GameManager.Instance().GetCurrentTileMap().WorldToCell(this.gameObject.transform.position);
        Vector3Int nextPos = currentCell + new Vector3Int((int)dir.x, (int)dir.y, 0);

        if (GameManager.Instance().GetCurrentTileMap().HasTile(nextPos) && !isMoving)
        {
            isMoving = true;
            Vector3 targetPos = GameManager.Instance().GetCurrentTileMap().GetCellCenterWorld(nextPos);
            Vector3 direction = (targetPos - this.gameObject.transform.position).normalized;

            while (Vector2.Distance(this.gameObject.transform.position, targetPos) > 0.1f)
            {
                this.gameObject.transform.position += direction * speed * Time.deltaTime;
                yield return null;
            }

            this.gameObject.transform.position = targetPos;
            isMoving = false;
        }
    }
    
    public void UseItem()
    {
        if (!Item.activeSelf || !isFarming) return;
        
        Pos pos = GetPlayerCell();
        Vector3Int direction = dir == DIR.RIGHT ? Vector3Int.right : Vector3Int.left;
        Vector3Int frontCell = new Vector3Int(pos.x, pos.y, 0) + direction;
        if (!GameManager.Instance().GetCurrentTileMap().HasTile(frontCell)) return; 
        
        Vector2 spawnDest = GameManager.Instance().GetCurrentTileMap().GetCellCenterWorld(frontCell);
        Instantiate(farmGrond, spawnDest, Quaternion.identity);
    }
    public void SetChar(string _name, CHARTYPE _type)
    {
        SetCharType(_type);
        SetCharName(_name);
    }
    public CHARTYPE GetCharType() { return type; }
    
    public Pos GetPlayerCell() { return pos; }
    public void SetPlayerCell(Pos pos) { this.pos = pos; }
    
    public void SetCharType(CHARTYPE _type)
    {
        type = _type; 
        if (type == CHARTYPE.GIRL)
            animator.runtimeAnimatorController = FemaleController;
        else if(type == CHARTYPE.BOY)
            animator.runtimeAnimatorController = MaleController;

    }
    public void SetCharName(string str)
    {
        Name.text = str;
    }
    public void UpdatePos()
    {
        Tilemap map = GameManager.Instance().GetCurrentTileMap();
        Vector2Int cellPos = (Vector2Int)map.WorldToCell(this.gameObject.transform.position);
        pos.x = cellPos.x;
        pos.y = cellPos.y;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Farm"))
        {
            isFarming = true;
        }

        if(collision.CompareTag("Npc"))
        {
            Npc npc = collision.gameObject.GetComponent<Npc>();
            npc.CharCommentSetting();
            npcArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Farm"))
        {
            isFarming = false;
        }

        if (collision.CompareTag("Npc"))
        {
            npcArea = false;
        }
    }

    //================================================================
    public Animator animator;
    public RuntimeAnimatorController FemaleController;
    public RuntimeAnimatorController MaleController;
    public SpriteRenderer spriteRenderer;
    public GameObject Item = null;
    public GameObject farmGrond = null;
    public Text Name;

    public float speed;
    private bool isTalking = false;
    private bool isMoving = false;
    private bool isFarming = false;
    private bool npcArea = false;
    
    private DIR dir = DIR.LEFT;
    private CHARTYPE type = CHARTYPE.NONE;
    private Pos pos;
}
