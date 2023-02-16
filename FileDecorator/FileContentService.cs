using System.Text.Json;

namespace FileDecorator;

public class FileContentService : IContentService
{
    private readonly string metadataPath;
    private readonly ILogger<FileContentService> logger;
    private Dictionary<string, ContentMetadata> metadatas = null!;

    public FileContentService(string metadataPath, ILogger<FileContentService> logger)
    {
        this.metadataPath = metadataPath;
        this.logger = logger;
        UpdateAll().GetAwaiter().GetResult();
    }

    public bool TryGet(string id, out ContentMetadata? metadata) => metadatas.TryGetValue(id, out metadata);

    public async Task UpdateAll()
    {
        try
        {
            var file = new FileStream(metadataPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            var json = await JsonSerializer.DeserializeAsync<ContentMetadata[]>(file);
            if (json == null)
            {
                logger.LogError("failed to update files. Json is not parsable");
                metadatas = new Dictionary<string, ContentMetadata>();
                return;
            }
            var dict = json.ToDictionary(meta => meta.Id);
            metadatas = dict;
        }
        catch (Exception e)
        {
            logger.LogError(e, "failed to update files");
            metadatas = new Dictionary<string, ContentMetadata>();
        }
    }
}
