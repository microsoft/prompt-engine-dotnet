// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using Microsoft.AI.PromptEngine;
using Microsoft.AI.PromptEngine.Generic;
using Xunit;

namespace PromptEngine.Test.Generic;

public class GenericEngineTest
{
    [Fact]
    public void ItRendersDescription()
    {
        // Arrange
        var target =
            new GenericEngine(new Settings { Description = "Some description" });

        // Act
        IPrompt result = target.Render();

        // Assert
        Assert.Equal("Some description\n\n", result.ToString());
    }

    [Fact]
    public void ItRendersNoDescription()
    {
        // Arrange
        var target = new GenericEngine(new Settings { Description = "" });

        // Act
        IPrompt result = target.Render();

        // Assert
        Assert.Equal(String.Empty, result.ToString());
    }

    [Theory]
    [InlineData(null, null, null, "")]
    [InlineData("", "", "", "")]
    [InlineData("", "desc", "", "desc\n\n")]
    [InlineData("/*", "desc", "", "/*desc\n\n")]
    [InlineData("", "desc", "*/", "desc*/\n\n")]
    [InlineData("/*", "desc", "*/", "/*desc*/\n\n")]
    [InlineData("/*", "", "*/", "/**/\n\n")]
    public void ItRendersDescriptionPreAndPostfix(string prefix, string description, string postfix,
        string expected)
    {
        // Arrange
        var target = new GenericEngine(new Settings
        {
            Description = description, DescriptionPrefix = prefix, DescriptionPostfix = postfix,
        });

        // Act
        IPrompt result = target.Render();

        // Assert
        Assert.Equal(expected, result.ToString());
    }

    [Fact]
    public void ItRendersWithNullExamples()
    {
        // Arrange
        var target = new GenericEngine(new Settings { Description = "Tell a joke", Examples = null, });

        // Act
        IPrompt result = target.Render();

        // Assert
        Assert.Equal("Tell a joke\n\n", result.ToString());
    }

    [Fact]
    public void ItRendersExamples()
    {
        // Arrange
        const string S1 = "Tell a joke";
        const string S2 = "Why couldn't the bicycle stand up by itself? Because it was...two tired!";
        var target =
            new GenericEngine(new Settings { Examples = new[] { new Interaction { Input = S1, Output = S2 } }, });

        // Act
        IPrompt result = target.Render();

        // Assert
        Assert.Equal($"{S1}\n{S2}\n\n", result.ToString());
    }

    [Fact]
    public void ItRendersDescriptionAndExamples()
    {
        // Arrange
        const string S0 = "Be a great comedian";
        const string S1 = "Tell a joke";
        const string S2 = "Why couldn't the bicycle stand up by itself? Because it was...two tired!";
        var target = new GenericEngine(new Settings
        {
            Description = S0, Examples = new[] { new Interaction { Input = S1, Output = S2 } },
        });

        // Act
        IPrompt result = target.Render();

        // Assert
        Assert.Equal($"{S0}\n\n{S1}\n{S2}\n\n", result.ToString());
    }

    [Fact]
    public void ItRendersDescriptionAndExamplesWithCustomSeparators()
    {
        // Arrange
        const string S0 = "Be a great comedian";
        const string S1 = "Tell a joke";
        const string S2 = "Why couldn't the bicycle stand up by itself? Because it was...two tired!";
        const string SEP1 = "--------------------------";
        const string SEP2 = ": ";
        var target = new GenericEngine(new Settings
        {
            Description = S0,
            TextBlocksSeparator = SEP1,
            InputOutputSeparator = SEP2,
            Examples = new[] { new Interaction { Input = S1, Output = S2 } },
        });

        // Act
        IPrompt result = target.Render();

        // Assert
        Assert.Equal($"{S0}{SEP1}{S1}{SEP2}{S2}{SEP1}", result.ToString());
    }

    [Fact]
    public void ItUsesAllSettings()
    {
        // Arrange
        const string DESC = "Some\ndescription";
        const string DESC_PRE = "[[";
        const string DESC_POST = "]]\n\n";
        const string TEXT_BLOCK_SEP = "\n----\n";
        const string INPUT_PRE = "/** ";
        const string INPUT_POST = " */[foo]\n\n";
        const string OUTPUT_PRE = "{{";
        const string OUTPUT_POST = "}}\n";
        const string IN_OUT_SEP = "\r\n\r\n";
        const string EX_IN_1 = "hello";
        const string EX_OUT_1 = "world";
        const string EX_IN_2 = "hello!";
        const string EX_OUT_2 = "world!";
        const string RESET_SEP = "\r\n\r\n";
        var settings = new Settings
        {
            PromptMaxLength = 0,
            Description = DESC,
            DescriptionPrefix = DESC_PRE,
            DescriptionPostfix = DESC_POST,
            TextBlocksSeparator = TEXT_BLOCK_SEP,
            InputPrefix = INPUT_PRE,
            InputPostfix = INPUT_POST,
            OutputPrefix = OUTPUT_PRE,
            OutputPostfix = OUTPUT_POST,
            InputOutputSeparator = IN_OUT_SEP,
            Examples = new Interaction[]
            {
                new() { Input = EX_IN_1, Output = EX_OUT_1 }, new() { Input = EX_IN_2, Output = EX_OUT_2 }
            },
            ContextResetText = RESET_SEP
        };
        string userInput = Guid.NewGuid().ToString();

        // Act
        var target = new GenericEngine(settings);
        IPrompt result = target.Render(userInput);

        // Assert
        Assert.Equal(
            $"{DESC_PRE}{DESC}{DESC_POST}" +
            $"{TEXT_BLOCK_SEP}" +
            $"{INPUT_PRE}{EX_IN_1}{INPUT_POST}" +
            $"{IN_OUT_SEP}" +
            $"{OUTPUT_PRE}{EX_OUT_1}{OUTPUT_POST}" +
            $"{TEXT_BLOCK_SEP}" +
            $"{INPUT_PRE}{EX_IN_2}{INPUT_POST}" +
            $"{IN_OUT_SEP}" +
            $"{OUTPUT_PRE}{EX_OUT_2}{OUTPUT_POST}" +
            $"{TEXT_BLOCK_SEP}" +
            $"{RESET_SEP}" +
            $"{TEXT_BLOCK_SEP}" +
            $"{INPUT_PRE}{userInput}{INPUT_POST}" +
            $"{IN_OUT_SEP}" +
            $"{OUTPUT_PRE}",
            result.ToString());
    }
}
