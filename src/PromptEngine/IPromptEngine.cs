// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Microsoft.AI.PromptEngine;

public interface IPromptEngine
{
    IPrompt Render(string input = null);
}
