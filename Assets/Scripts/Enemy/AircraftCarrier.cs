using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AircraftCarrier : EnemyBase {
    public GameObject Plane;
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
        if (cnt % 150 == 0) {
            Create();
        }
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
    //艦載機を出す
    void Create() {
        var tempHP = 1;
        var plane = Instantiate(Plane, transform.position, Quaternion.identity);
		plane.GetComponent<Zako1>().Create(speed + 1.0f, tempHP, 20, 80, true);
        switch (PlayerObj.GetComponent<Player>().power) {
            case 1:
                tempHP = 50;
                break;
            case 2:
                tempHP = 150;
                break;
            case 3:
                tempHP = 250;
                break;
        }
    }

    public void HitBox(string bulletType) {
        if (bulletType == "Normal") {
            hp -= player.atk;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        var targetTag = other.gameObject.tag;
        if (targetTag == "Player") {
            hp -= 120;
        }
    }
}
