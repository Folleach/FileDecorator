FROM mcr.microsoft.com/dotnet/aspnet:7.0

WORKDIR /app
ADD ./out .

STOPSIGNAL SIGQUIT
CMD [ "dotnet", "FileDecorator.dll" ]
