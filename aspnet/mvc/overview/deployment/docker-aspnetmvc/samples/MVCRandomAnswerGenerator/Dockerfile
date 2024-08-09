# The `FROM` instruction specifies the base image. You are
# extending the `mcr.microsoft.com/dotnet/framework/aspnet:4.8` image.

FROM mcr.microsoft.com/dotnet/framework/aspnet:4.8

# Next, this Dockerfile creates a directory for your application
RUN mkdir C:\randomanswers

# configure the new site in IIS.
RUN powershell -NoProfile -Command \
    Import-module IISAdministration; \
    New-IISSite -Name "ASPNET" -PhysicalPath C:\randomanswers -BindingInformation "*:8000:"

# This instruction tells the container to listen on port 8000. 
EXPOSE 8000

# The final instruction copies the site you published earlier into the container.
ADD MVCRandomAnswerGenerator/bin/PublishOutput /randomanswers
