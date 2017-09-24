# Tennis Scores
A client for reading Tennis scores from an input file and outputting them. Featuring NUnit, FluentAssertions,  NSubstitute and AutoFixture.

#### Restoring
> `Update-Package -reinstall`

If you have any issues with the NuGet restore run `Update-Package -reinstall` from the Package Manager Console to re-path the hintpaths in the .csproj files. (This may not be an issue with VS2017)

## Preface

This exercise is designed to demonstrate the skills a candidate has learned for writing clear and readable programs which model
complex problem domains and which can be easily modified in the face of changing requirements. These skills are critically important for the role.

Neither the size of the submitted program nor the time taken to complete the exercise are considered to be important, but to give
an estimate it should take candidates approximately one hour and involve writing approximately 200 lines of code, excluding tests, comments, punctuation and whitespace.

Maximum credit will be given for a program which is clearly written with minimal duplication and which demonstrates good object-oriented design practice including the use of appropriate design patterns. Part of the interview process will be a review of the submitted program, and topics for discussion will include justifying particular design decisions and testing strategies, and demonstrating how the program may be extended to handle new requirements. The program must be the candidate's own work, and the interview will include questions to ensure that this is the case.

The submission may make use of any widely-available programming languages, although the use of C# is strongly encouraged. Credit will not be given for using third-party libraries to solve the problem: the ideal solution only uses library code for reading and writing files and for formatting text.

## Criteria

Problem description: Write a program which reads in descriptions of tennis matches and displays the current score in each match.

The program should take two arguments on the command line: respectively the name of an input file and the name of an output file.

Each line of the input file is a description of a tennis match between two players named 'A' and 'B', and comprises a sequence of 'A's and 'B's which indicates the winner of each point in the match in the order that they are played.

For each line in the input, the program should write a line to the output in the format:

[completed set scores] [score in current set] [score in current game]

For example: 3-6 6-4 0-2 0-15\
Where:
3-6 6-4: scores in completed sets
0-2: score in current set
0-15: points in current game

Each score is shown in the form 'n-m' where 'n' is the server's score and 'm' is the receiver's score. The server changes at the
end of each game, and player A serves first.

The score in points in the current game is omitted if it is 0-0.

The score in games in the current set is shown even if it is 0-0.

An advantage score is shown 40-A or A-40.

There is no tiebreak in any set.

An example input file, and the corresponding output file, is included. The submitted program is expected to produce a  character-for-character copy of this output file from this input file, so careful attention must be paid to whitespace characters.

The input file can be assumed to only comprise lines of sequences of 'A's and 'B's which form matches which have not been won by either player. It is not required to handle gracefully any other format of input.

The rules of tennis scoring may be found online at [1]. For this exercise, the match is won by the best of three sets and each set is won by two clear games. There is no tiebreak in any set.

Candidates unfamiliar with the rules of tennis may request an alternative exercise.
>Probably should have done this! I know nothing about Tennis!

[1] http://en.wikipedia.org/wiki/Tennis_score

### input

[Linebreak]\
A\
AA\
AAA\
B\
BB\
BBB\
BBBA\
BBBAA\
BBBAAA\
BBBAAAA\
BBBAAAAB\
BBBAAAABB\
AAAA\
BBBB\
BBBAAAABBB\
AAAAA\
AAAAABB\
AAAABBBB\
AAAABBBBAAAABBBB\
AAAABBBBAAAABBBBAAAABBBB\
AAAABBBBAAAABBBBAAAABBBBAAAABBBBAAAABBBB\
AAAABBBBAAAABBBBAAAABBBBAAAABBBBAAAABBBBAAAABBBB\
AAAABBBBAAAABBBBAAAABBBBAAAABBBBAAAAAAAA\
AAAABBBBAAAABBBBAAAABBBBAAAABBBBAAAABBBBAAAAAAAA\
AAAABBBBAAAABBBBAAAABBBBAAAABBBBAAAAAAAAA\
AAAABBBBAAAABBBBAAAABBBBAAAAAAAAAAAAA\
AAAABBBBAAAABBBBAAAABBBBAAAABBBBAAAABBBBAAAAAAAAA\
AAAABBBBAAAABBBBAAAABBBBAAAAAAAAAAAABBBBAAAABBBBAAAABBBBAAAABBBBAAAABBBBBBBBA

### Output

0-0\
0-0 15-0\
0-0 30-0\
0-0 40-0\
0-0 0-15\
0-0 0-30\
0-0 0-40\
0-0 15-40\
0-0 30-40\
0-0 40-40\
0-0 A-40\
0-0 40-40\
0-0 40-A\
0-1\
1-0\
1-0\
0-1 0-15\
0-1 30-15\
1-1\
2-2\
3-3\
5-5\
6-6\
6-4 0-0\
7-5 0-0\
6-4 0-0 15-0\
3-6 0-0 0-15\
7-5 0-0 15-0\
3-6 6-4 0-0 0-15

### Table Input / Output

| Number | First Server | Input            | Output                                                                        |
|--------|--------------|------------------|-------------------------------------------------------------------------------|
| 1      | A            | 0-0              |                                                                               |
| 2      | A            | 0-0 15-0         | A                                                                             |
| 3      | A            | 0-0 30-0         | AA                                                                            |
| 4      | A            | 0-0 40-0         | AAA                                                                           |
| 5      | A            | 0-0 0-15         | B                                                                             |
| 6      | A            | 0-0 0-30         | BB                                                                            |
| 7      | A            | 0-0 0-40         | BBB                                                                           |
| 8      | A            | 0-0 15-40        | BBBA                                                                          |
| 9      | A            | 0-0 30-40        | BBBAA                                                                         |
| 10     | A            | 0-0 40-40        | BBBAAA                                                                        |
| 11     | A            | 0-0 A-40         | BBBAAAA                                                                       |
| 12     | A            | 0-0 40-40        | BBBAAAAB                                                                      |
| 13     | A            | 0-0 40-A         | BBBAAAABB                                                                     |
| 14     | B            | 0-1              | AAAA                                                                          |
| 15     | B            | 1-0              | BBBB                                                                          |
| 16     | B            | 1-0              | BBBAAAABBB                                                                    |
| 17     | B            | 0-1 0-15         | AAAAA                                                                         |
| 18     | B            | 0-1 30-15        | AAAAABB                                                                       |
| 19     | A            | 1-1              | AAAABBBB                                                                      |
| 21     | A            | 2-2              | AAAABBBBAAAABBBB                                                              |
| 22     | A            | 3-3              | AAAABBBBAAAABBBBAAAABBBB                                                      |
| 23     | A            | 5-5              | AAAABBBBAAAABBBBAAAABBBBAAAABBBBAAAABBBB                                      |
| 24     | A            | 6-6              | AAAABBBBAAAABBBBAAAABBBBAAAABBBBAAAABBBBAAAABBBB                              |
| 25     | A            | 6-4 0-0          | AAAABBBBAAAABBBBAAAABBBBAAAABBBBAAAAAAAA                                      |
| 26     | A            | 7-5 0-0          | AAAABBBBAAAABBBBAAAABBBBAAAABBBBAAAABBBBAAAAAAAA                              |
| 27     | A            | 6-4 0-0 15-0     | AAAABBBBAAAABBBBAAAABBBBAAAABBBBAAAAAAAAA                                     |
| 28     | B            | 3-6 0-0 0-15     | AAAABBBBAAAABBBBAAAABBBBAAAAAAAAAAAAA                                         |
| 29     | A            | 7-5 0-0 15-0     | AAAABBBBAAAABBBBAAAABBBBAAAABBBBAAAABBBBAAAAAAAAA                             |
| 30     | B            | 3-6 6-4 0-0 0-15 | AAAABBBBAAAABBBBAAAABBBBAAAAAAAAAAAABBBBAAAABBBBAAAABBBBAAAABBBBAAAABBBBBBBBA |