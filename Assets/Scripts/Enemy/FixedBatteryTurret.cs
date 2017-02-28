using UnityEngine;
using System.Collections;

public class FixedBatteryTurret : MonoBehaviour {
    float speed = 0.5f;
    public int turretNumber;//大砲の番号、これはUnityのインスペクタで数字いれる

    public GameObject player;
    public GameObject turret;
    public Quaternion playerQuater;
    public Quaternion turretQuater;
    FixedBattery battery;

    public float targetAngle;
    public float currentTurretAngle;
    float rotZ;

    float x, y;
    bool isOn;

    private Vector3 enemyPos, playerPos;
    private object a;

    // Use this for initialization
    void Start() {
        var objs = GameObject.FindGameObjectsWithTag("FixedBatteryTurret");
        foreach (var obj in objs) {
            if (obj.gameObject.name == "turret" + turretNumber) {
                turret = obj.gameObject;
            }
        }



        battery = GetComponentInParent<FixedBattery>();
        player = GameObject.FindGameObjectWithTag("Player");
        for (int i = 0; i < 4; i++) {
            if (gameObject.name == "turret" + i) {
                turretNumber = i;
            }
        }
    }

    public void Shot(int atk) {
        Vector3 vec = Vector3.forward + transform.position;
        if (battery.start_cnt <= 0 && battery.cnt % battery.shotInterval == 0) {
            var turretAngle = transform.GetComponentInChildren<FixedBatteryTurret>().targetAngle;
            var bullet1 = Instantiate(battery.BulletPrefab, vec, Quaternion.Euler(0, 0, turretAngle)) as GameObject;
            //Debug.LogFormat("ang="+turretAngle);
            bullet1.GetComponent<EnemyBullet>().Create(5);
        }
    }

    // Update is called once per frame
    /// <summary>
    /// 
    /// </summary>
    void Update() {
        for (int i = 0; i < transform.childCount; i++) {
            Debug.LogFormat("{0}", transform.GetChild(i).gameObject.name);
        }
        currentTurretAngle = transform.localEulerAngles.z;
        enemyPos = turret.transform.position;
        playerPos = player.transform.position;
        targetAngle = TurretRotate(turretNumber, enemyPos, playerPos);
        RotaingTurret();
        rotZ = Rad2Deg(transform.rotation.z);
    }
    void RotaingTurret() {
        Shot(GetComponentInParent<FixedBattery>().attack);

        turretQuater.eulerAngles = new Vector3(0, 0, targetAngle);

        playerQuater.eulerAngles = new Vector3(0, 0, Rad2Deg(transform.rotation.z));
        transform.rotation = Quaternion.Slerp(turretQuater, playerQuater, 0.001f * Time.deltaTime);
    }

    float TurretRotate(int turretNum, Vector3 turretPos, Vector3 playerPos) {
        float rad = 180.0f;
        //f (turretNumber == 1) {
        Vector3 direction = playerPos - turretPos;
        direction = direction / direction.magnitude;  // 後の計算のために大きさを1に統一
        rad = Mathf.Atan2(direction.x, direction.y);
        //}
        return -Rad2Deg(rad);
    }

    Vector3 SetTurretPos(int turretNumber) {
        Vector3 vec = new Vector3(0, 0, 0);
        //Debug.LogFormat("{0}",transform.GetChild(0).name);
        //if (transform.GetChild(0).name)

        //}
        return vec;
    }

    float Rad2Deg(float rad) {
        return targetAngle = rad * 180 / Mathf.PI;
    }
}