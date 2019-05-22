namespace NVP.Helpers
{
    interface IAnimatable<T>
    {
        AnimationHelper<T> AnimationHelper { get; set; }
    }
}
