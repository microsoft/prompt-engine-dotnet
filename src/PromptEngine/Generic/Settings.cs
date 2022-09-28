// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace Microsoft.AI.PromptEngine.Generic;

public class Settings
{
    // How many tokens (or chars) the selected model can handle.
    // Often this will depend on the selected AI model, so the value
    // might be overridden when integrating with a specific model.
    public int PromptMaxLength { get; set; } = 1024;

    // An optional description added at the beginning of the prompt
    public string Description { get; set; }

    // When the engine description is not empty, this text is added as a prefix
    // e.g. to start a code comment, e.g. '/*' '#' etc.
    public string DescriptionPrefix { get; set; }

    // When the engine description is not empty, this text is added at the end
    // e.g. to close a code comment, e.g. '*/'
    public string DescriptionPostfix { get; set; }

    // The character used to separate descriptions, examples, etc.
    public string TextBlocksSeparator { get; set; } = "\n\n";

    // Optional text automatically inserted before the input, both examples and user
    // interactions. This might be a User name for a chat, a comment start operator
    // such as '/*' in case of code generation, etc.
    public string InputPrefix { get; set; }

    // Optional text automatically inserted after the input, both examples and user
    // interactions. This might be a comment close operator such '*/' in case of code
    // generation, etc.
    public string InputPostfix { get; set; }

    // Optional text automatically inserted before the output, both examples and user
    // interactions. This might be a Bot name for a chat where we define the bot response,
    // etc.
    public string OutputPrefix { get; set; }

    // Optional text automatically inserted after the output, both examples and user
    // interactions. This might be a '.' (dot) to end sentences, etc.
    public string OutputPostfix { get; set; }

    // Optional format, supported values are "text" and "json".
    public string OutputFormat { get; set; } = "text";

    // What character to use to separate input from output.
    public string InputOutputSeparator { get; set; } = "\n";

    // What character to use to separate the name of multiple output values (only for text format).
    public string MultipleTextKeyValueSeparator { get; set; } = ": ";

    // What character to use to separate multiple output values (only for text format).
    public string MultipleTextValuesSeparator { get; set; } = "\n";

    // Input+Output examples, telling the AI model what kind of output we expect
    // to be generated. If the list is too long and exceeds the model max tokens,
    // some examples are automatically excluded. The logic used to exclude exceeding
    // examples might depend, e.g. the library might exclude similar ones, or examples
    // that are too long, or simply take a few from the top, etc.
    public Interaction[] Examples { get; set; } = { };

    // Optional text used to separate examples from real turn-based interactions
    // e.g. what comes before this text are given as examples, defined by `Examples`,
    // and what comes after depends on the previous user interactions. This is useful,
    // for instance, to "reset" the model "memory", to inform the model that what comes
    // before do not provide "context".
    public string ContextResetText { get; set; }

    public static Settings Empty = new Settings
    {
        PromptMaxLength = 0,
        Description = null,
        DescriptionPrefix = null,
        DescriptionPostfix = null,
        TextBlocksSeparator = null,
        InputPrefix = null,
        InputPostfix = null,
        OutputPrefix = null,
        OutputPostfix = null,
        InputOutputSeparator = null,
        Examples = null,
        ContextResetText = null
    };
}
