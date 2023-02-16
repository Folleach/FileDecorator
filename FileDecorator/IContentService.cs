namespace FileDecorator;

public interface IContentService
{
    bool TryGet(string id, out ContentMetadata? metadata);
}
