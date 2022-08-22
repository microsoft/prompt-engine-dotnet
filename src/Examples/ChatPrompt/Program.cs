// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using Microsoft.AI.PromptEngine;
using Microsoft.AI.PromptEngine.Generic;

/*
Output:

===========================
Quark is a friendly bot knowledgeable about geography.

Devis: Where is Rome?
Quark: Rome is a beautiful town in Italy, the capital of Italy in fact.

Devis: How long is the Nile river?
Quark: The Nile River flows over 6600 kilometers (4100 miles) into the Mediterranean Sea.

Devis: What's the smallest country in the world?
Quark: Vatican City is the smallest country in the world, measuring just 0.2 square miles, almost 120 times smaller than the island of Manhattan.

Devis: Where is the saltiest lake in the world?
Quark: The Gaet'ale Pond in Ethiopia is the saltiest water body in the world with a salinity of 43%.

Forget the earlier conversation and start afresh.

Devis: How many towns are there in the world?
Quark: 
===========================
*/

/*
Copy and paste, or send the text above to a GPT3 model, to receive a response like this:

There's no definitive answer to this question as it depends on how you define a town. However, according to the 
World Gazetteer, there are over 100,000 towns and cities in the world.
*/

class Program
{
    static void Main(string[] args)
    {
        // Prepare the engine
        var settings = new Settings
        {
            Description = "Quark is a friendly bot knowledgeable about geography.",
            InputPrefix = "Devis: ",
            OutputPrefix = "Quark: ",
            Examples = new[]
            {
                new Interaction
                {
                    Input = "Where is Rome?",
                    Output = "Rome is a beautiful town in Italy, the capital of Italy in fact."
                },
                new Interaction
                {
                    Input = "How long is the Nile river?",
                    Output = "The Nile River flows over 6600 kilometers (4100 miles) into the Mediterranean Sea."
                },
                new Interaction
                {
                    Input = "What's the smallest country in the world?",
                    Output = "Vatican City is the smallest country in the world, measuring just 0.2 square miles, almost 120 times smaller than the island of Manhattan."
                },
                new Interaction
                {
                    Input = "Where is the saltiest lake in the world?",
                    Output = "The Gaet'ale Pond in Ethiopia is the saltiest water body in the world with a salinity of 43%."
                }
            },
            ContextResetText = "Forget the earlier conversation and start afresh.",
        };
        var promptEngine = new GenericEngine(settings);

        // Use the engine to answer a new question
        var prompt = promptEngine.Render("How many towns are there in the world?");

        Console.WriteLine("===========================");
        Console.WriteLine(prompt);
        Console.WriteLine("===========================");

        // Send the prompt to OpenAI / Azure OpenAI to get the answer
        // ...
    }
}
