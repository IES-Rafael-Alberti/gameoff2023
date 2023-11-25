using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIScript : MonoBehaviour
{
    public GameObject healthImagePrefab;

    private Image[] healthImages = new Image[3];
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < healthImages.Length; i++)
        {
            GameObject newHI = Instantiate(
                healthImagePrefab, 
                transform.position + Vector3.right * i * 25, 
                Quaternion.identity);
            newHI.transform.parent = transform;
            healthImages[i] = newHI.GetComponent<Image>();
        }
        PlayerMovementPlatforming.OnHealthChange += UpdateHealth;
    }

    // Update is called once per frame
    public void UpdateHealth(int value)
    {
        for (int i = 0; i < healthImages.Length; i++)
        {
            if (i < value)
            {
                healthImages[i].color = new Color(1, 1, 1, 1f);
            } else
            {
                healthImages[i].color = new Color(0, 0, 0, 0.2f);
            }
        }

    }
}
