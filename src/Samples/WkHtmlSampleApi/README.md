# WkHtmlTo [PDF][Image] with Web Api on Docker

To use it with Docker you need to add the necessary libraries to your Docker file

### How to
- Build and run docker image

- Run docker exec to execute commands on the container
```console
docker exec -it [pod-name] -n [namespace] -- /bin/bash # for images with bash
docker exec -it [pod-name] -n [namespace] -- /bin/ash # for alpine
```

- Get OS Version
```console
cat etc/os-release
```

- Get 32bit(i686) or 64bits(x86_64)
```console
uname -m
# or
arch
```

- Download the right renderer on [WkHtml Downloads](https://wkhtmltopdf.org/downloads.html)

- Refer the downloaded file on the folders Linux/Windows as showed on the samples

- Give permission and delete Windows exe on Dockerfile with the lines
```console
RUN chmod +x Render/Linux/wkhtmltoimage
RUN rm -rf Render/Windows
```

>Tip: if you dont want to keep copying the file to multiple places (cause it have more than 30mb), just add to a folder like "assets" on root project and refer to other virtual folders with ("Add as link")[https://jeremybytes.blogspot.com/2019/07/linking-files-in-visual-studio.html]

## Troubleshooting

Run docker exec and try to run the wkhtmltopdf/wkhtmltoimage file inside the container

It should retrieve a error saying wich packages are missing to run the library
