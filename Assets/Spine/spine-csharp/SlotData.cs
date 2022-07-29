using System;

namespace Spine
{
    public class SlotData
    {
        internal int index;
        internal string name;
        internal BoneData boneData;
        internal float r = 1, g = 1, b = 1, a = 1;
        internal float r2 = 0, g2 = 0, b2 = 0;
        internal bool hasSecondColor = false;
        internal string attachmentName;
        internal BlendMode blendMode;

        public int Index { get { return index; } }
        public string Name { get { return name; } }
        public BoneData BoneData { get { return boneData; } }
        public float R { get { return r; } set { r = value; } }
        public float G { get { return g; } set { g = value; } }
        public float B { get { return b; } set { b = value; } }
        public float A { get { return a; } set { a = value; } }

        public float R2 { get { return r2; } set { r2 = value; } }
        public float G2 { get { return g2; } set { g2 = value; } }
        public float B2 { get { return b2; } set { b2 = value; } }
        public bool HasSecondColor { get { return hasSecondColor; } set { hasSecondColor = value; } }

        /// <summary>May be null.</summary>
        public String AttachmentName { get { return attachmentName; } set { attachmentName = value; } }
        public BlendMode BlendMode { get { return blendMode; } set { blendMode = value; } }

        public SlotData(int index, String name, BoneData boneData)
        {
            if (index < 0) throw new ArgumentException("index must be >= 0.", "index");
            if (name == null) throw new ArgumentNullException("name", "name cannot be null.");
            if (boneData == null) throw new ArgumentNullException("boneData", "boneData cannot be null.");
            this.index = index;
            this.name = name;
            this.boneData = boneData;
        }

        override public string ToString()
        {
            return name;
        }
    }
}
