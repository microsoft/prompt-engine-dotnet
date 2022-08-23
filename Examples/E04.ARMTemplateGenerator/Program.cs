// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.IO;
using Microsoft.AI.PromptEngine;
using Microsoft.AI.PromptEngine.Generic;
using Newtonsoft.Json;

/*
Output:

===========================
Generate and edit a JSON ARM template following my requests.

# Create an empty template
{"$schema":"https://schema.management.azure.com/schemas/2019-04-01/deploymentTemplate.json#","contentVersion":"1.0.0.0","parameters":{},"functions":[],"variables":{},"resources":[],"outputs":{}}

# Add some storage
{"$schema":"https://schema.management.azure.com/schemas/2019-04-01/deploymentTemplate.json#","contentVersion":"1.0.0.0","parameters":{},"functions":[],"variables":{},"resources":[{"name":"storageaccount1","type":"Microsoft.Storage/storageAccounts","apiVersion":"2021-04-01","tags":{"displayName":"storageaccount1"},"location":"[resourceGroup().location]","kind":"StorageV2","sku":{"name":"Premium_LRS","tier":"Premium"}}],"outputs":{}}

# Add a parameter with name 'myparameter'
{"$schema":"https://schema.management.azure.com/schemas/2019-04-01/deploymentTemplate.json#","contentVersion":"1.0.0.0","parameters":{"parameter1":{"type":"string","metadata":{"description":"description parameter 1"}},"myparameter":{"type":"int","metadata":{"description":"description for myparameter"}}},"functions":[],"variables":{},"resources":[],"outputs":{}}

# Add a variable with name 'variable2'
{"$schema":"https://schema.management.azure.com/schemas/2019-04-01/deploymentTemplate.json#","contentVersion":"1.0.0.0","parameters":{},"functions":[],"variables":{"variable1":"value 1","variable2":"value 2"},"resources":[],"outputs":{}}

# Add a Ubuntu VM
{"$schema":"https://schema.management.azure.com/schemas/2019-04-01/deploymentTemplate.json#","contentVersion":"1.0.0.0","parameters":{},"functions":[],"variables":{},"resources":[{"name":"[toLower('ubuntuVM1storage')]","type":"Microsoft.Storage/storageAccounts","apiVersion":"2021-04-01","location":"[resourceGroup().location]","tags":{"displayName":"ubuntuVM1 Storage Account"},"sku":{"name":"Standard_LRS"},"kind":"Storage"},{"name":"ubuntuVM1-PublicIP","type":"Microsoft.Network/publicIPAddresses","apiVersion":"2020-11-01","location":"[resourceGroup().location]","tags":{"displayName":"PublicIPAddress"},"properties":{"publicIPAllocationMethod":"Dynamic","dnsSettings":{"domainNameLabel":"[toLower('ubuntuVM1')]"}}},{"name":"ubuntuVM1-nsg","type":"Microsoft.Network/networkSecurityGroups","apiVersion":"2020-11-01","location":"[resourceGroup().location]","properties":{"securityRules":[{"name":"nsgRule1","properties":{"description":"description","protocol":"Tcp","sourcePortRange":"*","destinationPortRange":"22","sourceAddressPrefix":"*","destinationAddressPrefix":"*","access":"Allow","priority":100,"direction":"Inbound"}}]}},{"name":"ubuntuVM1-VirtualNetwork","type":"Microsoft.Network/virtualNetworks","apiVersion":"2020-11-01","location":"[resourceGroup().location]","dependsOn":["[resourceId('Microsoft.Network/networkSecurityGroups', 'ubuntuVM1-nsg')]"],"tags":{"displayName":"ubuntuVM1-VirtualNetwork"},"properties":{"addressSpace":{"addressPrefixes":["10.0.0.0/16"]},"subnets":[{"name":"ubuntuVM1-VirtualNetwork-Subnet","properties":{"addressPrefix":"10.0.0.0/24","networkSecurityGroup":{"id":"[resourceId('Microsoft.Network/networkSecurityGroups', 'ubuntuVM1-nsg')]"}}}]}},{"name":"ubuntuVM1-NetworkInterface","type":"Microsoft.Network/networkInterfaces","apiVersion":"2020-11-01","location":"[resourceGroup().location]","dependsOn":["[resourceId('Microsoft.Network/publicIPAddresses', 'ubuntuVM1-PublicIP')]","[resourceId('Microsoft.Network/virtualNetworks', 'ubuntuVM1-VirtualNetwork')]"],"tags":{"displayName":"ubuntuVM1-NetworkInterface"},"properties":{"ipConfigurations":[{"name":"ipConfig1","properties":{"privateIPAllocationMethod":"Dynamic","publicIPAddress":{"id":"[resourceId('Microsoft.Network/publicIPAddresses', 'ubuntuVM1-PublicIP')]"},"subnet":{"id":"[resourceId('Microsoft.Network/virtualNetworks/subnets', 'ubuntuVM1-VirtualNetwork', 'ubuntuVM1-VirtualNetwork-Subnet')]"}}}]}},{"name":"ubuntuVM1","type":"Microsoft.Compute/virtualMachines","apiVersion":"2021-03-01","location":"[resourceGroup().location]","dependsOn":["[resourceId('Microsoft.Network/networkInterfaces', 'ubuntuVM1-NetworkInterface')]"],"tags":{"displayName":"ubuntuVM1"},"properties":{"hardwareProfile":{"vmSize":"Standard_A2_v2"},"osProfile":{"computerName":"ubuntuVM1","adminUsername":"adminUsername","adminPassword":"adminPassword"},"storageProfile":{"imageReference":{"publisher":"Canonical","offer":"UbuntuServer","sku":"16.04-LTS","version":"latest"},"osDisk":{"name":"ubuntuVM1-OSDisk","caching":"ReadWrite","createOption":"FromImage"}},"networkProfile":{"networkInterfaces":[{"id":"[resourceId('Microsoft.Network/networkInterfaces', 'ubuntuVM1-NetworkInterface')]"}]},"diagnosticsProfile":{"bootDiagnostics":{"enabled":true,"storageUri":"[reference(resourceId('Microsoft.Storage/storageAccounts/', toLower('ubuntuVM1storage'))).primaryEndpoints.blob]"}}}}],"outputs":{}}

# Create a new template

===========================
*/

/*
Copy and paste, or send the text above to a GPT3 model, to receive a response like this:

{"$schema":"https://schema.management.azure.com/schemas/2019-04-01/deploymentTemplate.json#","contentVersion":"1.0.0.0","parameters":{},"functions":[],"variables":{},"resources":[],"outputs":{}}
*/

/*
 ... and you can further interact with more commands, e.g.
 
 # Add a variable
 # Another one
 # Add a network adapter
 # Add a linux VM
 
 etc.
*/

class Program
{
    static void Main(string[] args)
    {
        // Prepare the engine
        var settings = new Settings
        {
            Description = "Generate and edit a JSON ARM template following my requests.",
            InputPrefix = "# ",
            Examples = new[]
            {
                new Interaction
                {
                    Input = "Create an empty template",
                    Output = JsonFileMinimized("examples_basic.json")
                },
                new Interaction
                {
                    Input = "Add some storage",
                    Output = JsonFileMinimized("examples_storage.json")
                },
                new Interaction
                {
                    Input = "Add a parameter with name 'myparameter'",
                    Output = JsonFileMinimized("examples_parameters.json")
                },
                new Interaction
                {
                    Input = "Add a variable with name 'variable2'",
                    Output = JsonFileMinimized("examples_variables.json")
                },
                new Interaction
                {
                    Input = "Add a Ubuntu VM",
                    Output = JsonFileMinimized("examples_ubuntuvm.json")
                }
            }
        };
        var promptEngine = new GenericEngine(settings);

        // Use the engine to answer a new question
        var prompt = promptEngine.Render("Create a new template");

        Console.WriteLine("===========================");
        Console.WriteLine(prompt);
        Console.WriteLine("===========================");

        // Send the prompt to OpenAI / Azure OpenAI to generate a custom ARM template
        // ...
    }

    static string JsonFileMinimized(string file)
    {
        if (!File.Exists(file)) throw new Exception($"File not found: {file}");
        var json = File.ReadAllText(file);
        return JsonConvert.SerializeObject(JsonConvert.DeserializeObject(json), Formatting.None);
    }
}
