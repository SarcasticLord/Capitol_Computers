using UnityEngine;
using System.Collections;

public class EmployeeSpawner : MonoBehaviour
{

    public Transform EmpPoint;
    public GameObject Employee; 
    public GameObject EmployeeExist;

    public bool facingRight = true;

    public int employeeCount = 0;
    public int maxEmployees = 10;

    void Start()
    {
        StartCoroutine(EmployeeSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator EmployeeSpawn() 
    {
        while (true)
        {
            if (employeeCount < maxEmployees)
            {
                EmployeeExist = Instantiate(Employee, EmpPoint.position, facingRight ? EmpPoint.rotation : Quaternion.Euler(-90, 0, 0));
                employeeCount++;
            
            }
            yield return new WaitForSeconds(20f);  
        } 
    }
}
