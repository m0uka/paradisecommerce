# ParadiseCommerce
### ParadiseCommerce is a fully-fledged web e-commerce system built with the Microservices architecture.

# [See it live here!](https://paradisecommerce.m0uka.dev)

## Used Technologies

ParadiseCommerce utilizes a lot of technologies and architectures.

#### Backend
- ASP.Net Core
- Microservices architecture
- MassTransit (RabbitMQ)
- MongoDB
- Ocelot
- JWT
- Stripe

#### Frontend
- Nuxt 3
- Pinia
- TailwindCSS

## Software Documentation

### Installation and Setup

Installing ParadiseCommerce isn't that simple, because it utilizes the Microservice architecture, however it is simplified for you.  
  
It should be deployed on an orchestrator, such as Kubernetes, but for the sake of simplicity there's a Docker Compose file used for deploying the backend and its required services.  
Frontend is not included in the Docker Compose and it has to be deployed manually, ideally on a service such as Cloudflare Pages.

#### Configuring app secrets and settings
You will need to create a config file for every microservice - there should be an appsettings.Example.json in every microservice - copy that, rename it to appsettings.Production.json and tweak the settings.
  
If you don't wish to use Docker for the backend deployment, you can compile and run all the services seperately, including RabbitMQ and MongoDB.  
If you did everything right, the backend should be running.

Before building the frontend, you'll need to set a BASE_URL environment variable, that points to the API backend. It must end with a trailing slash.

You can start the frontend in dev mode by using NPM or Yarn, with the following commands (for Yarn):

```bash
cd ParadiseCommerce.Frontend
yarn install
yarn dev
```

Deploying the frontend will matter on your preferences and hosting environment, but you should use
```bash
yarn build
```
to build a production environment.

NOTE: This actually doesn't work yet, pending https://github.com/nuxt/framework/issues/886

#### Store Setup

Your Store should be running at port 3000.  
In order to setup the store, first you have to register. After registering, you will need to add yourself to the Admin role in MongoDB.  
After adding yourself to Admin, you can visit the /admin path, where you can configure most of the stuff.  
  
Please do not enter invalid values, there is almost none form validation built in currently.

## Ending words
This project was made as a school project, and I might continue working on it for my personal purposes.  
It is definitely not finished, but it's a solid codebase to build on.  
This repository does not have a public license and all rights are reserved. You cannot modify or redistribute this code without explicit permission from the copyright holder.
