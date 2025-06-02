using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Image fillImage;       // 빨간 Fill 이미지 (Image Type: Filled)
    public PlayerHealth player;   // PlayerHealth 스크립트 연결

    void Update()
    {
        float ratio = (float)player.CurrentHealth / player.maxHealth;
        fillImage.fillAmount = ratio;
    }
}
