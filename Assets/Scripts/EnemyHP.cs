using UnityEngine;
using UnityEngine.UI;

public class EnemyHP : MonoBehaviour {
    public Text textHP;
    public GameObject EnemyObj;
    public string type;
    // Use this for initialization
    void Start() {
        type = EnemyObj.tag;
        switch (type) {
            case "Zako1": {
                    var hp = EnemyObj.GetComponent<Zako1>().hp;
                    //textHP.text = hp.ToString();
                    break;
                }
            case "Zako2": {
                    //var hp = EnemyObj.GetComponent<Zako2>().hp;
                    //textHP.text = hp.ToString();
                    break;
                }
            case "Tank1": {
                    var hp = EnemyObj.GetComponent<Tank1>().hp;
                    //textHP.text = hp.ToString();
                    break;
                }
            case "AircraftCarrier": {
                    var hp = EnemyObj.GetComponent<AircraftCarrier>().hp;
                    break;
                }
            case "FixedBattery": {
                    var hp = EnemyObj.GetComponent<FixedBattery>().hp;
                    //textHP.text = hp.ToString();
                    break;
                }
        }
    }

    // Update is called once per frame
    void Update() {
        var hp = 0;
        switch (type) {
            case "Zako1": {
                    hp = EnemyObj.GetComponent<Zako1>().hp;
                    break;
                }
            //case "Zako2": {
            //        var hp = EnemyObj.GetComponent<Zako2>().hp;
            //        break;
            //   }
            case "Tank1": {
                    hp = EnemyObj.GetComponent<Tank1>().hp;
                    break;
                }
            case "AircraftCarrier": {
                    hp = EnemyObj.GetComponent<AircraftCarrier>().hp;
                    break;
                }
            case "FixedBattery": {
                    hp = EnemyObj.GetComponent<FixedBattery>().hp;
                    //textHP.text = hp.ToString();
                    break;
                }
        }
        //textHP.text = hp.ToString();
    }
}
