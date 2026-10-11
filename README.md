Carsties

A car auction platform built as a microservices system. The backend is split into five services (Identity, Auction, Search, Bidding, and Notification) that communicate asynchronously through a RabbitMQ event bus and sit behind an API Gateway. Bidding updates reach users in real time via SignalR. Authentication is handled by IdentityServer, and the Next.js frontend talks to the backend through a BFF layer.

Architecture: PostgreSQL for the Identity and Auction services, MongoDB for Search and Bidding, everything containerized with Docker.

Tech stack: .NET, Next.js, RabbitMQ, IdentityServer, SignalR, PostgreSQL, MongoDB, Docker

Built while following Neil Cummings’ “Build a Microservices App with .NET and NextJS from Scratch” course on Udemy.

You can run this app locally on your computer by following these instructions:

1. Using your terminal or command prompt clone the repo onto your machine in a user folder

    git clone https://github.com/mercera/Carsties.git

2. Change into the Carsties directory
   
    cd Carsties

3. Ensure you have Docker Desktop installed on your machine. If not download and install from Docker and review their installation instructions for your Operating system here.
   
4. Build the services locally on your computer by running (NOTE: this may take several minutes to complete):
   
    docker compose build

5. Once this completes you can use the following to run the services:
   
    docker compose up -d

6. To see the app working you will need to provide it with an SSL certificate. To do this please install 'mkcert' onto your computer which you can get from here. Once you have this you will need to install the local Certificate Authority by using:

    mkcert -install

7. You will then need to create the certificate and key file on your computer to replace the certificates that I used. You will need to change into the 'devcerts' directory and then run the following command:
   
    cd devcerts
    mkcert -key-file carsties.local.key -cert-file carsties.local.crt app.carsties.local api.carsties.local id.carsties.local

8. You will also need to create an entry in your host file so you can reach the app by its domain name. Please use this guide if you do not know how to do this. Create the following entry:
   
    127.0.0.1 id.carsties.local app.carsties.local api.carsties.local

You should now be able to browse to the app on https://app.carsties.local
