﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Microsoft.AI.PromptEngine.Generic;

/// <summary>
/// A generic prompt engine with all the raw features. It allows to compose a prompt
/// for LLM with descriptions, examples, custom syntax, etc. See the included examples
/// and unit tests for details.
/// </summary>
public class GenericEngine : IPromptEngine
{
    protected Settings settings;

    // Input+Output strings from a real runtime conversation between human and AI
    // Dialog interactions are added at the end of the prompt, and help the AI
    // to keep context/memory about the ongoing human-AI interactions.
    protected Interaction[] Dialog { get; set; } = { };

    /// <summary>Constructor</summary>
    public GenericEngine()
    {
        this.settings = new Settings();
    }

    /// <summary>Constructor</summary>
    /// <param name="settings">Options defining how to generate prompts, e.g. syntax, separators, examples, etc.</param>
    public GenericEngine(Settings settings)
    {
        this.settings = settings;
    }

    /// <summary>Instance factory using YAML settings</summary>
    /// <param name="yamlSettingsFile"></param>
    /// <returns>Engine instance</returns>
    public static GenericEngine FromYamlSettings(string yamlSettingsFile)
    {
        if (!File.Exists(yamlSettingsFile)) throw new Exception($"YAML configuration not found: {yamlSettingsFile}");

        var yamlDeserializer = new DeserializerBuilder()
            .WithNamingConvention(PascalCaseNamingConvention.Instance)
            .Build();
        Settings settings = yamlDeserializer.Deserialize<Settings>(File.ReadAllText(yamlSettingsFile));
        return new GenericEngine(settings);
    }

    /// <summary>Generate a prompt using the settings provided input</summary>
    /// <param name="input">Optional input to include in the prompt</param>
    /// <returns>Generated prompt, ready to be sent to a LLM model</returns>
    public virtual IPrompt Render(string input = null)
    {
        var builder = new StringBuilder();

        this.AddDescription(builder);
        this.AddExamples(builder);
        this.AddDialog(builder);
        this.AddInput(builder, input);

        var prompt = new Prompt(builder.ToString());
        return prompt;
    }

    protected void AddDescription(StringBuilder builder)
    {
        if (!string.IsNullOrEmpty(this.settings.DescriptionPrefix))
            builder.Append(this.settings.DescriptionPrefix);

        if (!string.IsNullOrEmpty(this.settings.Description))
            builder.Append(this.settings.Description);

        if (!string.IsNullOrEmpty(this.settings.DescriptionPostfix))
            builder.Append(this.settings.DescriptionPostfix);

        if (builder.Length > 0)
            builder.Append(this.settings.TextBlocksSeparator);
    }

    protected void AddExamples(StringBuilder builder)
    {
        this.AddInteractions(builder, this.settings.Examples, this.settings.ContextResetText);
    }

    protected void AddDialog(StringBuilder builder)
    {
        this.AddInteractions(builder, this.Dialog, null);
    }

    private void AddInteractions(StringBuilder builder, Interaction[] interactions, string endOfInteractionsText)
    {
        if (interactions == null || interactions.Length == 0) return;

        var jsonOutput = (string.Compare(this.settings.OutputFormat, "json", StringComparison.InvariantCultureIgnoreCase) == 0);

        foreach (var example in interactions)
        {
            if (!string.IsNullOrEmpty(example.Input))
            {
                if (!string.IsNullOrEmpty(this.settings.InputPrefix))
                    builder.Append(this.settings.InputPrefix);

                builder.Append(example.Input);

                if (!string.IsNullOrEmpty(this.settings.InputPostfix))
                    builder.Append(this.settings.InputPostfix);

                if (!string.IsNullOrEmpty(example.Output) || example.OutputValues != null)
                {
                    builder.Append(this.settings.InputOutputSeparator);

                    if (!string.IsNullOrEmpty(this.settings.OutputPrefix))
                        builder.Append(this.settings.OutputPrefix);

                    if (!string.IsNullOrEmpty(example.Output))
                    {
                        builder.Append(jsonOutput ? JsonConvert.SerializeObject(example.Output) : example.Output);
                    }
                    else if (example.OutputValues != null)
                    {
                        if (jsonOutput)
                        {
                            builder.Append(JsonConvert.SerializeObject(example.OutputValues));
                        }
                        else
                        {
                            foreach (KeyValuePair<string, string> values in example.OutputValues)
                            {
                                builder.Append(values.Key);
                                builder.Append(this.settings.MultipleTextKeyValueSeparator);
                                builder.Append(values.Value);
                                builder.Append(this.settings.MultipleTextValuesSeparator);
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(this.settings.OutputPostfix))
                        builder.Append(this.settings.OutputPostfix);
                }

                builder.Append(this.settings.TextBlocksSeparator);
            }
            else if (!string.IsNullOrEmpty(example.Output))
            {
                builder.Append(example.Output);
                builder.Append(this.settings.TextBlocksSeparator);
            }
        }

        if (!string.IsNullOrEmpty(endOfInteractionsText))
        {
            builder.Append(endOfInteractionsText);
            builder.Append(this.settings.TextBlocksSeparator);
        }
    }

    private void AddInput(StringBuilder builder, string input)
    {
        if (!string.IsNullOrEmpty(input))
        {
            if (!string.IsNullOrEmpty(this.settings.InputPrefix))
                builder.Append(this.settings.InputPrefix);

            builder.Append(input);

            if (!string.IsNullOrEmpty(this.settings.InputPostfix))
                builder.Append(this.settings.InputPostfix);

            builder.Append(this.settings.InputOutputSeparator);
        }

        if (!string.IsNullOrEmpty(this.settings.OutputPrefix))
            builder.Append(this.settings.OutputPrefix);
    }
}
