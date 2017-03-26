using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {
    public float timeScale=1.0f;
    public int shot_freq;
    const int kShotSpeed = 10,kSpeed =6;
    int shotCnt = 0;
    public static int score;
    public GameObject bullet;
    public GameObject HpGauge;
    public Text textHpUI;
    public Text textScore;
    public Text textPower;

    public int HP, maxHP;
    public int power, atk;

    // Use this for initialization
    void Start() {
        shot_freq = 16;
        power = 1;
        score = 0;
        maxHP = 500;
        HP = maxHP;
        textScore.text = "score:" + score.ToString();
        textPower.text = "power:" + power;
        textHpUI.text = "" + HP;
    }
    #region Main
    // Update is called once per frame
    void Update() {
        if (maxHP < HP) {
            HP = maxHP;
        }
        HpGauge.GetComponent<Slider>().value= HP;
        Time.timeScale = timeScale;
        PowerSet();
        textScore.text = "score:" + score.ToString();
        textPower.text = "power:" + power;
        textHpUI.text = "" + HP;
        shotCnt++;
        float x = Input.GetAxisRaw("Horizontal") * 1.7f;
        float y = Input.GetAxisRaw("Vertical");
        Vector2 direction = new Vector2(x, y).normalized;
        Shot();
        Move(direction);
        float percent = (float)HP / (maxHP)*100;
        //Debug.LogFormat("hp="+HP+"MaxHP"+maxHP+"percent"+percent);
    }

    void PowerSet() {
        if (power == 1) {
            atk=20;
            shot_freq = 16;
        } else if (power == 2) {
            atk = 30;
            shot_freq = 13;
        } else if (power == 3) {
            atk = 40;
            shot_freq = 10;
        }


        if (score >= 200 && score < 500) {
            power = 2;

        } else if (score >= 500 && score < 800) {
            power = 3;
        }
    }

    void Move(Vector2 direction) {
        // 画面左下のワールド座標をビューポートから取得
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        // 画面右上のワールド座標をビューポートから取得
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        // プレイヤーの座標を取得
        Vector2 pos = transform.position;
        // 移動量を加える
        pos += direction * kSpeed * Time.deltaTime;
        // プレイヤーの位置が画面内に収まるように制限をかける
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);
        // 制限をかけた値をプレイヤーの位置とする
        transform.position = pos;
    }

    void Shot() {
        if (Input.GetKey(KeyCode.Space) && shotCnt % shot_freq == 0) {
            switch (power) {
                case 1: {
                        ShotVelocity(ShotIntantiate(0.2f, 0), kShotSpeed);
                        ShotVelocity(ShotIntantiate(-0.2f, 0), kShotSpeed);
                        break;
                    }
                case 2: {
                        ShotVelocity(ShotIntantiate(0.3f, 0, 150.0f), kShotSpeed);
                        ShotVelocity(ShotIntantiate(0.2f, 0), kShotSpeed);
                        ShotVelocity(ShotIntantiate(-0.2f, 0), kShotSpeed);
                        ShotVelocity(ShotIntantiate(-0.3f, 0, 210.0f), kSpeed);
                        break;
                    }
                case 3: {
                        ShotVelocity(ShotIntantiate(0.5f,0, 150.0f), kShotSpeed);
                        ShotVelocity(ShotIntantiate(0.5f, 0, 160.0f), kShotSpeed);
                        ShotVelocity(ShotIntantiate(0.3f, 0), kShotSpeed);
                        ShotVelocity(ShotIntantiate(0, 0), kShotSpeed);
                        ShotVelocity(ShotIntantiate(-0.3f, 0), kShotSpeed);
                        ShotVelocity(ShotIntantiate(-0.5f, 0, 200f), kShotSpeed);
                        ShotVelocity(ShotIntantiate(-0.5f, 0, 210.0f), kShotSpeed);
                        break;
                    }

            }
        }
    }
    //どれだけ離れた位置に弾を生成するか
    GameObject ShotIntantiate(float dx, float dy,float rotaion=180) {
        GameObject Obj = Instantiate(bullet, new Vector2(transform.position.x+dx, transform.position.y+dy), Quaternion.Euler(0, 0, rotaion));
        return Obj;
    }
    void ShotVelocity(GameObject obj,float speed){
        obj.GetComponent<Rigidbody2D>().velocity= -obj.transform.up.normalized * speed;
    }

    #endregion Main

    void OnTriggerEnter2D(Collider2D c) {
        // レイヤー名を取得
        string layerName = LayerMask.LayerToName(c.gameObject.layer);

        // レイヤー名がBullet (Enemy)の時は弾を削除
        if (layerName == "Bullet (Enemy)") {
            // 弾の削除
            Destroy(c.gameObject);
        }

        switch (c.gameObject.tag) {
            case "Zako1":
                HP -= 20;
                break;
            case "Zako2":
                HP -= 30;
                break;
            case "Tank1":
                HP -= 50;
                break;
            case "FixedTurret":
            case "AircraftCarrier":
                HP -= 100;
                break;
        }
    }

    public void ScoreUP() {
        score += 10;
    }
    public int GetScore() {
        return score;
    }
}
