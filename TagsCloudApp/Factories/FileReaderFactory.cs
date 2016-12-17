namespace TagsCloudApp.Factories
{
    public class FileReaderFactory : IInputStreamFactory
    {
        public IInputStream Create(Options args)
        {
            return new FileReader(args.InputFile);
        }
    }
}
