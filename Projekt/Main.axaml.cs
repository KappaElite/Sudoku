using System;
using System.Collections.Generic;
using Avalonia.Controls;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Media;

namespace Projekt
{
    public partial class Main : Window, ISubject
    {
        private static Main _main = null;
        private SudokuCreator _test;
        private int[][] _sudokuWithBlanks;
        private int[][] _sudokuWithoutBlanks;
        private IObserver[][] _UISudoku;
        private int _numberofValidNumbers;
        private IOperationStrategy _strategy;
        

        private Main()
        {
            _UISudoku = new IObserver[9][];
            _numberofValidNumbers = 0;
            for (int i = 0; i < 9; i++)
            {
                _UISudoku[i] = new IObserver[9];
            }
            InitializeComponent();
        }

        public static Main GetInstance()
        {
            if (_main == null)
            {
                _main = new Main();
            }

            return _main;
        }
        

        private void OnStartButtonClicked(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            do
            {
                _test = new SudokuCreator();
            } while (_test.TestSudoku() < 40);

            _sudokuWithBlanks = _test.GetSudokuWithBlanks();
            _sudokuWithoutBlanks = _test.GetSudoku();

            if (DifficultyComboBox.SelectedIndex == 0)
            {
               SetStrategy(new EasyModeStrategy(HintsAvailableText));
               
            }
            else if (DifficultyComboBox.SelectedIndex == 1)
            {
                SetStrategy(new HardModeStrategy(HintsAvailableText));
            }
            
            
            _strategy.InitializeHints();
            DifficultyText.IsVisible = false;
            StartButton.IsVisible = false;
            DifficultyComboBox.IsVisible = false;
            ShowAnswerButton.IsVisible = true;
            CheckButton.IsVisible = true;
            HintButton.IsVisible = true;
            HintsAvailableText.IsVisible = true;
            InitializeValue();
            AttachTextChangedEvents();

            for (int i = 0; i < 9; i++)
            {
                if (i % 3 == 0)
                {
                    Console.WriteLine("-------------------");
                }
                Console.WriteLine($"{_sudokuWithoutBlanks[i][0]} , {_sudokuWithoutBlanks[i][1]} ,{_sudokuWithoutBlanks[i][2]} ,{_sudokuWithoutBlanks[i][3]} ,{_sudokuWithoutBlanks[i][4]} ,{_sudokuWithoutBlanks[i][5]} ,{_sudokuWithoutBlanks[i][6]} ,{_sudokuWithoutBlanks[i][7]} ,{_sudokuWithoutBlanks[i][8]}");
            }

        }
        
        
        private void OnShowAnswerButtonClicked(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            ShowAnswerButton.IsVisible = false;
            CheckButton.IsVisible = false;
            HintButton.IsVisible = false;
            HintsAvailableText.IsVisible = false;
            var uniformGrind = this.FindControl<UniformGrid>("SudokuUniformGrid");
            uniformGrind.Children.Clear();

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    var textBox = new TextBox
                    {
                        Margin = new Thickness(5),
                        MaxLength = 1,
                        FontSize = 24,
                        TextAlignment = TextAlignment.Center,
                        BorderThickness = new Thickness(1),
                        Text = _sudokuWithoutBlanks[i][j].ToString(),
                        IsReadOnly = true
                    };
                    uniformGrind.Children.Add(textBox);
                }
            }
        }

        private void OnCheckAnswerButtonClicked(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (_UISudoku[i][j] is SingleSudokuField singleSudokuField)
                    {
                        if (singleSudokuField.GetState() is ValidInputeState)
                        {
                            continue;
                        }

                        if (singleSudokuField.ReturnConvertedNumber() == _sudokuWithoutBlanks[i][j])
                        {
                            singleSudokuField.SetState(new ValidInputeState());
                            _numberofValidNumbers++;
                        }
                        else
                        {
                            singleSudokuField.SetState(new InvalidInputState());
                        }
                    }
                }
            }

            if (_numberofValidNumbers == 81)
            {
                ShowAnswerButton.IsVisible = false;
                CheckButton.IsVisible = false;
                HintButton.IsVisible = false;
                HintsAvailableText.IsVisible = false;
            }
        }


        private void OnShowHintButtonClicked(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Random random = new Random();
            int maxAttempts = 100;
            
            _strategy.Handle();
            if (_strategy.GetNumberOfHints() == 0)
            {
                HintButton.IsVisible = false;
            }

            for (int i = 0; i < maxAttempts; i++)
            {
                int a = random.Next(0, 9);
                int b = random.Next(0, 9);

                if (_UISudoku[a][b] is SingleSudokuField tmp)
                {
                    if (tmp.GetState() is ValidInputeState)
                    {
                        continue;
                    }
                    
                    tmp.GetTextBox().Text = _sudokuWithoutBlanks[a][b].ToString();
                    tmp.SetState(new ValidInputeState());
                    _numberofValidNumbers++;
                    
                    if (_numberofValidNumbers == 81)
                    {
                        ShowAnswerButton.IsVisible = false;
                        CheckButton.IsVisible = false;
                        HintButton.IsVisible = false;
                        HintsAvailableText.IsVisible = false;
                    }
                    return;
                    
                }
            }


            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (_UISudoku[i][j] is SingleSudokuField tmp)
                    {
                        if (tmp.GetState() is ValidInputeState)
                        {
                            continue;
                        }
                        
                        tmp.GetTextBox().Text = _sudokuWithoutBlanks[i][j].ToString();
                        tmp.SetState(new ValidInputeState());
                        
                        _numberofValidNumbers++;
                        if (_numberofValidNumbers == 81)
                        {
                            ShowAnswerButton.IsVisible = false;
                            CheckButton.IsVisible = false;
                            HintButton.IsVisible = false;
                            HintsAvailableText.IsVisible = false;
                        }
                        return;
                    }
                }
            }
            
            
        }
        
        private void InitializeValue()
        {
            var uniformGrid = this.FindControl<UniformGrid>("SudokuUniformGrid");
            uniformGrid.Children.Clear();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    var textBox = new TextBox
                    {
                        Margin = new Thickness(5),
                        MaxLength = 1,
                        FontSize = 24,
                        TextAlignment = TextAlignment.Center,
                        BorderThickness = new Thickness(1),
                        Tag = $"{i},{j}"
                    };

                    if (_sudokuWithBlanks[i][j] == 0)
                    {
                        textBox.Text = "";
                    }
                    else
                    {
                        _numberofValidNumbers++;
                        textBox.Text = _sudokuWithBlanks[i][j].ToString();
                        textBox.IsReadOnly = true;
                        
                    }
                    AddObserver(new SingleSudokuField(textBox), i,j);
                    uniformGrid.Children.Add(textBox);
                    
                }
            }
        }
        private void AttachTextChangedEvents()
        {
            var uniformGrid = this.FindControl<UniformGrid>("SudokuUniformGrid");
    
            foreach (var child in uniformGrid.Children)
            {
                if (child is TextBox textBox && !textBox.IsReadOnly)
                {
                    textBox.TextChanged += (sender, args) => OnPlayerInput(sender, args);
                }
            }
        }
        
        private void OnPlayerInput(object sender, EventArgs args)
        {
            var textBox = sender as TextBox;
            string tagValue = textBox.Tag as string;
            if (!string.IsNullOrEmpty(tagValue))
            {
                var parts = tagValue.Split(',');
                int row = int.Parse(parts[0]);
                int col = int.Parse(parts[1]);

                if (_UISudoku[row][col] is SingleSudokuField tmp)
                {
                    if (tmp.GetState() is InvalidInputState)
                    {
                        textBox.Foreground = Brushes.White;
                    }
                    NotifyObserver(_UISudoku[row][col], textBox);
                }
            }
        }
        
        
        public void AddObserver(IObserver observer, int x, int y)
        {
            _UISudoku[x][y] = observer;
        }

        public void NotifyObserver(IObserver observer,TextBox textBox)
        {
            observer.Update(textBox);
        }

        public void SetStrategy(IOperationStrategy strategy)
        {
            _strategy = strategy;
        }
        
    }
}