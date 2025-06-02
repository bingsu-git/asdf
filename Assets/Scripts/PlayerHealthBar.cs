using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Image fillImage;       // ���� Fill �̹��� (Image Type: Filled)
    public PlayerHealth player;   // PlayerHealth ��ũ��Ʈ ����

    void Update()
    {
        float ratio = (float)player.CurrentHealth / player.maxHealth;
        fillImage.fillAmount = ratio;
    }
}
