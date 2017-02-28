using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D other) {
        var targetTag = other.gameObject.tag;
        switch (targetTag) {
            case "Zako1":
                other.gameObject.GetComponent<Zako1>().HitBox("Normal");
                Destroy(gameObject);
                break;
            //case "Zako2":
            //    //other.gameObject.GetComponent<Zako2>().HitBox("Normal");
            //    Debug.LogFormat("not defined");
            //    break;
            case "Tank1":
                other.gameObject.GetComponent<Tank1>().HitBox("Normal");
                Destroy(gameObject);
                break;

            case "FixedBattery":
                other.gameObject.GetComponent<FixedBattery>().HitBox("Normal");
                Destroy(gameObject);
                break;
        }
    }
}