using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
        public GameObject Zako1, Zako2, FixedTurret;
        public GameObject[] tank;

    }
    public Prefabs prefabs = new Prefabs();

    enum EnemyNumber {
        Zako1,
        Tank,
        AircraftCarrier,
        FixedTurret,
    }

    //public class EnemyData_t {
    //    public EnemyData_t(int freq, string name, int attack) {
    //        this.freq = freq;
    //        this.name = name;
    //        this.attack = attack;
    //    }
    //    public int freq;
    //    public string name;
    //    public int attack;
    //};
    //public List<EnemyData_t> enemyData;

    //public void EnemyAddList(int freq, string name, int attack) {
    //    enemyData.Add(new EnemyData_t(freq, name, attack));
    //}

    public Dictionary<string, int> Freq = new Dictionary<string, int>();
    public Dictionary<string, int> Attack = new Dictionary<string, int>();

    public void AddEnemyData(string name,int freq,int attack) {
        Freq.Add(name, freq);
        Attack.Add(name, attack);
    }

    [System.Serializable]
    //public class Freq {
    //    public Freq() {
    //        zako1 = 80;
    //        zako2 = 80;
    //        tank = 300;
    //        fixedTurret = 600;
    //    }
    //    public int zako1, zako2;
    //    public int tank;
    //    public int fixedTurret;
    //}
    //Freq freq = new Freq();

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
        AddEnemyData("zako1", 80, 20);
        AddEnemyData("zako2", 80, 20);
        AddEnemyData("tank", 300, 20);
        AddEnemyData("aircraftCarrier", 300, 20);
        AddEnemyData("fixedTurret", 600, 3);

        //EnemyAddList(80, "Zako1", 20);
        //EnemyAddList(80, "Zako2", 20);
        //EnemyAddList(300, "Tank", 20);
        //EnemyAddList(300, "AircraftCarrier", 20);
        //EnemyAddList(600, "FixedTurret", 3);
    }

    // Update is called once per frame
    void Update() {
        //FreqSet();
        cnt++;
        EncountZako1(Freq["zako1"],90,Attack["zako1"]);
        //EncountZako2(freq.zako2);
        EncountTank(Freq["tank"],0.5f,kTankAllCount,100,Attack["tank"]);
        if (player.power >= 2) {
            EncountFixedBattery(Freq["fixedTurret"], 0.2f, 30,Attack["fixedTurret"]);
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


    void FreqSet() {
        switch (player.power) {
            case 1:
                Freq["zako1"] = 80;
                Freq["tank"] = 330;
                break;
            case 2:
                Freq["zako1"] = 70;
                Freq["tank"] = 280;
                break;
                case 3:
                Freq["zako1"] = 60;
                Freq["tank"] = 230;
                break;
        }
    }
}

