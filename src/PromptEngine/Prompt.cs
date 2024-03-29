﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Microsoft.AI.PromptEngine;

public interface IPrompt
{
    string ToString();
}

/// <summary>
/// The prompt generated by engine's Render method.
/// </summary>
public class Prompt : IPrompt
{
    private readonly string value;

    public Prompt(string value)
    {
        this.value = value;
    }

    public override string ToString()
    {
        return this.value;
    }
}
