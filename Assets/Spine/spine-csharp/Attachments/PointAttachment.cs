namespace Spine
{
    public class PointAttachment : Attachment
    {
        internal float x, y, rotation;
        public float X { get { return x; } set { x = value; } }
        public float Y { get { return y; } set { y = value; } }
        public float Rotation { get { return rotation; } set { rotation = value; } }

        public PointAttachment(string name)
            : base(name)
        {
        }

        public void ComputeWorldPosition(Bone bone, out float ox, out float oy)
        {
            bone.LocalToWorld(this.x, this.y, out ox, out oy);
        }

        public float ComputeWorldRotation(Bone bone)
        {
            float cos = MathUtils.CosDeg(rotation), sin = MathUtils.SinDeg(rotation);
            float ix = cos * bone.a + sin * bone.b;
            float iy = cos * bone.c + sin * bone.d;
            return MathUtils.Atan2(iy, ix) * MathUtils.RadDeg;
        }
    }
}

