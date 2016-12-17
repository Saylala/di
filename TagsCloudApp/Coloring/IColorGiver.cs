namespace TagsCloudApp
{
    public interface IColorGiver
    {
        Cloud<T> GiveColors<T>(Cloud<T> cloud);
    }
}
