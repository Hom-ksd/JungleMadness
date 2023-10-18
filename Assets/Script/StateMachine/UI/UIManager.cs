using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject damageTextPrefab;
    public GameObject healthTextPrefab;

    public Canvas gameCanvas;

    private void Awake()
    {

    }

    public void OnEnable()
    {
        CharactorEvents.characterDamaged += CharactorTookDamage;
        CharactorEvents.characterHealed += CharactorHealed;
    }
    public void OnDisable()
    {
        CharactorEvents.characterDamaged -= CharactorTookDamage;
        CharactorEvents.characterHealed -= CharactorHealed;
    }
    public void CharactorTookDamage(GameObject character,int damageReceived)
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();

        tmpText.text = damageReceived.ToString();
    }

    public void CharactorHealed(GameObject character,int healthRestored) 
    {
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate(healthTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();

        tmpText.text = healthRestored.ToString();
    }
}
