# Remarks, assumptions and limitations:

1. The word_counter endpoint handle the three different types of string using an enum (called StringType). This could also be achieved using a query parameter or by analyzing the string (the more cumbersome way).
2. The word_counter endpoint is meant to handle sentences as shown in the exercise’s description.
When testing the URL implementation the response was in JSON format with escape characters. I solve it in the code, but still - there might be so other cases which would require more specific solution.
3. input size - I use stream reader to read the string line by line to not overload the app’s RAM memory in case of a very large string. Specifically in the case of a simple string that is sent as part of the web request - I did not find a way within a reasonable time limit for the exercise to split the request into chunks, as I know is possible in Node.js for example.
4. The results are persistent and can be found in a Sqlite DB (retrain.db) located in the root folder. You can easily view its content using an sqlite db browser: https://sqlitebrowser.org/
5. a. when the input is read from a file, an absolute path to the file is expected. I created a simple text file in the root folder called “some-text”. In my case I accessed it using “/Users/kobi/Projects/Retrain/Retrain/some-text.txt”. Please change it accordingly when you send the request.
b. When the input is read from a URL I used this free endpoint that returns a JSON response (reminder - note #2):
https://catfact.ninja/fact
6. To test the endpoints you can simply run the VS solution and a Swagger window will show up.
7. I focused in this assignment on making the functionality work, taking into account the requirements and limitations and design it as cleanly as possible with OOP principles and design patterns. Testing is important, but I preferred not to exceed the time I could dedicate for this exercise.
If this is a crucial part for the scope of this exercise - please let me know.
8. If you have any questions or something doesn’t work properly (SHOULD NOT HAPPEN :)) - please let me know.

For simplicity sake:
1. I created a general, simplistic exception handler in the Program.cs file for exceptions that are thrown in the project.
2. I did not create several projects to separate the n-tier app, but made the separation in different folders within the same project (also, in a real world app I’d separate the interfaces from the implementations).
