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
