using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyConfiguration", menuName = "Scriptable Objects/Enemy/Enemy Configuration", order = 2)]
public class EnemyConfigurationSO : ScriptableObject
{
    [Header("Main section")]
    [SerializeField] private string enemyName;
    [SerializeField] private float health;
    [SerializeField] private LootSO enemyLoot;
    [SerializeField] private Sprite enemySprite;
    [SerializeField] private bool boss;
    [SerializeField] private GameObject enemyPrefab;


    [Header("Movement section")]
    [SerializeField] private float movementSpeed;
    [Tooltip("Minimum time (in seconds) object waits to move when is not triggered by target")]
    [SerializeField] private int minMoveInterval;
    [Tooltip("Maximum time (in seconds) object waits to move when is not triggered by target")]
    [SerializeField] private int maxMoveInterval;

    [Header("Deff section")]
    [Tooltip("Armor reduce physical damages acording: Damage Multiplier = 100/(100 + Armor)")]
    [SerializeField] private float armor;
    [Tooltip("Magic Resistance reduce magic damages acording: Damage Multiplier = 100/(100 + Magic Resistance)")]
    [SerializeField] private float magicResistance;

    [Header("Attack section")]
    [Tooltip("Attacks per minute")]
    [SerializeField] private float attackSpeed;
    [Tooltip("Minimum Distance between target to attack")]
    [SerializeField] private float attackRange;
    [Tooltip("Minimum Distance between target to trigger object")]
    [SerializeField] private float triggerRange;
    [SerializeField] private float physicalDamage;
    [SerializeField] private float magicDamage;
    [SerializeField] private float trueDamage;
    [SerializeField] private bool projectileAttack;
    [SerializeField] [HideInInspector] private GameObject projectilePrefab;

    public string EnemyName { get => enemyName; }
    public float Health { get => health; }
    public LootSO Loot { get => enemyLoot; }
    public Sprite Sprite { get => enemySprite; }
    public bool Boss { get => boss; }
    public float MovementSpeed { get => movementSpeed; }
    public int MinMoveInterval { get => minMoveInterval; }
    public int MaxMoveInterval { get => maxMoveInterval; }
    public float Armor { get => armor; }
    public float MagicResistance { get => magicResistance; }
    public float AttackSpeed { get => attackSpeed; }
    public float AttackRange { get => attackRange; }
    public float TriggerRange { get => triggerRange; }
    public float PhysicalDamage { get => physicalDamage; }
    public float MagicDamage { get => magicDamage; }
    public float TrueDamage { get => trueDamage; }
    public bool ProjectileAttack { get => projectileAttack; }
    public GameObject ProjectilePrefab 
    { 
        get
        {
            if (projectileAttack)
            {
                return projectilePrefab;
            }
            return null;
        }
    }
    public GameObject EnemyPrefab { get => enemyPrefab; }

#if UNITY_EDITOR
    [CustomEditor(typeof(EnemyConfigurationSO))]
    public class EnemyConfigurationSOEditor : Editor
    {
        private EnemyConfigurationSO enemyConfigSO;

        private void OnEnable()
        {
            enemyConfigSO = (EnemyConfigurationSO)target;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (enemyConfigSO.projectileAttack)
            {
                enemyConfigSO.projectilePrefab = (GameObject)EditorGUILayout.ObjectField("Projectile Prefab", enemyConfigSO.projectilePrefab, typeof(GameObject), true);
            }

            serializedObject.ApplyModifiedProperties();
            serializedObject.Update();
        }
    }
#endif
}
