# Fundtop

Fundtop is an application that displays fund rankings

## Overview
![QRcodehub](https://raw.githubusercontent.com/reggieqiao/fundtop/main/docs/assets/examples.png)

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