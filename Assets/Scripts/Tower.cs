using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower : MonoBehaviour
{
    Transform top;
    [SerializeField] GameObject gun;
    public Transform objectToPan;

    [SerializeField] float timeSchedule = 1.5f;
    [SerializeField] float towerRange = 30f;

    //public GameObject closest;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = objectToPan.position;
        var emission = gun.GetComponent<ParticleSystem>().emission;
        emission.rateOverTime = timeSchedule;
        
        
    }

    private void Awake()
    {
        top = transform.Find("Tower A Top");


    }

    // Update is called once per frame
    void Update()
    {
        LookAtEnemy(GetAim());
    }

    void LookAtEnemy(GameObject enemy)
    {
        if (enemy != null)
        {
            top.LookAt(enemy.transform.position);
            gun.SetActive(true);

        }
        else
        {
            gun.SetActive(false);
        }
    }


    GameObject GetAim() //TODO Stop search of closest out of range (?)
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy")
                                .Where(enemy => DistanceTo(enemy) <= towerRange)
                                .ToList();
        if (enemies.Count > 0)
        {
            return GetClosestEnemy(enemies);
        }
        return null;
    }

    private GameObject GetClosestEnemy(List<GameObject> enemies)
    {
        return enemies.Aggregate((enemy1, enemy2) => DistanceTo(enemy1) <= DistanceTo(enemy2) ? enemy1 : enemy2);
    }

    float DistanceTo(GameObject aim)
    {
        return Vector3.Distance(transform.position, aim.transform.position);
    }

}
