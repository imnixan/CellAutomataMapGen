using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpCounter : MonoBehaviour
{
    [SerializeField]
    private Image oilCounter,
        hpCounter,
        hpIcon,
        oilIcon;

    [SerializeField]
    private Color green,
        yellow,
        red;

    [SerializeField]
    AudioClip alarm,
        collectBonus;
    private SoundManager sm;
    private const float SecsToBurnOil = 6;
    private float hp,
        oil;
    private Movement player;

    public void Init()
    {
        player = GetComponent<Movement>();
        hp = 1f;
        oil = 1f;
        UpdateHP();
        UpdateOil();
        StartCoroutine(OilBurn());
        sm = GetComponent<SoundManager>();
    }

    private void UpdateHP()
    {
        hpCounter.fillAmount = hp;
        if (hp <= 0)
        {
            player.Dead();
        }
        UpdateColor(hp, hpCounter, hpIcon);
    }

    private void UpdateOil()
    {
        oilCounter.fillAmount = oil;
        if (oil <= 0)
        {
            player.Fall();
        }
        UpdateColor(oil, oilCounter, oilIcon);
    }

    protected void OnParticleCollision(GameObject other)
    {
        hp -= 0.2f;
        sm.PlaySound(alarm);
        UpdateHP();
    }

    private void UpdateColor(float counter, Image counterImage, Image icon)
    {
        if (counter > 0.7f)
        {
            counterImage.color = green;
        }
        else if (counter <= 0.4f)
        {
            counterImage.color = red;
        }
        else
        {
            counterImage.color = yellow;
        }

        icon.color = counterImage.color;
    }

    private IEnumerator OilBurn()
    {
        while (true)
        {
            yield return new WaitForSeconds(SecsToBurnOil);
            oil -= 0.1f;
            UpdateOil();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Oil"))
        {
            Destroy(other.gameObject);
            oil += 0.5f;
            if (oil > 1)
            {
                oil = 1;
            }
            sm.PlaySound(collectBonus);
            UpdateOil();
        }
        if (other.CompareTag("Repair"))
        {
            Destroy(other.gameObject);
            hp += 0.4f;
            if (hp > 1)
            {
                hp = 1;
            }
            UpdateHP();
            sm.PlaySound(collectBonus);
        }
        if (other.CompareTag("Ammo"))
        {
            sm.PlaySound(collectBonus);
        }
    }
}
