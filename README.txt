*** ProgramName: LongestParentWord ****

Usage:
LogestParentWord.exe 'C:\Temp\wordList.txt'

Type:
Console application (exe)

Details:
1. The program will throw an exception and terminates if arguments are supplied as expected. (i.e  with no input  or more incorrect inputs).
2. The program will break with a reason if the given input file doesn't actually exists
3. The program outputs the result in the console window for the given input wordsList file.

LongestParentWord

REQUIREMENT
For the given input file with list of words, the program needs to be identify the valid longest parent word that is made up of more number of combination of other small words in the list.
If the word is longer but it does NOT only contains the combination of other words, then it is not considered as the valid word.Also, the program should output the second largest parent word and the count of other small words for the first longest parent word.

OUTPUT:
1. First longest Parent word
2. Second longest Parent word
3. Count of words in the longest parent word.


PROGRAM LOGIC IMPLEMENTATION/FLOW:
1. Input arguments validation
2. Input value validation (the input argument should be a valid WordsList.txt file that should exists on the system where the program is being run)
3. the program reads all the words list and stores in the array.
4. Sort the words list by Word's length descending to identify the longest word in the list. 
       (It is obvious that the longest length word has the high probability to be the longest parent word than the other smaller words in the list.)
5. For each word in the sorted list, valid if the word is made of only the other smallest words in the list and also count the no.of other words occurrences.
6. The program moves char by char for the current word in the list and checks if substring word selected so far exists in the list or NOT. If exists, it then validates if the rest of the word is of valid other words using the same procedure of all possible combinations. If the word is not a valid word, then the other words count will be 0 or can be -1.
7. Therefore by end of this loop, the program has all the info computed for each words in the list and other words count if valid.
8. Sort the list by other words count descending.
9.  So, the word which is in first of the list (sorted by other words count descending) is the first longest parent word and so on)
10. The program will display the result.
(The program also has the comments written when there is a key logic context, please refer)

LANGUAGE USED:
C#.NET

Core C# FUNCTIONS in the program
IsValidFormation(string word, List<string> wordsList)
GetAllPossibleCombination(string word)

PERFORMANCE HANDLING:
The program needs to output only the first two longest word. Therefore, it will exit when it finds the two valid words from the descending sorted words list and output the result very quickly without having to process all the words in the list and irrespective of no.of words in the input file.
