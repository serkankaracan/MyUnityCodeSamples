using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RadialObjectSelector : MonoBehaviour
{
    private GameObject[] RadialContainer; // Konteyner nesnelerinin referanslarını tutacak dizi
    private Vector3[] objectsPositions; // Konteyner nesnelerinin pozisyonlarını tutacak dizi
    private int containerCount; // Konteyner nesne sayısı
    private int currentIndex = 0; // Şu anda seçili olan nesnenin indeksi

    public float radius = 2f; // Daire yarıçapı
    public float minRadius = 1f; // Minimum yarıçap
    public float maxRadius = 5f; // Maksimum yarıçap
    public float rotSpeed = .5f; // Nesnelerin yer değiştirme hızı

    public Slider radiusSlider; // Yarıçapı ayarlamak için kaydırıcı nesnesi
    private TextMeshProUGUI radiusSliderText;

    private bool isChangingRadius = false;

    private void Awake()
    {
        radiusSliderText = radiusSlider.gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        containerCount = transform.childCount; // Konteyner nesne sayısını al
        objectsPositions = new Vector3[containerCount]; // Konteyner nesnelerinin pozisyonlarını tutacak dizi oluştur
        RadialContainer = new GameObject[containerCount]; // Konteyner nesnelerinin referanslarını tutacak dizi oluştur

        radiusSlider.onValueChanged.AddListener(delegate { SliderValueChangeCheck(); });
        radiusSlider.value = 1 / radius;
        radiusSliderText.SetText(radius.ToString());

        // Konteyner nesnelerinin referanslarını diziye ata
        for (int i = 0; i < containerCount; i++)
            RadialContainer[i] = transform.GetChild(i).gameObject;

        PlaceObjects(containerCount); // Nesneleri yerleştir
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            LeftPlaceObjectsDynamically();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            RightPlaceObjectsDynamically();
        }
    }

    private void PlaceObjects(int mContainerCount)
    {
        for (int i = 0; i < mContainerCount; i++)
        {
            float angle = (360f / mContainerCount) * i; // Açıyı hesapla
            Vector3 pos = CalculatePosition(angle); // Pozisyonu hesapla
            RadialContainer[i].transform.position = pos; // Konteyner nesnesinin pozisyonunu ayarla
            objectsPositions[i] = pos; // Pozisyonu kaydet
        }
    }

    private Vector3 CalculatePosition(float mAngle)
    {
        float x = Mathf.Cos(mAngle * Mathf.Deg2Rad) * radius; // x koordinatını hesapla
        float z = Mathf.Sin(mAngle * Mathf.Deg2Rad) * radius; // z koordinatını hesapla

        return new Vector3(x, 0f, z); // Yeni pozisyonu döndür
    }

    public void LeftPlaceObjectsDynamically()
    {
        currentIndex--; // Seçili nesneyi bir azalt

        if (currentIndex < 0)
            currentIndex = containerCount - 1;
        PlaceObjectsDynamically(); // Nesneleri güncelle
    }

    public void RightPlaceObjectsDynamically()
    {
        currentIndex++; // Seçili nesneyi bir artır

        if (currentIndex >= containerCount)
            currentIndex = 0;
        PlaceObjectsDynamically(); // Nesneleri güncelle
    }

    private void PlaceObjectsDynamically()
    {
        for (int i = 0; i < containerCount; i++)
        {
            int index = (currentIndex + i) % containerCount; // Hedef pozisyonun indeksini hesapla
            StartCoroutine(PlaceSmooth(RadialContainer[i].transform, objectsPositions[index])); // Nesneyi hedef pozisyona doğru hareket ettir
        }
    }

    private IEnumerator PlaceSmooth(Transform targetTransform, Vector3 targetPosition)
    {
        Vector3 startingPosition = targetTransform.position; // Başlangıç pozisyonunu al
        float elapsedTime = 0f; // Geçen süreyi sıfırla

        float initialAngle = Mathf.Atan2(startingPosition.z, startingPosition.x) * Mathf.Rad2Deg; // Başlangıç açısını hesapla
        float targetAngle = Mathf.Atan2(targetPosition.z, targetPosition.x) * Mathf.Rad2Deg; // Hedef açıyı hesapla

        float initialRadius = startingPosition.magnitude; // Başlangıç yarıçapını hesapla
        float targetRadius = targetPosition.magnitude; // Hedef yarıçapı hesapla

        while (elapsedTime < rotSpeed)
        {
            elapsedTime += Time.deltaTime; // Geçen süreyi güncelle
            float percentCompletion = elapsedTime / rotSpeed; // Hareket tamamlanma yüzdesini hesapla

            float currentAngle = Mathf.LerpAngle(initialAngle, targetAngle, percentCompletion); // Geçerli açıyı hesapla
            float currentRadius = Mathf.Lerp(initialRadius, targetRadius, percentCompletion); // Geçerli yarıçapı hesapla

            float x = Mathf.Cos(currentAngle * Mathf.Deg2Rad) * currentRadius; // x koordinatını hesapla
            float z = Mathf.Sin(currentAngle * Mathf.Deg2Rad) * currentRadius; // z koordinatını hesapla
            Vector3 currentPosition = new Vector3(x, 0f, z); // Yeni pozisyonu oluştur

            targetTransform.position = currentPosition; // Hedef pozisyonu güncelle

            yield return null;
        }

        targetTransform.position = targetPosition; // Hedef pozisyonunu son konuma ayarla
    }

    public void SliderValueChangeCheck()
    {
        float mRadius = Mathf.Lerp(minRadius, maxRadius, radiusSlider.value); // Kaydırıcının değerine göre yarıçapı hesapla
        radiusSliderText.SetText(mRadius.ToString());
        //isChangingRadius = true;
        SetRadius(mRadius); // Yarıçapı ayarla
    }

    private void SetRadius(float mRadius)
    {
        radius = mRadius; // Yarıçapı ayarla

        for (int i = 0; i < containerCount; i++)
        {
            float mAngle = (360f / containerCount) * i; // Açıyı hesapla
            Vector3 pos = CalculatePosition(mAngle); // Pozisyonu hesapla
            objectsPositions[i] = pos; // Pozisyonu kaydet
            if (isChangingRadius)
            {
                RadialContainer[i].transform.position = pos; // Nesnelerin pozisyonunu güncelle
            }
        }
    }
}
