﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour { 
    public static TurretAI Instance { get; private set; } //-Instancia del script

    [Header("Object Pool Settings")]
    [SerializeField] int poolSize = 10;  //-Tamaño de la cantidad de proyectiles
    [SerializeField] List<GameObject> bulletPool; //-Se guardan los proyectiles
    public enum TurretType
    {
        Single = 1,
        Dual = 2,
        Catapult = 3,
    }
    
    public GameObject currentTarget;
    public Transform turreyHead;

    public float attackDist = 10.0f;
    public float attackDamage;
    public float shootCoolDown;
    private float timer;
    public float loockSpeed;

    public Vector3 randomRot;
    public Animator animator;

    [Header("[Turret Type]")]
    public TurretType turretType = TurretType.Single;
    
    public Transform muzzleMain;
    public Transform muzzleSub;
    public GameObject bullet;
    private bool shootLeft = true;

    private Transform lockOnPos;


    void Start () {

        if (Instance == null)
        {
            Instance = this;
        }
       

        InvokeRepeating("ChackForTarget", 0, 0.5f);

        for (int i = 0; i < poolSize; i++) //-Crea las balas, las desactiva y las agrega a la lista
        {
            GameObject newBullet = Instantiate(bullet); 
            newBullet.SetActive(false); 
            bulletPool.Add(newBullet);   
        }

        if (transform.GetChild(0).GetComponent<Animator>())
        {
            animator = transform.GetChild(0).GetComponent<Animator>();
        }

        randomRot = new Vector3(0, Random.Range(0, 359), 0);
    }
	
	void Update () {
        if (currentTarget != null)
        {
            FollowTarget();
            if (Instance == null)
            {
                Instance = this;
            }
            float currentTargetDist = Vector3.Distance(transform.position, currentTarget.transform.position);
            if (currentTargetDist > attackDist)
            {
                currentTarget = null;
            }
        }
        else
        {
            IdleRitate();
        }

        
    }

    private void ChackForTarget()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, attackDist);
        float distAway = Mathf.Infinity;

        for (int i = 0; i < colls.Length; i++)
        {
            if (colls[i].tag == "Player")
            {
                float dist = Vector3.Distance(transform.position, colls[i].transform.position);
                if (dist < distAway)
                {
                    currentTarget = colls[i].gameObject;
                    distAway = dist;
                }
            }
        }
    }

    private void FollowTarget() //todo : smooth rotate
    {
        Vector3 targetDir = currentTarget.transform.position - turreyHead.position;
        targetDir.y = 0;
        if (turretType == TurretType.Single)
        {
            turreyHead.forward = targetDir;
            
        }
        else
        {
            turreyHead.transform.rotation = Quaternion.RotateTowards(turreyHead.rotation, Quaternion.LookRotation(targetDir), loockSpeed * Time.deltaTime);
        }
    }

    
    Vector3 CalculateVelocity(Vector3 target, Vector3 origen, float time)
    {
        Vector3 distance = target - origen;
        Vector3 distanceXZ = distance;
        distanceXZ.y = 0;

        float Sy = distance.y;
        float Sxz = distanceXZ.magnitude;

        float Vxz = Sxz / time;
        float Vy = Sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        Vector3 result = distanceXZ.normalized;
        result *= Vxz;
        result.y = Vy;

        return result;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDist);
    }

    public void IdleRitate()
    {
        bool refreshRandom = false;
        
        if (turreyHead.rotation != Quaternion.Euler(randomRot))
        {
            turreyHead.rotation = Quaternion.RotateTowards(turreyHead.transform.rotation, Quaternion.Euler(randomRot), loockSpeed * Time.deltaTime * 0.2f);
        }
        else
        {
            refreshRandom = true;

            if (refreshRandom)
            {

                int randomAngle = Random.Range(0, 359);
                randomRot = new Vector3(0, randomAngle, 0);
                refreshRandom = false;
            }
        }
    }

    public void Shoot(GameObject go) //-Pide una bala, la activa y la posiciona para ser disparada
    {
      
        GameObject bulletToShoot = GetBulletFromPool();

        if (turretType == TurretType.Single)
        {
            bulletToShoot.transform.position = muzzleMain.position;
            bulletToShoot.transform.rotation = muzzleMain.rotation;
            bulletToShoot.SetActive(true);  

            Projectile projectile = bulletToShoot.GetComponent<Projectile>();
            projectile.target = go.transform;
        }
    }
    private GameObject GetBulletFromPool()//-Busca una bala desactivada en la lista y si no hay crea una y la agrega
    {
        foreach (GameObject bullet in bulletPool)
        {
            if (!bullet.activeInHierarchy)
            {
                return bullet;
            }
        }

        GameObject newBullet = Instantiate(bullet);
        newBullet.SetActive(false);
        bulletPool.Add(newBullet);
        return newBullet;
    }
}
