// Copyright (c) Microsoft. All rights reserved.

namespace Microsoft.AI.PromptEngine;

public interface IPromptEngine
{
    IPrompt Render(string input = null);
}
