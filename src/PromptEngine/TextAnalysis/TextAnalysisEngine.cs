// Copyright (c) Microsoft. All rights reserved.

using Microsoft.AI.PromptEngine.Generic;

namespace Microsoft.AI.PromptEngine.TextAnalysis;

/// <summary>
/// A simple engine to generate a prompt containing some content and a query on the content
/// provided. The query can be prepended or appended at the end (default).
/// </summary>
public class TextAnalysisEngine : GenericEngine
{
    private readonly string text;
    private readonly bool queryBeforeText;

    /// <summary>Constructor</summary>
    /// <param name="text">Content to analyze</param>
    /// <param name="queryBeforeText">Whether to insert the query before the content or after (default)</param>
    public TextAnalysisEngine(string text, bool queryBeforeText = false) : base()
    {
        if (text == null) text = "";
        text = text.Trim();

        this.text = text;
        this.queryBeforeText = queryBeforeText;

        if (!queryBeforeText) this.settings.Description = text;
    }

    public override IPrompt Render(string query = null)
    {
        if (string.IsNullOrEmpty(query)) return new Prompt("");
        query = query.Trim();

        if (this.queryBeforeText)
        {
            this.settings.Description = query;
            return base.Render(this.text);
        }

        return base.Render(query);
    }
}
