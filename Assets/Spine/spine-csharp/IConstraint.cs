namespace Spine
{

    /// <summary>The interface for all constraints.</summary>
    public interface IConstraint : IUpdatable
    {
        /// <summary>The ordinal for the order a skeleton's constraints will be applied.</summary>
        int Order { get; }

    }

}