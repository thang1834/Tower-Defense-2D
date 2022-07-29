using System;

namespace Spine
{
    /// <summary>Attachment that displays a texture region using a mesh.</summary>
    public class MeshAttachment : VertexAttachment, IHasRendererObject
    {
        internal float regionOffsetX, regionOffsetY, regionWidth, regionHeight, regionOriginalWidth, regionOriginalHeight;
        private MeshAttachment parentMesh;
        internal float[] uvs, regionUVs;
        internal int[] triangles;
        internal float r = 1, g = 1, b = 1, a = 1;
        internal int hulllength;
        internal bool inheritDeform;

        public int HullLength { get { return hulllength; } set { hulllength = value; } }
        public float[] RegionUVs { get { return regionUVs; } set { regionUVs = value; } }
        /// <summary>The UV pair for each vertex, normalized within the entire texture. <seealso cref="MeshAttachment.UpdateUVs"/></summary>
        public float[] UVs { get { return uvs; } set { uvs = value; } }
        public int[] Triangles { get { return triangles; } set { triangles = value; } }

        public float R { get { return r; } set { r = value; } }
        public float G { get { return g; } set { g = value; } }
        public float B { get { return b; } set { b = value; } }
        public float A { get { return a; } set { a = value; } }

        public string Path { get; set; }
        public object RendererObject { get; set; }
        public float RegionU { get; set; }
        public float RegionV { get; set; }
        public float RegionU2 { get; set; }
        public float RegionV2 { get; set; }
        public bool RegionRotate { get; set; }
        public float RegionOffsetX { get { return regionOffsetX; } set { regionOffsetX = value; } }
        public float RegionOffsetY { get { return regionOffsetY; } set { regionOffsetY = value; } } // Pixels stripped from the bottom left, unrotated.
        public float RegionWidth { get { return regionWidth; } set { regionWidth = value; } }
        public float RegionHeight { get { return regionHeight; } set { regionHeight = value; } } // Unrotated, stripped size.
        public float RegionOriginalWidth { get { return regionOriginalWidth; } set { regionOriginalWidth = value; } }
        public float RegionOriginalHeight { get { return regionOriginalHeight; } set { regionOriginalHeight = value; } } // Unrotated, unstripped size.

        public bool InheritDeform { get { return inheritDeform; } set { inheritDeform = value; } }

        public MeshAttachment ParentMesh
        {
            get { return parentMesh; }
            set
            {
                parentMesh = value;
                if (value != null)
                {
                    bones = value.bones;
                    vertices = value.vertices;
                    worldVerticesLength = value.worldVerticesLength;
                    regionUVs = value.regionUVs;
                    triangles = value.triangles;
                    HullLength = value.HullLength;
                    Edges = value.Edges;
                    Width = value.Width;
                    Height = value.Height;
                }
            }
        }

        // Nonessential.
        public int[] Edges { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public MeshAttachment(string name)
            : base(name)
        {
        }

        public void UpdateUVs()
        {
            float u = RegionU, v = RegionV, width = RegionU2 - RegionU, height = RegionV2 - RegionV;
            float[] regionUVs = this.regionUVs;
            if (this.uvs == null || this.uvs.Length != regionUVs.Length) this.uvs = new float[regionUVs.Length];
            float[] uvs = this.uvs;
            if (RegionRotate)
            {
                for (int i = 0, n = uvs.Length; i < n; i += 2)
                {
                    uvs[i] = u + regionUVs[i + 1] * width;
                    uvs[i + 1] = v + height - regionUVs[i] * height;
                }
            }
            else
            {
                for (int i = 0, n = uvs.Length; i < n; i += 2)
                {
                    uvs[i] = u + regionUVs[i] * width;
                    uvs[i + 1] = v + regionUVs[i + 1] * height;
                }
            }
        }

        override public bool ApplyDeform(VertexAttachment sourceAttachment)
        {
            return this == sourceAttachment || (inheritDeform && parentMesh == sourceAttachment);
        }
    }
}
