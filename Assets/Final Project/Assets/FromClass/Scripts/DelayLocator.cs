using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayLocator : MonoBehaviour
{
    [SerializeField]
    private GameObject locator1;
    [SerializeField]
    private GameObject locator2;
    [SerializeField]
    private GameObject locator3;
    [SerializeField]
    private GameObject locator4;
    [SerializeField]
    private GameObject locator5;
    [SerializeField]
    private CinemachineVirtualCamera virtualCamera;

    private Vector3 FIXED_POS_1 = new Vector3(0.3f, 0, 34);
    private Vector3 FIXED_POS_2 = new Vector3(26.8f, 0, 15.1f);
    private Vector3 FIXED_POS_3 = new Vector3(8.5f, 0, -30.06f);
    private Vector3 FIXED_POS_4 = new Vector3(-23.5f, 0, -27.8f);
    private Vector3 FIXED_POS_5 = new Vector3(-21.62f, 0, 21.74f);

    // Start is called before the first frame update
    void Start()
    {
        locator1.transform.position = FIXED_POS_1;
        locator2.transform.position = FIXED_POS_2;
        locator3.transform.position = FIXED_POS_3;
        locator4.transform.position = FIXED_POS_4;
        locator5.transform.position = FIXED_POS_5;

        locator1.SetActive(false);
        locator2.SetActive(false);
        locator3.SetActive(false);
        locator4.SetActive(false);
        locator5.SetActive(false);

        StartCoroutine(DoSchedule());
    }
    
    IEnumerator DoSchedule()
    {
        virtualCamera.LookAt = locator1.transform;
        yield return new WaitForSeconds(2);
        SetLocatorActive(locator1);
        yield return new WaitForSeconds(10);
        virtualCamera.LookAt = locator2.transform;
        yield return new WaitForSeconds(2);
        SetLocatorActive(locator2);
        yield return new WaitForSeconds(10);
        virtualCamera.LookAt = locator3.transform;
        yield return new WaitForSeconds(2);
        SetLocatorActive(locator3);
        yield return new WaitForSeconds(10);
        virtualCamera.LookAt = locator4.transform;
        yield return new WaitForSeconds(2);
        SetLocatorActive(locator4);
        yield return new WaitForSeconds(10);
        virtualCamera.LookAt = locator5.transform;
        yield return new WaitForSeconds(2);
        SetLocatorActive(locator5);
    }

    private void SetLocatorActive( GameObject locator)
    {
        locator.SetActive(true);
    }
}
