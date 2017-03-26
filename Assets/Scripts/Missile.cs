using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {
    float speed = 0.001f;
    public GameObject target;
    public Vector3 Pos;
	// Use this for initialization
	void Start () {
        Pos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (target) {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position), 3f);
            transform.position += transform.forward * speed;
        }
    }

    void Create(GameObject target) {
        this.target = target;
    }

    void hoge() {
        GameObject[] zako1 = GameObject.FindGameObjectsWithTag("Zako1");
        GameObject[] tank = GameObject.FindGameObjectsWithTag("Tank");
        foreach (var enemy in zako1) {
            float dis = Vector2.Distance(transform.position,enemy.transform.position);

        }
        foreach (var enemy in tank) {
            float dis = Vector2.Distance(transform.position, enemy.transform.position);

        }

        if (target) {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position), 3f);
            
            transform.position += transform.forward * speed;
        }
       

    }
}
