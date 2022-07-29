using System;

namespace Spine
{
    /// <summary>Attachment that has a polygon for bounds checking.</summary>
    public class BoundingBoxAttachment : VertexAttachment
    {
        public BoundingBoxAttachment(string name)
            : base(name)
        {
        }
    }
}
