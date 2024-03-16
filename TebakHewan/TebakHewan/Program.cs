using System;

class Word
{
    public string Value { get; }
    public string[] Clues { get; }

    public Word(string value, string[] clues)
    {
        Value = value.ToLower();
        Clues = clues;
    }
}

class Game
{
    private Word currentWord;
    private char[] guessedLetters;

    public void StartGame()
    {
        do
        {

            Console.Clear();
            Console.WriteLine("==============================================");
            Console.WriteLine("   Selamat datang di Game Tebak-Tebak Hewan!  ");
            Console.WriteLine("==============================================");

            Word[] wordsToGuess = {
                new Word("singa", new string[] { "Karnivora kuat, bulu coklat/kuning.", "Hidup berkelompok.", "Makan daging sebagai makanan utama." }),
                new Word("gajah", new string[] { "Mamalia besar, gading panjang.", "Pandai berenang.", "Telinga besar untuk mendengar suara jauh." }),
                new Word("kucing", new string[] { "Peliharaan rumah, bulu lembut.", "Cuci muka dengan lidah.", "Bisa melompat tinggi." }),
                new Word("kelinci", new string[] { "Hewan kecil, berbulu.", "Telinga panjang.", "Biasanya dikenal sebagai hewan kelinci." }),
                new Word("harimau", new string[] { "Karnivora besar, bulu oranye/hitam.", "Habitat di hutan atau savana.", "Termasuk hewan yang terancam punah." }),
                new Word("jerapah", new string[] { "Hewan tinggi dengan leher panjang.", "Dikenal sebagai hewan pemakan daun.", "Tinggal di savana Afrika." }),
                new Word("penguin", new string[] { "Burung yang tidak bisa terbang.", "Hidup di lingkungan dingin.", "Memiliki bulu hitam dan putih." }),
                new Word("kanguru", new string[] { "Hewan yang berasal dari Australia.", "Memiliki kantung di perutnya.", "Melompat menggunakan kaki belakangnya." }),

            };

            currentWord = GetRandomWord(wordsToGuess);
            guessedLetters = new char[currentWord.Value.Length];

            InitializeGuessedLetters();
            DisplayWordClues();

            int attempts = 0;

            do
            {
                DisplayGuessedWord();
                ProcessGuess();

                attempts++;

            } while (!IsWordGuessed());

            Console.WriteLine($"Selamat! Anda berhasil menebak kata '{currentWord.Value}' dalam {attempts} percobaan.");
            Console.Write("Ingin Bermain Lagi? (y/n): ");
        } while (Console.ReadLine().ToLower() == "y");
    }

    private Word GetRandomWord(Word[] words)
    {
        int index = new Random().Next(words.Length);
        return words[index];
    }

    private void InitializeGuessedLetters()
    {
        for (int i = 0; i < guessedLetters.Length; i++)
        {
            guessedLetters[i] = '_';
        }
    }

    private void DisplayGuessedWord()
    {
        Console.WriteLine("Kata yang harus ditebak:");
        foreach (char letter in guessedLetters)
        {
            Console.Write($"{letter} ");
        }
        Console.WriteLine();
    }

    private void DisplayWordClues()
    {
        Console.WriteLine("\nClue:");
        foreach (string clue in currentWord.Clues)
        {
            Console.WriteLine($"- {clue}");
        }
    }

    private void ProcessGuess()
    {
        Console.Write("\nHayoo Coba Tebak : ");
        string guess = Console.ReadLine().ToLower();

        if (guess.Length == 1 && Char.IsLetter(guess[0]))
        {
            char guessedLetter = guess[0];

            if (IsLetterGuessed(guessedLetter))
            {
                Console.WriteLine("Anda sudah menebak huruf ini sebelumnya. Coba lagi.");
            }
            else
            {
                UpdateGuessedLetters(guessedLetter);
            }
        }
        else if (guess.Length == currentWord.Value.Length && guess == currentWord.Value)
        {
            guessedLetters = currentWord.Value.ToCharArray();
        }
        else
        {
            Console.WriteLine("Kata yang anda masukkan salah. Ayo Coba lagi!!");
        }
    }

    private bool IsLetterGuessed(char letter)
    {
        return Array.IndexOf(guessedLetters, letter) != -1;
    }

    private void UpdateGuessedLetters(char guessedLetter)
    {
        for (int i = 0; i < currentWord.Value.Length; i++)
        {
            if (currentWord.Value[i] == guessedLetter)
            {
                guessedLetters[i] = guessedLetter;
            }
        }
    }

    private bool IsWordGuessed()
    {
        return Array.IndexOf(guessedLetters, '_') == -1;
    }
}

class Program
{
    static void Main()
    {
        bool exitGame = false;

        while (!exitGame)
        {
            Console.Clear();

            Console.WriteLine("======================================");
            Console.WriteLine("        Game Tebak-Tebakan Hewan      ");
            Console.WriteLine("======================================");
            Console.WriteLine("1. Mulai Permainan");
            Console.WriteLine("2. Cara Bermain");
            Console.WriteLine("3. Keluar");
            Console.WriteLine("======================================");

            Console.Write("Pilih menu (1/2/3): ");
            string menuChoice = Console.ReadLine();

            switch (menuChoice)
            {
                case "1":
                    Game game = new Game();
                    game.StartGame();
                    break;
                case "2":
                    DisplayInstructions();
                    break;
                case "3":
                    exitGame = true;
                    break;
                default:
                    Console.WriteLine("Pilihan tidak valid. Silakan pilih lagi.");
                    break;
            }
        }

        Console.WriteLine("Terima kasih telah bermain!");
    }

    static void DisplayInstructions()
    {
        Console.Clear();

        Console.WriteLine("======================================");
        Console.WriteLine("            Cara Bermain              ");
        Console.WriteLine("======================================");
        Console.WriteLine("1. Pilih menu 'Mulai Permainan' untuk memulai tebak-tebakan kata.");
        Console.WriteLine("2. Anda akan diberikan beberapa clue untuk kata yang harus ditebak.");
        Console.WriteLine("3. Tebak kata dengan menebak huruf atau seluruh kata secara utuh.");
        Console.WriteLine("4. Anda memiliki beberapa kesempatan untuk menebak sebelum permainan berakhir.");
        Console.WriteLine("5. Setelah menebak kata dengan benar, Anda akan mendapatkan hasil akhir.");
        Console.WriteLine("6. Tekan Enter untuk kembali ke menu.");
        Console.WriteLine("======================================");

        Console.ReadLine();
    }
}
