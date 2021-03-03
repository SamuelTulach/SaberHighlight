using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SaberHighlight
{
    class SummaryController : MonoBehaviour
    {
        public static SummaryController Instance { get; private set; }

        public void Awake()
        {
            if (Instance != null)
            {
                GameObject.DestroyImmediate(this);
                return;
            }
            GameObject.DontDestroyOnLoad(this);
            Instance = this;
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.F3))
            {
                Highlight.ShowSummary();
            }
        }
    }
}
