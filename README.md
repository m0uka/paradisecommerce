# ParadiseCommerce
### ParadiseCommerce is a fully-fledged web e-commerce system built with the Microservices architecture.

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
  
If you don't wish to use Docker for the backend deployment, you can compile and run all the services seperately, including RabbitMQ and MongoDB.

