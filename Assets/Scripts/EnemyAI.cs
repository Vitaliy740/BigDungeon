using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Animator anim;
    private Transform player;
    public bool isAttacking=false;
    public float speed = 1f;
    public float signrange = 4;
    public double attackrange = 0.96;
    public bool rotatinge;
    void Awake()
    {
        player = GameObject.Find("Игрок").transform;
    }
    public void LoadData(Save.EnemySaveData save)
    {
        GetComponent<Enemyhealthbar>().enemyhealth = save.Health;
        transform.position = new Vector3(save.Position.x, save.Position.y, save.Position.z);
        GetComponent<Enemyhealthbar>().UpdateHealthBar();
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        Vector2 reliativPos = player.position - transform.position;
        var napr = reliativPos.magnitude;
        
        if ((Mathf.Abs(napr) < signrange) && (Mathf.Abs(napr) > 0.9))
        {
            Quaternion nr = transform.rotation;
            Quaternion rot = Quaternion.LookRotation(Vector3.back, reliativPos);
            transform.rotation = rot;
            transform.Translate(new Vector3(0, Time.deltaTime * speed));
            if (!rotatinge)
            {
                transform.rotation = nr;
            }

            if (Mathf.Abs(napr) < attackrange)
            {
                anim.SetTrigger("Attack");
                isAttacking = true;
            }
            else isAttacking = false;
        }
    }

}
    

