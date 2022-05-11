# azure-function-keda
Azure function demo hosted on Kubernetes with Keda extension

## Getting Started
- Install .Net SDK 6.0
- Install Docker from [here](https://docs.docker.com/engine/install/)
- Run Azurite docker container
```bash
docker run --restart=always -p 10000:10000 -p 10001:10001 -p 10002:10002 -d mcr.microsoft.com/azure-storage/azurite
```

## Run
- `cd src/Demo.WebApi/`
- `dotnet run`
- To insert 500 items to Azure Queue: `curl -method POST 'https://localhost:5002/api/demo'`

## Build Docker compose
Run the following command from the root directory
```bash
docker-compose -f "docker-compose.yml" up -d --build
```

## Run Kubernetes dashboard
- `kubectl apply -f https://raw.githubusercontent.com/kubernetes/dashboard/v2.5.0/aio/deploy/recommended.yaml`
- `kubectl apply -f ./k8s/dashboard-adminuser.yaml`
- `kubectl apply -f ./k8s/dashboard-cluster-admin.yaml`
- Generate login token: `kubectl -n kubernetes-dashboard get secret $(kubectl -n kubernetes-dashboard get sa/admin-user -o jsonpath="{.secrets[0].name}") -o go-template="{{.data.token | base64decode}}"`
- `kubectl proxy`
- Navigate to: `http://localhost:8001/api/v1/namespaces/kubernetes-dashboard/services/https:kubernetes-dashboard:/proxy/#/login`
- Login using generated token from the previous step

## Install KEDA
```
helm repo add kedacore https://kedacore.github.io/charts
helm repo update
kubectl create namespace keda
helm install keda kedacore/keda --version 2.0.0 --namespace keda
```

## Setup local docker repository for local kubernetes cluster
- Run the local repository: `docker run -d -p 5000:5000 --restart=always --name registry registry:2`
- Add 'localhost:5000' to insecure-registries in docker settings
