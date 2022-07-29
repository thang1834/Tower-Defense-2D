using System;

namespace Spine
{
    public class PathConstraintData
    {
        internal string name;
        internal int order;
        internal ExposedList<BoneData> bones = new ExposedList<BoneData>();
        internal SlotData target;
        internal PositionMode positionMode;
        internal SpacingMode spacingMode;
        internal RotateMode rotateMode;
        internal float offsetRotation;
        internal float position, spacing, rotateMix, translateMix;

        public string Name { get { return name; } }
        public int Order { get { return order; } set { order = value; } }
        public ExposedList<BoneData> Bones { get { return bones; } }
        public SlotData Target { get { return target; } set { target = value; } }
        public PositionMode PositionMode { get { return positionMode; } set { positionMode = value; } }
        public SpacingMode SpacingMode { get { return spacingMode; } set { spacingMode = value; } }
        public RotateMode RotateMode { get { return rotateMode; } set { rotateMode = value; } }
        public float OffsetRotation { get { return offsetRotation; } set { offsetRotation = value; } }
        public float Position { get { return position; } set { position = value; } }
        public float Spacing { get { return spacing; } set { spacing = value; } }
        public float RotateMix { get { return rotateMix; } set { rotateMix = value; } }
        public float TranslateMix { get { return translateMix; } set { translateMix = value; } }

        public PathConstraintData(String name)
        {
            if (name == null) throw new ArgumentNullException("name", "name cannot be null.");
            this.name = name;
        }

        public override string ToString()
        {
            return name;
        }
    }

    public enum PositionMode
    {
        Fixed, Percent
    }

    public enum SpacingMode
    {
        Length, Fixed, Percent
    }

    public enum RotateMode
    {
        Tangent, Chain, ChainScale
    }
}
