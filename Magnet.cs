using UnityEngine;

public class Magnet : MonoBehaviour
{
    public Transform character; // Karakter objesinin Transform bileşeni

    public float magnetDistance = 5f; // Mıknatısın etki mesafesi
    public float magnetSpeed = 20f; // Çekme hızı
    public float destroyDistance = 0.5f; // Yok olma mesafesi

    private bool magnetApplied = false; // Mıknatıs etkisi uygulandı mı?

    private void Awake()
    {
        character = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (!magnetApplied) // Mıknatıs etkisi daha önce uygulanmadıysa
        {
            Vector3 directionToCharacter = character.position - transform.position;
            float distance = directionToCharacter.magnitude;

            if (distance < magnetDistance)
            {
                if (distance < destroyDistance) // Eğer mesafe belirlediğimiz değerin altındaysa
                {
                    gameObject.SetActive(false); // Para objesini görünmez yap
                }
                else
                {
                    // Para objesini oyuncuya doğru yavaşça çek
                    Vector3 newPosition = Vector3.MoveTowards(transform.position, character.position, Time.deltaTime * magnetSpeed);
                    transform.position = newPosition;
                }
            }
        }
    }
}
