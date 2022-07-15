using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SpaceDemo
{
    [CustomEditor(typeof(PlanetController))]
    [CanEditMultipleObjects]
    public class PlanetControllerInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawDefaultInspector();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Autobuild", EditorStyles.boldLabel);
            EditorGUILayout.HelpBox("This button will automatically add everything needed for the planet's functioning, except for the mesh and the collider.", MessageType.Warning, true);

            if (GUILayout.Button("Autobuild Planet"))
            {
                GeneratePlanet();
            }

            serializedObject.ApplyModifiedProperties();
        }

        public void GeneratePlanet()
        {
            Debug.LogFormat(target.name + " : Planet Autobuild Start");

            PlanetController planet = (PlanetController)target;

            planet.gameObject.layer = LayerMask.NameToLayer("Planets");
            planet.gameObject.tag = "Planet";

            Rigidbody rigidbody = (planet.GetComponent<Rigidbody>() == null) ? planet.gameObject.AddComponent<Rigidbody>() : planet.GetComponent<Rigidbody>();
            rigidbody.mass = 1;
            rigidbody.drag = 0;
            rigidbody.angularDrag = 0.05f;
            rigidbody.interpolation = RigidbodyInterpolation.None;
            rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
            rigidbody.useGravity = false;
            rigidbody.isKinematic = true;

            if (planet.GetComponent<Storage>() == null) { planet.gameObject.AddComponent<Storage>(); }
            
            Outline outline = planet.GetComponent<Outline>();
            if (outline == null) { outline = planet.gameObject.AddComponent<Outline>(); }
            outline.enabled = false;
            outline.OutlineMode = Outline.Mode.OutlineVisible;
            outline.OutlineColor = new Color(0.5979676f, 1f, 0.07075471f);
            outline.OutlineWidth = 5;

            if (planet.GetComponent<OutlineOnMouseHover>() == null) { planet.gameObject.AddComponent<OutlineOnMouseHover>(); }
            if (planet.GetComponent<PlanetPreviewOnMouseHover>() == null) { planet.gameObject.AddComponent<PlanetPreviewOnMouseHover>(); }
            if (planet.GetComponent<Wallet>() == null) { planet.gameObject.AddComponent<Wallet>().Money = 2000; }

            TradePoint planetTradePoint = planet.GetComponent<TradePoint>();
            if (planetTradePoint == null) { planet.gameObject.AddComponent<TradePoint>(); }

            Vector3[] returnPointLocalPos = { new Vector3(0, 0, 8), new Vector3(8, 0, 0), new Vector3(-8, 0, 0), new Vector3(0, 0, -8) };
            if (planetTradePoint.returnPoints.Count == 0)
            {
                for (int i = 1; i < 5; i++)
                {
                    var newReturnPoint = new GameObject("ReturnPoint" + i.ToString());
                    newReturnPoint.transform.parent = planet.transform;
                    newReturnPoint.transform.localPosition = returnPointLocalPos[i - 1];
                    planetTradePoint.returnPoints.Add(newReturnPoint.transform);
                }
            }
        }
    }
}