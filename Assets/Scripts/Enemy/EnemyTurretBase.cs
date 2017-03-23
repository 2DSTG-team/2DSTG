using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurretBase : MonoBehaviour {
    protected Player player;
	protected float targetAngle, currentTurretAngle;
    protected Quaternion playerQ, enemyQ;

    protected Vector3 enemyPos, playerPos;

	protected float Rad2Deg(float rad) {
		return rad * 180 / Mathf.PI;
	}

    public virtual void Start() {
        
    }

}
