using System;

namespace Spine
{
    abstract public class Attachment
    {
        public string Name { get; private set; }

        protected Attachment(string name)
        {
            if (name == null) throw new ArgumentNullException("name", "name cannot be null");
            Name = name;
        }

        override public string ToString()
        {
            return Name;
        }
    }

    public interface IHasRendererObject
    {
        object RendererObject { get; }
    }
}
