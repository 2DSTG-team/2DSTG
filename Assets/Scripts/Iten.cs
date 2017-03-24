using UnityEngine;
using System.Collections;

public class Iten : MonoBehaviour {
    string type;
    Player player;
    GameObject PlayerObj;
	// Use this for initialization
	void Start () {
        PlayerObj = GameObject.FindGameObjectWithTag("Player");
        player = PlayerObj.GetComponent<Player>();
	}

	
	// Update is called once per frame
	void Update () {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0,-50.0f*Time.deltaTime);
	}

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag=="Player") {
            player.HP += 50;
            Destroy(gameObject);
        }
    }
}
