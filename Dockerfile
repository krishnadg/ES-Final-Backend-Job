
FROM microsoft/dotnet:1.1.2-sdk
LABEL Name=esjobv2 Version=0.0.1 
COPY . /usr/share/dotnet/sdk/esjobv2
WORKDIR /usr/share/dotnet/sdk/esjobv2
RUN dotnet restore 
RUN dotnet build 
ENTRYPOINT ["dotnet",  "run",  "--project",  "ESV2ClassLib/ESV2ClassLib.csproj"]



