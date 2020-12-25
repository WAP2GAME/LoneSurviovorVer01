using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchComponent : MonoBehaviour
{
    private Queue<GameObject> searchedObjs = new Queue<GameObject>(10);
    [SerializeField]
    private float recognitionDist = 20f; 
    [SerializeField]
    private float recognitionDegree = 180f; 
    [SerializeField]
    private int searchLayer = 8;
    internal Vector3 position;

    public Queue<GameObject> SearchedObjs
    {
        get => new Queue<GameObject>(searchedObjs);
    }


    private IEnumerator Search()
    {
        RaycastHit[] raycastedObjs = new RaycastHit[50];
        int targetLayer = 1 << searchLayer;
        while (true)
        {
            float searchRadius = 1f;
            Vector3 center = transform.position;
            searchedObjs.Clear();
            for (; GetDistanceTo(center) < recognitionDist; searchRadius *= 2)
            {
                center = center + transform.forward * searchRadius;
                int searchCnt = Physics.SphereCastNonAlloc(center, searchRadius, Vector3.one.normalized, raycastedObjs, 0.001f, targetLayer);
                int i = 0;
                for (; i < searchCnt && raycastedObjs[i].collider != null; i++)
                {
                    if (IsInPOV(raycastedObjs[i].transform.position))
                        searchedObjs.Enqueue(raycastedObjs[i].collider.gameObject);
                }
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    private bool IsInPOV(Vector3 pos)
    {
        var angleToPos = Vector3.Angle((pos - transform.position).normalized, transform.forward);
        return (Mathf.Abs(angleToPos) <= recognitionDegree / 2);
    }

    private float GetDistanceTo(Vector3 pos)
    {
        return Vector3.Distance(transform.position, pos);
    }

    private void OnEnable()
    {
        StartCoroutine(Search());
    }
}
