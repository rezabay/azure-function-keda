param (
    $registry = "localhost:5000",
    $appVersion = "1.0.0"
)

$ErrorActionPreference = "Stop"

function BuildDockerImage {
    param (
        $dockerFile,
        $imageName
    )
    
    $registryUrl =  "$registry/azure-func-demo/$imageName" + ":" + $appVersion
    Write-Host "Building Image: $registryUrl"

    docker build -t $registryUrl -f "$dockerFile/Dockerfile" --build-arg BUILD_NUMBER=$appVersion .

    docker push "$registryUrl"
}

function DeployHelmChart {
    param (
        $name
    )

    helm upgrade --install $name ./helm/$name --set image.tag=$appVersion
}

function BuildAll {
    BuildDockerImage "./src/Demo.WebApi" "demo-webapi"
    BuildDockerImage "./src/Demo.AzureFunction" "demo-azure-function"
}

function DeployAll {
    DeployHelmChart "demo-webapi"
    DeployHelmChart "demo-azure-function"
}

function DeleteAll {
    helm delete "demo-webapi" "demo-azure-function"
}

BuildAll
# DeployAll
# DeleteAll