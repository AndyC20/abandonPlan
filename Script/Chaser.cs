using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chaser : MonoBehaviour
{    
    public float speed = 3.0f;
    public float obstacleRange = 5.0f;
    public Transform player;
    float fovDist = 35.0f;
    float fovAngle = 130.0f;
    public NavMeshAgent agent;

    [SerializeField] float health, maxHealth = 9999f;

    [SerializeField] healthBar hp;

    private void Awaken(){
        hp = GetComponentInChildren<healthBar>();
        hp.UpdateHealthBar(health, maxHealth);
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        health = maxHealth;
    }
    private void Update()
    {
        if(lockon(player)){
            chase();
        } 
        else
        {
            wander();
        }
    }

    void wander(){
        transform.Translate(0, 0, speed * Time.deltaTime);

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if(Physics.SphereCast(ray, 0.75f, out hit))
        {
            if(hit.distance < obstacleRange)
            {
                float angle = Random.Range(-110, 110);
                transform.Rotate(0, angle, 0);
            }
        }
    }

    void chase(){
        agent.destination = player.position;
        agent.speed = speed;
        }
    
    

    bool lockon(Transform player)
    {
        Vector3 direction = player.position - transform.position;
        float angle = Vector3.Angle(direction, this.transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(this.transform.position, direction, out hit) && hit.collider.gameObject.tag == "Player" && direction.magnitude < fovDist && angle < fovAngle){
            return true;
        }
        else{
            return false;
        }
    }

    public void Damage(float damageAmt)
    {
        health -= damageAmt;
        hp.UpdateHealthBar(health, maxHealth);
        if(health <= 0){
            Destroy(gameObject);
        }
    }
}


