using UnityEngine;
using UnityEngine.AI;

public class Enamy : MonoBehaviour
{
    [Header("移動速度"), Range(0,50)]
    public float speed = 3;
    [Header("停止距離"), Range(0, 50)]
    public float stopDistance = 2.5f;
    [Header("攻擊冷卻時間"), Range(0, 50)]
    public float cd = 2f;
    [Header("攻擊中心點")]
    public Transform atkPoint;
    [Header("攻擊長度"), Range(0f, 5f)]
    public float atkLength;

    private Transform player;
    private NavMeshAgent nav;
    private Animator ani;
    /// <summary>
    /// 計時器
    /// </summary>
    private float timer;

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
        player = GameObject.Find("Robot Kyle").transform;
        nav.speed = speed;
        nav.stoppingDistance = stopDistance;
    }

    private void Update()
    {
        Track();
        Attack();
    }
    
    /// <summary>
    /// 繪製圖示事件 : 僅在 Unity 內顯示
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawRay(atkPoint.position, atkPoint.forward * atkLength);
    }

    /// <summary>
    /// 射線擊中的物件
    /// </summary>
    private RaycastHit hit;

    /// <summary>
    /// 攻擊
    /// </summary>
    private void Attack()
    {
        if (nav.remainingDistance < stopDistance)
        {
            timer += Time.deltaTime;

            Vector3 pos = player.position;
            pos.y = transform.position.y;
            transform.LookAt(pos);

            if (timer >= cd)
            {
                ani.SetTrigger("攻擊觸發");
                timer = 0;

                if(Physics.Raycast(atkPoint.position, atkPoint.forward, out hit, atkLength, 1 << 8))
                {
                    hit.collider.GetComponent<Player>().Damage();
                }
            }
            
        }
    }


    /// <summary>
    /// 追蹤
    /// </summary>
    private void Track()
    {
        nav.SetDestination(player.position);

        ani.SetBool("跑步開關", nav.remainingDistance > stopDistance);
    }
}
