// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Microsoft.AI.PromptEngine.TextAnalysis;
using Xunit;

namespace PromptEngine.Test.TextAnalysis;

public class TextAnalysisEngineTest
{
    [Fact]
    public void ItQueriesAfterContent()
    {
        // Arrange
        const string CONTENT = "some long content";
        const string QUERY = "summarize it";
        var target = new TextAnalysisEngine(CONTENT);

        // Act
        var result = target.Render(QUERY);

        // Assert
        Assert.Equal($"{CONTENT}\n\n{QUERY}\n", result.ToString());
    }

    [Fact]
    public void ItQueriesAfterContentOnDemand()
    {
        // Arrange
        const string CONTENT = "some long content";
        const string QUERY = "summarize it";
        var target = new TextAnalysisEngine(CONTENT, queryBeforeText: false);

        // Act
        var result = target.Render(QUERY);

        // Assert
        Assert.Equal($"{CONTENT}\n\n{QUERY}\n", result.ToString());
    }

    [Fact]
    public void ItCanQueryBeforeContent()
    {
        // Arrange
        const string CONTENT = "some long content";
        const string QUERY = "summarize it";
        var target = new TextAnalysisEngine(CONTENT, queryBeforeText: true);

        // Act
        var result = target.Render(QUERY);

        // Assert
        Assert.Equal($"{QUERY}\n\n{CONTENT}\n", result.ToString());
    }

    [Fact]
    public void ItTrimsContentAndQuery()
    {
        // Arrange
        const string CONTENT = "some long content";
        const string QUERY = "summarize it";
        const string CONTENT2 = $"\n  {CONTENT}\n \n \t\r";
        const string QUERY2 = $" \n \n{QUERY}\n \n\t \r";
        var target = new TextAnalysisEngine(CONTENT2);

        // Act
        var result = target.Render(QUERY2);

        // Assert
        Assert.Equal($"{CONTENT}\n\n{QUERY}\n", result.ToString());
    }
}
