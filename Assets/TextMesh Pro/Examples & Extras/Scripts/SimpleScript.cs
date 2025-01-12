using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider; // Referencia a Slider komponenshez
    public AudioSource audioSource; // Referencia az AudioSource-hoz

    private static VolumeController instance; // Statikus példány a Singleton mintához

    void Awake()
    {
        // Ellenőrzés, hogy van-e már egy példány
        if (instance == null)
        {
            instance = this; // Ha nincs, akkor beállítjuk az aktuális példányt
            DontDestroyOnLoad(gameObject); // Megakadályozza, hogy a zene törlődjön jelenetváltáskor
        }
        else
        {
            Destroy(gameObject); // Ha van már egy példány, akkor töröljük az újat
        }
    }

    void Start()
    {
        // Alapértelmezett érték beállítása
        float defaultVolume = 0.2f;
        volumeSlider.value = defaultVolume;
        if (audioSource != null)
        {
            audioSource.volume = defaultVolume;
        }

        // Feliratkozás az OnValueChanged eseményre
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    // A hangerő beállítása a slider aktuális értékére
    public void SetVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
    }
}
