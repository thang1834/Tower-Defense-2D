using System;
using System.Collections.Generic;

namespace Spine
{
    /// <summary>Stores the setup pose for an IkConstraint.</summary>
    public class IkConstraintData
    {
        internal string name;
        internal int order;
        internal List<BoneData> bones = new List<BoneData>();
        internal BoneData target;
        internal int bendDirection = 1;
        internal float mix = 1;

        /// <summary>The IK constraint's name, which is unique within the skeleton.</summary>
        public string Name
        {
            get { return name; }
        }

        public int Order
        {
            get { return order; }
            set { order = value; }
        }

        /// <summary>The bones that are constrained by this IK Constraint.</summary>
        public List<BoneData> Bones
        {
            get { return bones; }
        }

        /// <summary>The bone that is the IK target.</summary>
        public BoneData Target
        {
            get { return target; }
            set { target = value; }
        }

        /// <summary>Controls the bend direction of the IK bones, either 1 or -1.</summary>
        public int BendDirection
        {
            get { return bendDirection; }
            set { bendDirection = value; }
        }

        public float Mix { get { return mix; } set { mix = value; } }

        public IkConstraintData(string name)
        {
            if (name == null) throw new ArgumentNullException("name", "name cannot be null.");
            this.name = name;
        }

        override public string ToString()
        {
            return name;
        }
    }
}
