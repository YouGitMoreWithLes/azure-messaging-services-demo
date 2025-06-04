$rgName = "messaging-rg"
$location = "eastus"
$sbNamespaceName = "example-sbn"
$queueName = "example-queue"
$topicName = "example-topic"
$topicSn = "example-topic-subscription"

$createRg = $True

$expectedSubName = "insight-lmcwhirter-vse-github"

$azAccount = az account show --output json 2>$null | ConvertFrom-Json

if ($azAccount -and $azAccount.name -eq $expectedSubName) {
    Write-Host "You are logged in and using the expected subscription: $expectedSubName" -ForegroundColor Green
} elseif ($azAccount) {
    Write-Host "You are logged in, but the current subscription is '$($azAccount.name)', not '$expectedSubName'" -ForegroundColor Red
    Write-Host "Switching to the expected subscription..." -ForegroundColor Yellow
    az account set --name $expectedSubName
} else {
    Write-Host "You are not logged in. Please log in to Azure." -ForegroundColor Red
    az login --tenant $env:AZURE_TENANT_ID
}

az account show

if($createRg) {
    Write-Host "Creating resource group: $rgName in location: $location" -ForegroundColor Cyan
    az group create --name $rgName --location $location
}

Write-Host "Creating Service Bus Namespace..." -ForegroundColor Cyan
az servicebus namespace create `
  --name $sbNamespaceName `
  --resource-group $rgName `
  --location $location `
  --sku Standard

Write-Host "Creating Service Bus Queue..." -ForegroundColor Cyan
az servicebus queue create `
  --name $queueName `
  --namespace-name $sbNamespaceName `
  --resource-group $rgName

Write-Host "Creating Service Bus Topic..." -ForegroundColor Cyan
az servicebus topic create `
  --name $topicName `
  --namespace-name $sbNamespaceName `
  --resource-group $rgName

Write-Host "Creating Service Bus Topic Subscription..." -ForegroundColor Cyan
az servicebus topic subscription create `
  --name $topicSn `
  --topic-name $topicName `
  --namespace-name $sbNamespaceName `
  --resource-group $rgName

Write-Host "Creating Service Bus Topic Subscription #2..." -ForegroundColor Cyan
az servicebus topic subscription create `
  --name $topicSn"2" `
  --topic-name $topicName `
  --namespace-name $sbNamespaceName `
  --resource-group $rgName

Write-Host "Querying primary connection string..." -ForegroundColor Cyan
$connectionString = az servicebus namespace authorization-rule keys list `
  --resource-group $rgName `
  --namespace-name $sbNamespaceName `
  --name "RootManageSharedAccessKey" `
  --query "primaryConnectionString" `
  --output tsv

Write-Host "Service Bus Namespace: $sbNamespaceName" -ForegroundColor Green
Write-Host "Service Bus Connection: $connectionString" -ForegroundColor Green

[System.Environment]::SetEnvironmentVariable("SERVICE_BUS_CONNECTION_STRING", $connectionString, "User")

Write-Host "Environment variable SERVICE_BUS_CONNECTION_STRING set." -ForegroundColor Green
Write-Host "You can now run the Service Bus Producer and Consumer projects." -ForegroundColor Green
