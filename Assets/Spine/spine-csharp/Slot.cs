using System;

namespace Spine
{
    public class Slot
    {
        internal SlotData data;
        internal Bone bone;
        internal float r, g, b, a;
        internal float r2, g2, b2;
        internal bool hasSecondColor;
        internal Attachment attachment;
        internal float attachmentTime;
        internal ExposedList<float> attachmentVertices = new ExposedList<float>();

        public SlotData Data { get { return data; } }
        public Bone Bone { get { return bone; } }
        public Skeleton Skeleton { get { return bone.skeleton; } }
        public float R { get { return r; } set { r = value; } }
        public float G { get { return g; } set { g = value; } }
        public float B { get { return b; } set { b = value; } }
        public float A { get { return a; } set { a = value; } }

        public float R2 { get { return r2; } set { r2 = value; } }
        public float G2 { get { return g2; } set { g2 = value; } }
        public float B2 { get { return b2; } set { b2 = value; } }
        public bool HasSecondColor { get { return data.hasSecondColor; } set { data.hasSecondColor = value; } }

        /// <summary>May be null.</summary>
        public Attachment Attachment
        {
            get { return attachment; }
            set
            {
                if (attachment == value) return;
                attachment = value;
                attachmentTime = bone.skeleton.time;
                attachmentVertices.Clear(false);
            }
        }

        public float AttachmentTime
        {
            get { return bone.skeleton.time - attachmentTime; }
            set { attachmentTime = bone.skeleton.time - value; }
        }

        public ExposedList<float> AttachmentVertices { get { return attachmentVertices; } set { attachmentVertices = value; } }

        public Slot(SlotData data, Bone bone)
        {
            if (data == null) throw new ArgumentNullException("data", "data cannot be null.");
            if (bone == null) throw new ArgumentNullException("bone", "bone cannot be null.");
            this.data = data;
            this.bone = bone;
            SetToSetupPose();
        }

        public void SetToSetupPose()
        {
            r = data.r;
            g = data.g;
            b = data.b;
            a = data.a;
            if (data.attachmentName == null)
                Attachment = null;
            else
            {
                attachment = null;
                Attachment = bone.skeleton.GetAttachment(data.index, data.attachmentName);
            }
        }

        override public string ToString()
        {
            return data.name;
        }
    }
}
