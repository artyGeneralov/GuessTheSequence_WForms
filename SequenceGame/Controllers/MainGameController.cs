using System;
using System.Linq;

namespace GameSetupForm
{
    
    public static class MainGameController
    {
        public static int[] generatedCombination = new int[4];
        public static int MaxChances { get; private set; }
        public static int currentRow { get; private set; } = 1;
        public static int[] currentGuess = new int[4];

        public static void SetChances(int c)
        {
            MaxChances = c;
        }

        public static bool CheckWin()
        {
            bool hasWon = true;
            for(int i = 0; i < 4; i++)
            {
                if(currentGuess[i] != generatedCombination[i])
                {
                    hasWon = false;
                }
            }
            return hasWon;
        }

        public static EGuessResults[] EvaluateGuess()
        {
            EGuessResults[] feedback = new EGuessResults[4];
            int curIndex = 0;
            int feedbackIndex = 0;
            foreach (int curGuess in currentGuess)
            {
                if (generatedCombination.Contains(curGuess))
                {
                    if (generatedCombination[curIndex] == curGuess)
                    {
                        feedback[feedbackIndex] = EGuessResults.CorrectRightPos;
                    }
                    else
                    {
                        feedback[feedbackIndex] = EGuessResults.CorrectWrongPos;
                    }
                    feedbackIndex++;
                }
                curIndex++;
            }
            while(feedbackIndex < 4)
            {
                feedback[feedbackIndex] = EGuessResults.Wrong;
                feedbackIndex++;
            }
            Array.Sort(feedback, (a, b) => b.CompareTo(a));
            return feedback;
        }

        public static void GenerateSequence()
        {
            Random rnd = new Random();
            for (int i = 0; i < generatedCombination.Length; i++)
            {
                int randomNum;
                do
                {
                    randomNum = rnd.Next(1, 9);
                }
                while (generatedCombination.Contains(randomNum));

                generatedCombination[i] = randomNum;
            }
        }

        public static void ClearCurrentGuess()
        {
            for(int i = 0; i < currentGuess.Length; i++)
            {
                currentGuess[i] = 0;
            }
        }

        public static void AddToCurrentGuess(int guess, int position)
        {
            currentGuess[position-1] = guess;
        }

        public static bool currentGuessFull()
        {
            bool isComplete = true;
            foreach(int g in currentGuess)
            {
                if(g == 0)
                {
                    isComplete = false;
                }
            }
            return isComplete;
        }

        public static void nextRow()
        {
            currentRow++;
        }
    }
}
