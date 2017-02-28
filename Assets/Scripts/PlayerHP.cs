using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour {
    public Text textHP;
    public GameObject PlayerObj;
    public int hp;
    // Use this for initialization
    void Start() {
        hp = PlayerObj.GetComponent<Player>().HP;
        textHP.text = hp.ToString();
        //Debug.LogErrorFormat("passed");
    }

    // Update is called once per frame
    void Update() {
        hp = PlayerObj.GetComponent<Player>().HP;
        textHP.text = hp.ToString();
    }
}
