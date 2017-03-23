using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tank1 : EnemyBase {
    public GameObject Hpbar;
    Slider slider;
    // Use this for initialization
    public override void Start() {
        Init();
        pos = transform.position;
        hp = maxHP;
        start_cnt = 10;
        cnt = 0;
        player = PlayerObj.GetComponent<Player>();
        slider = Hpbar.GetComponent<Slider>();
        slider.maxValue = maxHP;
    }

    // Update is called once per frame
    void Update() {
        slider.value = hp;
        start_cnt--;
        cnt++;
        Move();
        if (hp <= 0) {
            player.ScoreUP();
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
            ItemMake(transform.position);
        }
        transform.position = pos;
    }

    void Move() {
        pos.y -= speed * Time.deltaTime;
    }

    public void HitBox(string bulletType) {
        if (bulletType == "Normal") {
            hp -= 40;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        var targetTag = other.gameObject.tag;
        if (targetTag == "Player") {
            hp -= 120;
        }
    }
}
