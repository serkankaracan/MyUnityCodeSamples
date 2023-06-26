using UnityEditor;
using UnityEngine;

public class UIToggler
{
    private const int UI_LAYER = 1 << 5;

    [InitializeOnLoadMethod]
    private static void Init()
    {
#if UNITY_2019_1_OR_NEWER
        // Unity sürümü 2019.1 veya daha yeni ise
        SceneView.duringSceneGui -= OnSceneGUI;
        SceneView.duringSceneGui += OnSceneGUI;
#else
		// Unity sürümü 2019.1'den eski ise
		SceneView.onSceneGUIDelegate -= OnSceneGUI;
		SceneView.onSceneGUIDelegate += OnSceneGUI;
#endif
    }

    private static void OnSceneGUI(SceneView sceneView)
    {
        Handles.BeginGUI();

        // UI katmanının görünürlük durumunu kontrol et
        bool uiVisible = (Tools.visibleLayers & UI_LAYER) == UI_LAYER;

        // Görünürlük durumuna bağlı olarak "Hide Canvas" veya "Show Canvas" etiketli bir düğme oluştur
        if (GUI.Button(new Rect(0f, 0f, 125f, 25f), uiVisible ? "Hide Canvas" : "Show Canvas"))
        {
            // Düğme tıklandığında UI katmanının görünürlüğünü değiştir
            if (uiVisible)
                Tools.visibleLayers &= ~UI_LAYER; // UI katmanını kapat
            else
                Tools.visibleLayers |= UI_LAYER; // UI katmanını aç
        }

        Handles.EndGUI();
    }
}
