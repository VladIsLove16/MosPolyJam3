//using UnityEditor;
//using UnityEngine;

//[CustomEditor(typeof(DamageModificationSpawner))]
//public class SpawnSettingsEditor : Editor
//{
//    // Кешируем объект для редактирования
//    private SerializedProperty spawnDinamically;
//    private SerializedProperty interval;
//    private SerializedProperty startDelay;
//    private SerializedProperty dinamicSpawnPoints;
//    private SerializedProperty damagePickupPrefab;
//    private SerializedProperty hardSpawnPoints;
//    private SerializedProperty availableModifiers;

//    private void OnEnable()
//    {
//        // Инициализация SerializedProperty для каждого параметра
//        spawnDinamically = serializedObject.FindProperty("spawnDinamically");
//        interval = serializedObject.FindProperty("Interval");
//        startDelay = serializedObject.FindProperty("StartDelay");
//        dinamicSpawnPoints = serializedObject.FindProperty("dinamicSpawnPoints");
//        damagePickupPrefab = serializedObject.FindProperty("damagePickupPrefab");
//        hardSpawnPoints = serializedObject.FindProperty("hardSpawnPoints");
//        availableModifiers = serializedObject.FindProperty("damageModificationsLists");
//    }

//    public override void OnInspectorGUI()
//    {
//        // Обновляем данные объекта
//        serializedObject.Update();

//        // Отображаем параметр spawnDinamically
//        EditorGUILayout.PropertyField(spawnDinamically);

//        // Если spawnDinamically == true, показываем остальные параметры
//        if (spawnDinamically.boolValue)
//        {
//            EditorGUILayout.PropertyField(interval);
//            EditorGUILayout.PropertyField(startDelay);
//            EditorGUILayout.PropertyField(dinamicSpawnPoints, true); // true для отображения массивов
//        }

//        // Отображаем другие параметры, которые всегда видны
//        EditorGUILayout.PropertyField(damagePickupPrefab);
//        EditorGUILayout.PropertyField(hardSpawnPoints, true); // true для массивов
//        EditorGUILayout.PropertyField(availableModifiers, true); // true для массивов

//        // Применяем изменения
//        serializedObject.ApplyModifiedProperties();
//    }
//}
