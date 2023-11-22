using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveRandomly : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    NavMeshPath path;
    public float timeForNewPath;
    bool inCoRoutine;
    Vector3 target;
    bool validPath;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
    }

    private void Update()
    {
        if (!inCoRoutine)
        {
            StartCoroutine(DoSomething());
        }
    }

    Vector3 getNewRandomPosition() 
    {
        float x = Random.Range(-20, 20);
        float z = Random.Range(-20, 20);

        Vector3 pos = new Vector3(x, 0, z);
        return pos;
    }

    IEnumerator DoSomething() 
    {
        inCoRoutine = true;
        yield return new WaitForSeconds(timeForNewPath);
        getNewPath();
        validPath = navMeshAgent.CalculatePath(target, path);
        if (!validPath)
        {
            Debug.Log("Geçersiz yüzey bulundu");
        }
        while (!validPath)
        {
            yield return new WaitForSeconds(0.01f);
            getNewPath();
            validPath = navMeshAgent.CalculatePath(target, path);
        }
        inCoRoutine = false;
    }

    void getNewPath() 
    {
        target = getNewRandomPosition();
        navMeshAgent.SetDestination(target);
    }
}
