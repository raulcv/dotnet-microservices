#!/bin/bash

RESOURCE_GROUP_NAME="rg-microservice"
LOCATION="eastus2"
SERVICE_BUS_NAMESPACE_NAME="sb-pickage-rcv"
CONTAINER_APPS_ENV_NAME="env-microservice"

# Create a resource group
az group create --name $RESOURCE_GROUP_NAME --location $LOCATION

# Create a Service Bus namespace and services for messaging
az servicebus namespace create --resource-group $RESOURCE_GROUP_NAME --name $SERVICE_BUS_NAMESPACE_NAME --location $LOCATION --sku Standard

az servicebus queue create --resource-group $RESOURCE_GROUP_NAME --namespace-name $SERVICE_BUS_NAMESPACE_NAME --name "pickage"

az servicebus topic create --resource-group $RESOURCE_GROUP_NAME --namespace-name $SERVICE_BUS_NAMESPACE_NAME --name "adulttopic"
az servicebus topic subscription create --resource-group $RESOURCE_GROUP_NAME --namespace-name $SERVICE_BUS_NAMESPACE_NAME --topic-name "adulttopic" --name S1

az servicebus topic create --resource-group $RESOURCE_GROUP_NAME --namespace-name $SERVICE_BUS_NAMESPACE_NAME --name "childtopic"
az servicebus topic subscription create --resource-group $RESOURCE_GROUP_NAME --namespace-name $SERVICE_BUS_NAMESPACE_NAME --topic-name "childtopic" --name S1

az containerapp env create --name $CONTAINER_APPS_ENV_NAME --resource-group $RESOURCE_GROUP_NAME --location $LOCATION --internal-only false
