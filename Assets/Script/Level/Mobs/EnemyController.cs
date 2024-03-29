using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI;
using UnityEngine.AI;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using Unity.Burst.CompilerServices;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    Transform anchor;
    [SerializeField]
    float maxDistanceFromAnchor;
    [SerializeField]
    float maxChaseDistance;
    [SerializeField]
    bool chassing = false;
    
    [SerializeField]
    int damage;
    private NavMeshAgent agent;
    [SerializeField]
    List<Transform> patrolPoints = new List<Transform>();

    [SerializeField]
    Transform deadZone;
    public int value;
    
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        
        StartCoroutine(PatrolCorout());
    }

    
    private void OnTriggerEnter(Collider other)
    {

        if (!other.gameObject.CompareTag("Player")) return;
        playerController player = other.GetComponent<playerController>();
        chassing = true;
        StopCoroutine(PatrolCorout());
        anchor.position = transform.position;
        StartCoroutine(ChasePlayerCorout(player));
        
    }

    IEnumerator ChasePlayerCorout(playerController player)
    {
        while (Vector3.Distance(transform.position, player.transform.position) < maxChaseDistance)
        {
            if (IsPlayerOnTop())
            {
                TankingDamage();
                yield break;
            }
            if (Vector3.Distance(transform.position, anchor.position) > maxDistanceFromAnchor)
            {
                
                break;
            }
            //transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
            agent.destination = player.transform.position;
            yield return null;
        }
        chassing = false;
        StopCoroutine(ChasePlayerCorout(player));
        StartCoroutine(PatrolCorout());
        yield return null;
    }

    int indexPatrol = 0;
    IEnumerator PatrolCorout()
    {

        while (!chassing) 
        {
            if (Vector3.Distance(transform.position, patrolPoints[indexPatrol].position) < 1)
            {
                indexPatrol++;
                if (indexPatrol >= patrolPoints.Count) indexPatrol = 0;
                agent.destination = patrolPoints[indexPatrol].position;

            }
            else agent.destination = patrolPoints[indexPatrol].position;
            yield return null;
        } 
        

    }

    private void TankingDamage()
    {
        AudioManager.instance.ToPlaySound(AudioManager.instance.bonk);
        GameManager.SCORE += value; 
        GameManager.instance.killCount++;
        Destroy(gameObject);
        
    }

    bool IsPlayerOnTop()
    {
        Vector3 playerPos = playerController.instance.transform.position;
        float playerHeight = playerPos.y;
        Vector3 deadZonePos = deadZone.position;
        float deadZoneHeight = deadZonePos.y;
        float heightDist = playerHeight - deadZoneHeight;
        if (heightDist < 0 || heightDist > 0.1f) return false;

        playerPos.y = 0;
        deadZonePos.y = 0;
        float dist = Vector3.Distance(deadZonePos, playerPos);
        if (dist > 0.5f) return false;
        return true;

    }
    
    
    

}
