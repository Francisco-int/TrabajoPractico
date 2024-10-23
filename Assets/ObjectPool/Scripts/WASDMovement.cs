﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASDMovement : MonoBehaviour {

	public float speed = 20f;

    void Update () {
        Vector3 pos = transform.position;

        if (Input.GetKey ("w")) {
            pos.z += speed * Time.deltaTime;
        }

        if (Input.GetKey ("s")) {
            pos.z -= speed * Time.deltaTime;
        }

        if (Input.GetKey ("d")) {
            pos.x += speed * Time.deltaTime;
        }

        if (Input.GetKey ("a")) {
            pos.x -= speed * Time.deltaTime;
        }
        if (Input.GetMouseButtonDown(0))//Se utiliza la instancia de TurretAI para llamar a Shoot
        {
            TurretAI.Instance.Shoot(TurretAI.Instance.currentTarget);
            Debug.Log(TurretAI.Instance);
        }

        transform.position = pos;
    }
}
