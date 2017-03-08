using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class EnemyData : MonoBehaviour {
    const int kEnemyAll = 4;
    public bool isDebug = false;
    public bool isLoad = true;

    public GameObject enemyController;
    EnemyController enemyControll;

    // Use this for initialization
    void Start() {
        //enemyData = new List<EnemyData_t>();
        enemyControll = enemyController.GetComponent<EnemyController>();
        if (isLoad) {
            Load("enemyData");
        } else {
            Save(@"Assets/Resources/enemyData.csv");
        }
        DontDestroyOnLoad(gameObject);
        //for (int i = 0; i < enemyControll.enemyData.Count; i++) {
        //    Debug.LogFormat("{0},{1},{2}", enemyControll.enemyData[i].attack, enemyControll.enemyData[i].freq, enemyControll.enemyData[i].name);
        //}
    }

    // Update is called once per frame
    void Update() {

    }

    bool Save(string filePath) {
        //enemyControll.EnemyAddList(80, "Zako1", 20);
        //enemyControll.EnemyAddList(80, "Zako2", 20);
        //enemyControll.EnemyAddList(300, "Tank", 20);
        //enemyControll.EnemyAddList(300, "AircraftCarrier", 20);
        //enemyControll.EnemyAddList(600, "FixedTurret", 3);

        FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
        Encoding shiftJis = Encoding.GetEncoding("SHIFT_JIS");
        StreamWriter sw = new StreamWriter(fs, shiftJis);
        //foreach (var enemy in enemyControll.Freq) {
        //    sw.WriteLine("{0},{1},{2}", enemy.name, enemy.freq, enemy.attack);
        //}
        sw.Close();
        return true;
    }

    bool Load(string filePath) {
        //if (enemyControll.enemyData != null) {
        //    for (int i = 0; i < enemyControll.enemyData.Count; i++) {
        //        enemyControll.enemyData.RemoveAt(i);
        //    }
        //}
        
        string EnemyCSV = ((TextAsset)Resources.Load(filePath, typeof(TextAsset))).text;

        string[] EnemyList = EnemyCSV.Split('\n');
        var header = EnemyList[0].Split(',');
        for (int i = 0; i < kEnemyAll; i++) {
            var EnemyDataCol = EnemyList[i + 1].Split(',');//ヘッダーを飛ばす
            //Debug.LogWarningFormat("{0}",EnemyDataCol);
            string name = EnemyDataCol[0];
            enemyControll.Freq[name] = int.Parse(EnemyDataCol[1]);
            enemyControll.Attack[name] = int.Parse(EnemyDataCol[2]);
            //enemyControll.Add(freq, name, attack);


            if (isDebug) {
                //foreach (var enemy in enemyControll.enemyData) {
                //    Debug.LogFormat("{0},{1}",enemy.name,enemy.attack);
                //}
            }
        }
        return true;
    }
}



//void EnemyAddList(int freq, string name, int attack) {
//    enemyData.Add(new EnemyData_t(freq, name, attack));
//}

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