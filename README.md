# Prompt-Engine

This package provides an easy and reusable interface to build prompts for large scale language
models (LLMs).

Prompt Engineering is a technique used to elicit intended responses out of a LLM model and can work
on many strategies.

This library is built on the strategy described in this
[Microsoft Prompt Engineering](https://microsoft.github.io/prompt-engineering/)
article wherein you are provided a high level description, some examples, and user interactions to
help the model understand what to produce and to keep track of what it already has produced. 

As new user interactions keep coming in, the old ones are cycled out based on the `max_tokens` set
in the ModelConfig. This ensures that the context of the model does not get too big and keeps
generating meaningful responses throughout its tenure.  

## Requirements

[...] *TODO*

## Install

[...] *TODO*

## Simple Demo

### Code

[...] *TODO*

### Output

[...] *TODO*

## Prompt Engineering and Context Files

To generate meaningful output out of a large scale language model, you need to provide it with an
equally descriptive prompt to coax the intended behavior. 

A good way to achieve that is by providing helpful examples to the LLM in the form of query-answer
interaction pairs. 

This is an example from the
[Codex-CLI](https://github.com/microsoft/Codex-CLI)
library following the above principle

```bash
# what's the weather in Venice?
curl wttr.in/Venice

# add temp files to git ignore
echo "*.tmp" >> .gitignore

# open it in vs code
code .
```

## Available Functions

[...] *TODO*

## Adding more info to your contexts

[...] *TODO*

## Contributing

This project welcomes contributions and suggestions.  Most contributions require you to agree to a
Contributor License Agreement (CLA) declaring that you have the right to, and actually do, grant us
the rights to use your contribution. For details, visit https://cla.opensource.microsoft.com.

When you submit a pull request, a CLA bot will automatically determine whether you need to provide
a CLA and decorate the PR appropriately (e.g., status check, comment). Simply follow the instructions
provided by the bot. You will only need to do this once across all repos using our CLA.

This project has adopted the
[Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct).
For more information see the
[Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq)
or contact
[opencode@microsoft.com](mailto:opencode@microsoft.com)
with any additional questions or comments.

## Trademarks

This project may contain trademarks or logos for projects, products, or services. Authorized use of
Microsoft trademarks or logos is subject to and must follow 
[Microsoft's Trademark & Brand Guidelines](https://www.microsoft.com/legal/intellectualproperty/trademarks/usage/general).
Use of Microsoft trademarks or logos in modified versions of this project must not cause confusion
or imply Microsoft sponsorship.
Any use of third-party trademarks or logos are subject to those third-party's policies.
