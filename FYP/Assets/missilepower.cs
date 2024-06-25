using System.Collections;
using UnityEngine;

public class MissileLauncher : MonoBehaviour
{
    public GameObject missilePrefab;
    public float launchInterval = 1f; // Interval between missile launches
    public Transform playerAttachPoint; // The point on the player where the launcher should attach
    public Vector3 launcherOffset = new Vector3(5f, -0.5f, 5f); // Offset for the launcher's position relative to the playerAttachPoint
    private bool isActivated = false;
    private Transform player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isActivated && collision.CompareTag("Player"))
        {
            isActivated = true;
            player = collision.transform;
            AttachToPlayer();
            StartCoroutine(LaunchMissiles());
        }
    }

    private void AttachToPlayer()
    {
        if (playerAttachPoint == null)
        {
            playerAttachPoint = player; // Default to attaching to the player's transform
        }
        transform.SetParent(playerAttachPoint);
        transform.localPosition = launcherOffset; // Use the launcherOffset to position the launcher
    }

    private IEnumerator LaunchMissiles()
    {
        while (isActivated)
        {
            LaunchMissile();
            yield return new WaitForSeconds(launchInterval);
        }
    }

    private void LaunchMissile()
    {
        GameObject missile = Instantiate(missilePrefab, transform.position, Quaternion.identity);
        Missile missileScript = missile.GetComponent<Missile>();
        if (missileScript != null)
        {
            missileScript.Activate();
        }
    }
}