namespace TagsCloudApp.Factories
{
    public class FileSaverFactory : IOutputStreamFactory
    {
        public IOutputStream Create(Options args)
        {
            return new FileSaver(args.OutputFile);
        }
    }
}
