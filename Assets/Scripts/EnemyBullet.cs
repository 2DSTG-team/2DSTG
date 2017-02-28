using UnityEngine;

public class EnemyBullet : MonoBehaviour {
    public float kSpeed = 2.5f;
    Player player;
    GameObject PlayerObj;
    /// <summary>
    /// 誘導弾 missile,ロケット弾 roket,自機狙い弾 aim bullet,
    /// 固定弾 fixed bullet
    /// 
    /// <para>誘導弾はそのまま、ロケット弾は攻撃力が高いやつ、
    /// 自機狙い弾はそのまま、ある方向にしか発射されない弾</para>
    /// </summary>
    string type;
    public int atk;

    public virtual void Create(int atk = 20,string type = "fixedBullet", float kSpeed = 2.5f) {
        this.kSpeed = kSpeed;
        this.type = type;
        this.atk = atk;
    }

    // Use this for initialization
    void Start() {
        PlayerObj = GameObject.FindGameObjectWithTag("Player");
        player = PlayerObj.GetComponent<Player>();
        switch (type) {
            case "fixedBullet":
                GetComponent<Rigidbody2D>().velocity = transform.up.normalized * kSpeed;
                break;
            case "aimBullet":
                
                break;
            case "machineGun":
                GetComponent<Rigidbody2D>().velocity = transform.up.normalized * 4.0f;
                break;
        }
    }

    // Update is called once per frame
    void Update() {
    }

    void OnTriggerEnter2D(Collider2D other) {
        var targetTag = other.gameObject.tag;
        if (targetTag == "Player") {
            player.HP -= atk;
            Destroy(gameObject);
        }
    }
}