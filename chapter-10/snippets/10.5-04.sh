# Modern .NET, Chapter 10, Section 10.5
dotnet ef migrations bundle --self-contained -r linux-x64 --output efbundle
./efbundle --connection "<connection string>"
