using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASDMovement : MonoBehaviour {

	public float speed = 20f;

    void Update()
    {
        MovePlayer();
        ShootTurret();
    }
    void MovePlayer() //-Se mueve el movimiento del jugador en un método para simplificar el código
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical);

        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }
    void ShootTurret()
    {
        if (Input.GetMouseButtonDown(0))//-Se utiliza la instancia de TurretAI para llamar a Shoot
        {
            TurretAI.Instance.Shoot(TurretAI.Instance.currentTarget);
        }
    }
}
