using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerCharacterAnimationControl animController;

    GameObject boxObj = null;
    GameObject leverObj = null;
    GameObject torchObj = null;

    Rigidbody2D rigid;

    public float moveSpeed;
    public float runSpeed;
    public float jumpPower;
    private float speed;
    private float leftKeyTime;
    private float rightKeyTime;

    bool leftRunChk = false;
    bool rightRunChk = false;

    [HideInInspector]
    public bool isGround = false;
    bool isLadder = false;

    Vector2 moveDir;

    private void Start()
    {
        speed = moveSpeed;
        rigid = GetComponent<Rigidbody2D>();

        if (!animController)
            animController = GetComponent<PlayerCharacterAnimationControl>();

        Load();
    }

    void Update()
    {
        RunAlgorithm();

        JumpOrLaddering();

        PushBox();

        ChkDead();

        if (PlayerInfo.Instance.ColdGuage >= PlayerInfo.Instance.MaxColdGuage)
        {
            Destroy();
            PlayerInfo.Instance.ColdGuage = 0;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Destroy();
        }
    }

    void SetDefault()
    {
        PlayerPrefs.SetFloat("PosX", 20);
        PlayerPrefs.SetFloat("PosY", -3);
    }

    /// <summary>
    /// 플레이어 달리기 함수
    /// </summary>
    void RunAlgorithm()
    {
        if (leftRunChk && (Time.time - leftKeyTime) > 0.5f)
        {
            leftRunChk = false;
        }
        if (rightRunChk && (Time.time - rightKeyTime) > 0.5f)
        {
            rightRunChk = false;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            moveDir = new Vector2(-1, 0);
            if (!leftRunChk)
            {
                leftRunChk = true;
                leftKeyTime = Time.time;
            }
            else if (leftRunChk && Time.time - leftKeyTime <= 0.5f)
            {
                leftRunChk = false;
                speed = runSpeed;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            moveDir = new Vector2(1, 0);
            if (!rightRunChk)
            {
                rightKeyTime = Time.time;
                rightRunChk = true;
            }
            else if (rightRunChk && Time.time - rightKeyTime <= 0.5f)
            {
                rightRunChk = false;
                speed = runSpeed;
            }
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            speed = moveSpeed;

    }

    /// <summary>
    /// 점프하거나 사다리를 오르는 함수
    /// </summary>
    void JumpOrLaddering()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, 0.9f);
        foreach (var hit in hits)
        {
            if (hit.transform.CompareTag("Platform") || hit.transform.CompareTag("Box"))
            {
                isGround = true;

                if (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
                    rigid.velocity = new Vector2(0, rigid.velocity.y);
            }

            else
                isGround = false;
        }

        if (isGround && Input.GetKeyDown(KeyCode.W))
        {
            rigid.AddForce(Vector2.up * jumpPower);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            if (isLadder)
            {
                rigid.velocity = Vector2.zero;
                transform.Translate(Vector2.up * (jumpPower / 100) * Time.deltaTime);
                animController.SetLadderAnim(true);
            }
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            if (isLadder)
                animController.SetLadderAnim(false);
        }
    }

    /// <summary>
    /// 상자를 미는 함수
    /// </summary>
    void PushBox()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, moveDir, 0.6f);

        foreach (var hit in hits)
        {
            if (hit.transform.CompareTag("Box") || hit.transform.CompareTag("BoxLadder"))
            {
                boxObj = hit.transform.gameObject;
                boxObj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                    animController.SetPushAnimation(true);
                else
                    animController.SetPushAnimation(false);
            }
            else
            {
                animController.SetPushAnimation(false);
                boxObj = null;
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (leverObj)
            {
                leverObj.GetComponent<LeverObject>().ObjectAction();
            }

            else if (torchObj)
            {
                Save();
            }
        }

    }

    /// <summary>
    /// 플레이어 사망 여부를 확인하는 함수 (상자 충돌)
    /// </summary>
    void ChkDead()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.up, 0.9f);

        foreach (var hit in hits)
        {
            if (hit.transform.CompareTag("Box"))
            {
                Destroy();
            }
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    /// 플레이어 걸음 함수
    /// </summary>
    private void Move()
    {
        float h = Input.GetAxis("Horizontal");

        RidingHook ridingHook = GetComponent<RidingHook>();
        Hook hook = ridingHook.hook.GetComponent<Hook>();

        if (h != 0)
        {
            if (ridingHook.isAttack)
            {
                rigid.AddForce(new Vector2(h * (speed / 2) * Time.deltaTime, 0));
            }

            else
                rigid.velocity = (new Vector2(h * speed * Time.deltaTime, rigid.velocity.y));
        }
        else
        {
            speed = moveSpeed;
            leftKeyTime = 0;
            rightKeyTime = 0;
        }

        if (hook.targetObject && transform.position.y < hook.transform.position.y - 1.5f)
        {
            if (hook.targetObject.transform.position.y - transform.position.y < 2f)
            {
                if (transform.position.x >= hook.targetObject.transform.position.x)
                    rigid.AddForce(new Vector2((-speed * 3) * Time.deltaTime, 0));
                else
                    rigid.AddForce(new Vector2((speed * 3) * Time.deltaTime, 0));
            }
        }
        else if (hook.targetObject && transform.position.y >= hook.transform.position.y - 1.5F)
        {
            rigid.AddForce(new Vector2(0, -(speed * 10) * Time.deltaTime));
        }
    }

    void UpColdGuage()
    {
        StopCoroutine(DownColdGuage());
        if (PlayerInfo.Instance.ColdGuage >= PlayerInfo.Instance.MaxColdGuage || torchObj)
        {
            CancelInvoke("UpColdGuage");
            return;
        }
        PlayerInfo.Instance.ColdGuage++;
    }

    IEnumerator DownColdGuage()
    {
        CancelInvoke("UpColdGuage");

        while (PlayerInfo.Instance.ColdGuage > 0)
        {
            yield return new WaitForSeconds(0.5f);
            PlayerInfo.Instance.ColdGuage--;
        }
    }

    /// <summary>
    /// 플레이어 좌표 저장
    /// </summary>
    void Save()
    {
        TorchManager.instance.TorchToggle(torchObj.GetComponent<TorchObject>());
        PlayerPrefs.SetFloat("PosX", transform.position.x);
        PlayerPrefs.SetFloat("PosY", transform.position.y);
    }

    /// <summary>
    /// 플레이어 좌표 불러오기
    /// </summary>
    void Load()
    {
        Vector2 loadPos = new Vector2(PlayerPrefs.GetFloat("PosX"), PlayerPrefs.GetFloat("PosY"));
        transform.position = loadPos;
    }

    bool isColdStart = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
            isLadder = true;
        else if (collision.CompareTag("Lever"))
            leverObj = collision.gameObject;
        else if (collision.CompareTag("Torch"))
        {
            torchObj = collision.gameObject;
            Save();
            StartCoroutine(DownColdGuage());
        }
        else if (collision.CompareTag("Deadzone"))
            Destroy();
        else if (collision.CompareTag("ClearZone"))
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            animController.SetLadderAnim(false);
        }

        else if (collision.CompareTag("Lever"))
            leverObj = null;
        else if (collision.CompareTag("ColdZone"))
        {
            isColdStart = false;
            StartCoroutine(DownColdGuage());
        }
        else if (collision.CompareTag("Torch"))
        {
            torchObj = null;
            if (isColdStart)
            {
                isColdStart = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("ColdZone"))
        {
            if (!isColdStart)
            {
                isColdStart = true;
                InvokeRepeating("UpColdGuage", 0, 1);
            }
        }
    }

    public void Destroy()
    {
        enabled = false;

        UnityEngine.SceneManagement.SceneManager.LoadScene("InGame");
    }
}
