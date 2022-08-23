// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using Microsoft.AI.PromptEngine;
using Microsoft.AI.PromptEngine.Generic;

/*
Output:

===========================
Generate and edit Mermaid graphs following my instructions.

# Create a flow chart from left to right
flowchart LR

# Create a flow chart with a square box A with label 'John'
flowchart LR
    A[John]

# A flow chart with John connected to a box B with label 'Mark'
flowchart LR
    A[John] --> B[Mark]

# A flow chart where Mark box is round
flowchart LR
    A[John] --> B(Mark)

# A flow chart with a connection between John and Mark labelled 'Knows'
flowchart LR
    A[John] -->|Knows| B(Mark)

# A flow chart showing that John knows Mark, with a 'Some Decision' decision box from B to C
flowchart LR
    A[John] -->|Knows| B(Mark)
    B --> C{Decision}

# Create a sequence diagram with Alice and John services, and a loop
sequenceDiagram
    Alice->>John: Hello John, how are you?
    loop Healthcheck
        John->>John: Fight against hypochondria
    end

# Create a pie chart with 386 dogs, 85.9 cats and 15 rats
pie
    "Dogs" : 386
    "Cats" : 85.9
    "Rats" : 15

# Create a new flow chart with an input box connected to a decision box connected to B and C options

===========================
*/

/*
Copy and paste, or send the text above to a GPT3 model, to receive a response like this:

flowchart LR
    A[Input] --> B{Decision}
    B --> C
    B --> D
*/

namespace MermaidGraphPrompt
{
    class Program
    {
        static void Main(string[] args)
        {
            // Prepare the engine
            var settings = new Settings
            {
                Description = "Generate and edit Mermaid graphs following my instructions.",
                InputPrefix = "# ",
                Examples = new[]
                {
                    new Interaction
                    {
                        Input = "Create a flow chart from left to right",
                        Output = "flowchart LR"
                    },

                    new Interaction
                    {
                        Input = "Create a flow chart with a square box A with label 'John'",
                        Output = "flowchart LR\n" +
                                 "    A[John]"
                    },

                    new Interaction
                    {
                        Input = "A flow chart with John connected to a box B with label 'Mark'",
                        Output = "flowchart LR\n" +
                                 "    A[John] --> B[Mark]"
                    },

                    new Interaction
                    {
                        Input = "A flow chart where Mark box is round",
                        Output = "flowchart LR\n" +
                                 "    A[John] --> B(Mark)"
                    },

                    new Interaction
                    {
                        Input = "A flow chart with a connection between John and Mark labelled 'Knows'",
                        Output = "flowchart LR\n" +
                                 "    A[John] -->|Knows| B(Mark)"
                    },

                    new Interaction
                    {
                        Input =
                            "A flow chart showing that John knows Mark, with a 'Some Decision' decision box from B to C",
                        Output = "flowchart LR\n" +
                                 "    A[John] -->|Knows| B(Mark)\n" +
                                 "    B --> C{Decision}"
                    },
                    new Interaction
                    {
                        Input = "Create a sequence diagram with Alice and John services, and a loop",
                        Output = "sequenceDiagram\n" +
                                 "    Alice->>John: Hello John, how are you?\n" +
                                 "    loop Healthcheck\n" +
                                 "        John->>John: Fight against hypochondria\n" +
                                 "    end"
                    },
                    new Interaction
                    {
                        Input = "Create a pie chart with 386 dogs, 85.9 cats and 15 rats",
                        Output = "pie\n" +
                                 "    \"Dogs\" : 386\n" +
                                 "    \"Cats\" : 85.9\n" +
                                 "    \"Rats\" : 15"
                    },
                }
            };
            var promptEngine = new GenericEngine(settings);

            // Use the engine to answer a new question
            var prompt =
                promptEngine.Render(
                    "Create a new flow chart with an input box connected to a decision box connected to B and C options");

            Console.WriteLine("===========================");
            Console.WriteLine(prompt);
            Console.WriteLine("===========================");

            // Send the prompt to OpenAI / Azure OpenAI to generate a custom ARM template
            // ...
        }
    }
}
