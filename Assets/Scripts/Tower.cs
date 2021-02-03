using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower : MonoBehaviour
{
    Transform top;
    [SerializeField] GameObject gun;
    AudioSource laserShot;
    [SerializeField] Transform objectToPan;

    [SerializeField] float timeSchedule = 2f;
    //public GameObject closest;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = objectToPan.position;



        var emission = gun.GetComponent<ParticleSystem>().emission;
        emission.rateOverTime = timeSchedule;
        
        laserShot = GetComponent<AudioSource>();
        top = transform.Find("Tower A Top");
        
    }

    // Update is called once per frame
    void Update()
    {
        List<GameObject> enemies = GameObject.FindGameObjectsWithTag("Enemy").ToList();
        if (enemies.Count > 0)
        {
            var closest = enemies.First(
                enemy1 => DistanceTo(enemy1) - enemies.Min(enemy2 => DistanceTo(enemy2)) < Mathf.Epsilon);
            LookAtEnemy(closest);
        } else
        {
            LookAtEnemy(null);
        }


    }

    void LookAtEnemy(GameObject enemy)
    {
        var bulletsEmission = gun.GetComponent<ParticleSystem>().emission;
        if (enemy != null)
        {
            top.LookAt(enemy.transform.position);
            bulletsEmission.enabled = true;
            if (!laserShot.isPlaying)
            {
                laserShot.PlayScheduled(timeSchedule);
            }

        }
        else
        {
            bulletsEmission.enabled = false;
            laserShot.Stop();
        }
    }

    float DistanceTo(GameObject aim)
    {
        return Vector3.Distance(transform.position, aim.transform.position);
    }

}
