using UnityEngine;
using System.Collections;

public class AircraftCarrierTurret : EnemyTurretBase {
    float speed = 0.5f;
    public GameObject player;
    public GameObject enemy;
    public Quaternion playerQ;
    public Quaternion enemyQ;
    AircraftCarrier aircraftCarrier;

    public float targetAngle;
    public float currentTurretAngle;
    float rotZ;

    float x, y;
    bool isOn;
    //bool isFinishRotate;

    private Vector3 enemyPos, playerPos;
    // Use this for initialization
    void Start() {
       aircraftCarrier= GetComponentInParent<AircraftCarrier>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Shot() {
        Vector3 vec = Vector3.forward + transform.position;
        if (aircraftCarrier.start_cnt <= 0 && aircraftCarrier.cnt % aircraftCarrier.shotInterval == 0) {
            var turretAngle = transform.GetComponentInChildren<AircraftCarrierTurret>().targetAngle;
            var bullet1 = Instantiate(aircraftCarrier.BulletPrefab, vec, Quaternion.Euler(0, 0, turretAngle)) as GameObject;
            //Debug.LogFormat("ang="+turretAngle);
            bullet1.GetComponent<EnemyBullet>().Create();
        }
    }

    // Update is called once per frame
    void Update() {
        currentTurretAngle = transform.localEulerAngles.z;
        enemyPos = enemy.transform.position;
        playerPos = player.transform.position;
        targetAngle = TurretRotate(enemyPos, playerPos);
        //Debug.LogWarningFormat("targetZ = {0},{1}",targetAngle,transform.localEulerAngles);
        RotaingTurret();
        rotZ = Rad2Deg(transform.rotation.z);
    }
    void RotaingTurret() {
        if ((targetAngle >= 90.0f && targetAngle <= 180.0f) ||
                (targetAngle >= -180.0f && targetAngle <= -90.0f)) {
            Shot();

            enemyQ.eulerAngles = new Vector3(0, 0, targetAngle);

            playerQ.eulerAngles = new Vector3(0, 0, Rad2Deg(transform.rotation.z));
            transform.rotation = Quaternion.Slerp(enemyQ, playerQ, 0.001f * Time.deltaTime);
        } else {
            transform.rotation = Quaternion.Euler(0, 0, 180.0f);
        }
        /*if ((targetAngle >= 90.0f && targetAngle <= 180.0f)){
               
        }*/
    }

    float TurretRotate(Vector3 enemy, Vector3 player) {
        Vector3 direction = player - enemy;
        direction = direction / direction.magnitude;  // 後の計算のために大きさを1に統一
        float rad = Mathf.Atan2(direction.x, direction.y);
        return -Rad2Deg(rad);
    }
}
