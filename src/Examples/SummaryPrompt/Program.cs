// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using Microsoft.AI.PromptEngine.Generic;

/*
Output (word-wrapped for readability):

===========================
Summarize the text:

Albert Einstein (/ˈaɪnstaɪn/ EYEN-styne; German: [ˈalbɛʁt ˈʔaɪnʃtaɪn]; 14 March 1879 – 18 April 1955) was a 
German-born theoretical physicist, widely acknowledged to be one of the greatest and most influential physicists 
of all time. Einstein is best known for developing the theory of relativity, but he also made important contributions 
to the development of the theory of quantum mechanics. Relativity and quantum mechanics are together the two pillars 
of modern physics. His mass–energy equivalence formula E = mc2, which arises from relativity theory, has been dubbed 
"the world's most famous equation". His work is also known for its influence on the philosophy of science. He 
received the 1921 Nobel Prize in Physics "for his services to theoretical physics, and especially for his discovery 
of the law of the photoelectric effect", a pivotal step in the development of quantum theory. His intellectual 
achievements and originality resulted in "Einstein" becoming synonymous with "genius".

===========================
*/

/*
Copy and paste, or send the text above to a GPT3 model, to receive a response like this:

Albert Einstein was a German-born theoretical physicist who is widely considered to be one of the greatest 
physicists of all time. He is best known for developing the theory of relativity, but he also made important 
contributions to the development of quantum mechanics. His mass-energy equivalence formula E = mc2 is one of the most 
famous equations in the world. He received the Nobel Prize in Physics in 1921 for his work in theoretical physics.
*/

class Program
{
    static void Main(string[] args)
    {
        // Prepare the engine
        var settings = new Settings { Description = "Summarize the text:", };
        var promptEngine = new GenericEngine(settings);

        // Use the engine to summarize some text
        var prompt = promptEngine.Render(
            "Albert Einstein (/ˈaɪnstaɪn/ EYEN-styne; German: [ˈalbɛʁt ˈʔaɪnʃtaɪn]; 14 March 1879 – 18 " +
            "April 1955) was a German-born theoretical physicist, widely acknowledged to be one of the greatest " +
            "and most influential physicists of all time. Einstein is best known for developing the theory of " +
            "relativity, but he also made important contributions to the development of the theory of quantum " +
            "mechanics. Relativity and quantum mechanics are together the two pillars of modern physics. His " +
            "mass–energy equivalence formula E = mc2, which arises from relativity theory, has been dubbed " +
            "\"the world's most famous equation\". His work is also known for its influence on the philosophy " +
            "of science. He received the 1921 Nobel Prize in Physics \"for his services to theoretical physics, " +
            "and especially for his discovery of the law of the photoelectric effect\", a pivotal step in the " +
            "development of quantum theory. His intellectual achievements and originality resulted in " +
            "\"Einstein\" becoming synonymous with \"genius\".");

        Console.WriteLine("===========================");
        Console.WriteLine(prompt);
        Console.WriteLine("===========================");

        // Send the prompt to OpenAI / Azure OpenAI to get the summary
        // ...
    }
}
