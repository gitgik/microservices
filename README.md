# Microservices

Building two microservices using C#, .NET, GRPC, RabbitMQ and Kubernetes.

## Running Microservices

- Ensure docker is running and kubernetes is enabled in docker settings
- Cd into the K8S (Kubernetes) folder
- Run kubernetes deployment with the command below

    ```
    kubectl apply -f platforms-depl.yaml
    ```