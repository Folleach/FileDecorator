set app=%1
set image=%2
set tag=%3

rm -r out/*

git rev-parse HEAD > out/version.txt

cat Dockerfile | sed -r "s/AppEntryPoint/%app%/" > TemporaryDockerfile

echo 'use registry %DEPLOYMENT_REGISTRY%'
dotnet publish -c Release %app%/%app%.csproj -o out
docker build -t %image% -f TemporaryDockerfile --no-cache .
docker tag %image% %DEPLOYMENT_REGISTRY%/folleach/%image%:%tag%
docker push %DEPLOYMENT_REGISTRY%/folleach/%image%:%tag%
docker tag %image% %DEPLOYMENT_REGISTRY%/folleach/%image%:latest
docker push %DEPLOYMENT_REGISTRY%/folleach/%image%:latest
