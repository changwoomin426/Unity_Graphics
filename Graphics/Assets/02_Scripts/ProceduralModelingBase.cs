using UnityEngine;

namespace ProceduraModeling {
    public enum ProceduralModelingMaterial {
        Standard,
        UV,
        Normal,
    };

    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    [ExecuteInEditMode]
    public abstract class ProceduralModelingBase : MonoBehaviour {
        private MeshFilter _filter;
        private MeshRenderer _renderer;
        [SerializeField] protected ProceduralModelingMaterial _materialType = ProceduralModelingMaterial.UV;

        public MeshFilter Filter {
            get {
                if (_filter == null) {
                    _filter = GetComponent<MeshFilter>();
                }

                return _filter;
            }
        }

        public MeshRenderer Renderer {
            get {
                if (_renderer == null) {
                    _renderer = GetComponent<MeshRenderer>();
                }

                return _renderer;
            }
        }

        protected virtual void Start() {
            Rebuild();
        }

        public void Rebuild() {
            if (Filter.sharedMesh != null) {
                if (Application.isPlaying) {
                    Destroy(Filter.sharedMesh);
                } else {
                    DestroyImmediate(Filter.sharedMesh);
                }
            }

            Filter.sharedMesh = Build();
            Renderer.sharedMaterial = LoadMaterial(_materialType);
        }

        protected virtual Material LoadMaterial(ProceduralModelingMaterial type) {
            switch (type) {
                case ProceduralModelingMaterial.UV:
                    return Resources.Load<Material>("Materials/UV");
                case ProceduralModelingMaterial.Normal:
                    return Resources.Load<Material>("Materials/Normal");
            }

            return Resources.Load<Material>("Materials/Standard");
        }

        protected abstract Mesh Build();
    }
}