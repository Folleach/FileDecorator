### Summary
**A simple interface for downloading file.**  

Supports custom id: https://example.net/d/{id}  
With custom filename using [Content-Disposition](https://developer.mozilla.org/ru/docs/Web/HTTP/Headers/Content-Disposition)

### Docker compose example
```yaml
version: "3.8"

services:
  file-decorator:
    image: folleach/file-decorator:v1.0.0
    restart: always
    environment:
      - FILE_DECORATOR_META_PATH=/config/meta.json
    volumes:
      - ./config:/config
      - ./data:/data
    ports:
      - 5100:80
    deploy:
      resources:
        limits:
          memory: 512M
```

### Configuration
**/config/meta.json** may looks like this
```json
[
  {
    "Id": "testId",
    "FilePath": "/data/testId.txt",
    "FileName": "hello.txt"
  }
]
```

| filed | required? | uses for |
| --- | --- | --- |
| Id | x | link formation: https://example.net/d/{id} |
| FilePath | x | path to the file in file system |
| FileName |  | overrides the name of the uploaded file |

