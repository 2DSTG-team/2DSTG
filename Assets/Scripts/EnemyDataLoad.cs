using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class EnemyDataLoad : MonoBehaviour {
    const int kEnemyAll = 2;
    public bool isDebug = false;
    public bool isLoad = true;
    enum EnemyNumber {
        Zako1,
        Tank,
        FixedTurret,
    }

    public class EnemyData_t {
        public EnemyData_t(int freq, string name, int attack) {
            this.freq = freq;
            this.name = name;
            this.attack = attack;
        }
        public int freq;
        public string name;
        public int attack;
    };
    public List<EnemyData_t> enemyData;

    // Use this for initialization
    void Start() {
        enemyData = new List<EnemyData_t>();
        if (isLoad) {
            Load("enemyData");
        } else {
            Save(@"Assets/Resources/enemyData.csv");
        }
        //if (Load("")) {
        //}
        DontDestroyOnLoad(gameObject);
        for (int i = 0; i < enemyData.Count; i++) {
            Debug.LogFormat("{0},{1},{2}", enemyData[i].attack, enemyData[i].freq, enemyData[i].name);
        }
    }

    void enemyAddList(int freq, string name, int attack) {
        enemyData.Add(new EnemyData_t(freq, name, attack));
    }

    // Update is called once per frame
    void Update() {

    }

    bool Save(string filePath) {
        enemyAddList(80, "Zako1", 20);
        enemyAddList(300, "Tank", 20);
        enemyAddList(600, "FixedTurret", 3);

        FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
        Encoding shiftJis = Encoding.GetEncoding("SHIFT_JIS");
        StreamWriter sw = new StreamWriter(fs, shiftJis);
        foreach (var enemy in enemyData) {
            sw.WriteLine("{0},{1},{2}", enemy.name, enemy.freq, enemy.attack);
        }
        sw.Close();
        return true;
    }

    bool Load(string filePath) {
        for (int i = 0; i < enemyData.Count; i++) {
            enemyData.RemoveAt(i);
        }
        string EnemyCSV = ((TextAsset)Resources.Load(filePath, typeof(TextAsset))).text;

        string[] EnemyList = EnemyCSV.Split('\n');
        var header = EnemyList[0].Split(',');
        for (int i = 0; i < kEnemyAll; i++) {
            var EnemyDataCol = EnemyList[i + 1].Split(',');//ヘッダーを飛ばす
            //Debug.LogWarningFormat("{0}",EnemyDataCol);
            string name = EnemyDataCol[0];
            int freq = int.Parse(EnemyDataCol[1]);
            int attack = int.Parse(EnemyDataCol[2]);
            enemyAddList(freq, name, attack);

            if (isDebug) {
                foreach (var enemy in enemyData) {
                    Debug.LogFormat("{0},{1}",enemy.name,enemy.attack);
                }
            }
        }
        return true;
    }
}