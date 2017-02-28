using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
    public static int kTankAllCount = 2;
    int cnt;
    public GameObject PlayerObj;
    string enemyType;
    Player player;

    [System.Serializable]
    public class Prefabs {
        public Prefabs() {
            tank = new GameObject[kTankAllCount];
        }
        public GameObject Zako1, Zako2,FixedTurret;
        public GameObject[] tank;

    }
    public Prefabs prefabs = new Prefabs();

    [System.Serializable]
    public class Freq {
        public Freq() {
            zako1 = 80;
            zako2 = 80;
            tank = 300;
            fixedTurret = 600;
        }
        public int zako1, zako2;
        public int tank;
        public int fixedTurret;
    }
    Freq freq = new Freq();

    /// <summary>
    /// 敵が出現するときにどのような位置に出現するか
    /// </summary>
    class EncountPosType {
        public GameObject Random(GameObject prefab, float posX_1, float posX_2, float posY = 3.5f) {
            var obj = Instantiate(prefab, new Vector2(UnityEngine.Random.Range(posX_1, posX_2), posY), Quaternion.identity) as GameObject;
            return obj;
        }
        public GameObject Fixing(GameObject prefab, float posX, float posY = 3.5f) {
            var obj = Instantiate(prefab, new Vector2(posX, posY), Quaternion.identity) as GameObject;
            return obj;
        }
    }
    EncountPosType encountPosType = new EncountPosType();

    // Use this for initialization
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        cnt = 0;
    }

    // Update is called once per frame
    void Update() {
        freq_set();
        cnt++;
        EncountZako1(freq.zako1,90,20);
        //EncountZako2(freq.zako2);
        EncountTank(freq.tank,0.5f,kTankAllCount,100,20);
        if (player.power >= 2) {
            EncountFixedBattery(freq.fixedTurret, 0.2f, 30,3);
        }
    }
    #region EnemyEncout Func
    void EncountZako1(int freq, int shotInterval, int atk,float speed = 1.0f) {
        var tempHP = 0;
        if (cnt % freq == 0) {
            var enemy = encountPosType.Random(prefabs.Zako1, -3.5f, 3.5f);
            var mNum = Random.Range(0, 1 + 1);
            bool isShot = true;
            if (mNum == 0) {
                isShot = false;
            } else if (mNum == 1) {
                isShot = true;
            }
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
            enemy.GetComponent<Zako1>().Create("Zako1", speed, tempHP, atk,shotInterval, isShot);
        }
    }
    /*void EncountZako2(int freq, float speed = 1.2f) {
        var tempHP = 0;
        if (cnt % freq == 0) {
            var number = Random.Range(0, 1 + 1);
            var enemy = encountPosType.Random(prefabs.Zako2, -3.5f, 3.5f);
            var mNum = Random.Range(0, 1 + 1);
            bool isShot = true;
            if (mNum == 0) {
                isShot = false;
            } else if (mNum == 1) {
                isShot = true;
            }
            switch (PlayerObj.GetComponent<Player>().power) {
                case 1:
                    tempHP = 80;
                    break;
                case 2:
                    tempHP = 150;
                    break;
                case 3:
                    tempHP = 300;
                    break;
            }
            //enemy.GetComponent<Zako2>().Create("Zako2", speed, tempHP,70, isShot);
            Debug.LogFormat("not defined");
        }
    }
    */

    void EncountTank(int freq, float speed,int tank_num,int shotInterval,int atk) {
        var num = Random.Range(0, tank_num);
        if (num > kTankAllCount) {
            Debug.LogFormat("idex is Over " + num + " at 117");
        }
        var tempHP = 0;
        if (cnt % freq == 0) {
            var enemy1 = encountPosType.Fixing(prefabs.tank[num], 2);
            var enemy2 = encountPosType.Fixing(prefabs.tank[num], -2);

            switch (player.power) {
                case 1:
                    tempHP = 150;
                    break;
                case 2:
                    tempHP = 300;
                    break;
                case 3:
                    tempHP = 500;
                    break;
            }
            //Debug.LogFormat("tank["+num+"]="+prefabs.tank[num]);
            //enemy1.GetComponent<Tank1>().Create("Tank" + num, speed, tempHP, atk, shotInterval);
            enemy1.GetComponent<Tank1>().Create("Created Object");
            enemy2.GetComponent<Tank1>().Create("Tank" + num, speed, tempHP, atk, shotInterval);
        }
    }

    void EncountFixedBattery(int freq, float speed, int shotInterval,int atk) {
        var tempHP = 0;
        if (cnt % freq == 0) {
            var enemy1 = encountPosType.Fixing(prefabs.FixedTurret, 0);

            switch (player.power) {
                case 1:
                    tempHP = 700;
                    break;
                case 2:
                    tempHP = 2000;
                    break;
                case 3:
                    tempHP = 3000;
                    break;
            }
            enemy1.GetComponent<FixedBattery>().Create("FixedBattery", speed, tempHP,atk, shotInterval);
        }
    }

    #endregion


    void freq_set() {
        switch (player.power) {
            case 1:
                freq.zako1 = 80;
                freq.tank = 330;
                break;
            case 2:
                freq.zako1 = 85;
                freq.tank = 280;
                break;
                case 3:
                freq.zako1 = 65;
                freq.tank = 220;
                break;
        }
    }
}

