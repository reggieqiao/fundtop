# Fundtop

Fundtop is an application that displays fund rankings

## Overview
![QRcodehub](https://raw.githubusercontent.com/reggieqiao/fundtop/main/docs/assets/examples.png)

## Languages and Tools
<a href="https://dotnet.microsoft.com/" target="_blank" rel="noreferrer"> <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/dot-net/dot-net-original-wordmark.svg" alt="dotnet" width="40" height="40"/> </a>
<a href="https://www.w3schools.com/cs/" target="_blank" rel="noreferrer"> <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/csharp/csharp-original.svg" alt="csharp" width="40" height="40"/> </a>

## Installation
Create database table structure, sql file path `fundtop/docs/database/fund_ranking.sql`

It is recommended that the application be deployed in [docker](https://www.docker.com)

### Deploy Fundtop.Crawler
```shell
# clone source code
git clone git@github.com:reggieqiao/fundtop.git
cd fundtop/src

# build new image
sudo docker build -f Fundtop.Crawler/Dockerfile -t fundtop/crawler .

# run container
sudo docker run -d --name fundtop-crawler -v /etc/hosts:/etc/hosts fundtop/crawler:latest
```

### Deploy Fundtop.Web
```shell
# clone source code
git clone git@github.com:reggieqiao/fundtop.git
cd fundtop/src

# build new image
sudo docker build -f Fundtop.Web/Dockerfile -t fundtop/web .

# run container
sudo docker run -d --name fundtop-web-prod -v /etc/hosts:/etc/hosts -p 3080:80 fundtop/web:latest
```
Open up http://localhost:3080 in your browser, and you'll see the default home page being displayed

