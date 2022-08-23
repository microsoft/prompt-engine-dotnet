// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Microsoft.AI.PromptEngine;

/// <summary>
/// Data model used to describe the expected output for a given input, also used to track
/// real interactions, e.g. 1:1 chats between a user (input) and a bot (output).
/// </summary>
public class Interaction
{
    // The text given in input to generate the following output.
    // This might be a sentence from a user, a long text to summarize,
    // a comment describing some code, etc.  
    public string Input { get; set; }
        
    // The text generated with the given input, for instance a bot reply,
    // a summary, code matching the input comment, etc.
    public string Output { get; set; }
}
